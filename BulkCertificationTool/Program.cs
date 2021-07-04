using BulkCertificationTool.Contracts;
using BulkCertificationTool.Models;
using BulkCertificationTool.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkCertificationTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                MessageBox.Show("There is no file to analyze.", "Bulk Certification Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //application works in 2 mode: generate "bates numbers" or "miscellaneous" certificate
            //first argument is a flag of this work mode
            var modeArg = args[0];
            AppModes mode;

            switch(modeArg)
            {
                case "/bates":
                    mode = AppModes.BatesNumbers;
                    break;
                case "/misc":
                    mode = AppModes.Misc;
                    break;
                default:
                    MessageBox.Show("Unsupported work mode!", "Bulk Certification Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            //first instance will get all filenames from other instances
            //and will process them
            Mutex mutexMain = new Mutex(false, "BulkCertificationTool");
            string pipeName = "BulkCertificationToolPipe";
            ConcurrentStack<string> fileNames = new ConcurrentStack<string>();
            string[] items = new string[256];

            try
            {
                if (mutexMain.WaitOne(0, false))
                {
                    fileNames.Push(args[1]);
                    using (var pipeServer = new PipesManager(pipeName))
                    {
                        pipeServer.StartServer(fileNames);
                        Thread.Sleep(500);  //wait for getting messages from another instances
                        fileNames.TryPopRange(items);
                    }
                }
                else
                {
                    var pipeClient = new PipesManager(pipeName);
                    pipeClient.SendClientMessage(args[1]);
                    //It need finish a process after sended filename to the server
                    return;
                }
            }
            finally
            {
                if (mutexMain != null)
                {
                    mutexMain.Close();
                    mutexMain = null;
                }
            }

            StartProcessFiles(mode, items);
        }

        private static void StartProcessFiles(AppModes mode, IEnumerable<string> fileNames)
        {
            // Path to templates (Word files, Excel files etc.)
            var templatesPath = ConfigurationManager.AppSettings["templatesPath"];
            // Excel file with users and languages lists
            var excelFile = ConfigurationManager.AppSettings["excelData"];
            // Filename of template "Park IP Certification - bates numbers (single).docx"
            var batesNumbersFile = ConfigurationManager.AppSettings["batesNumbersFile"];
            // Filename of template "Park IP Certification - miscellaneous.docx"
            var miscellaneousFile = ConfigurationManager.AppSettings["miscellaneousFile"];
            // Filename prefix for saving
            var filePrefix = ConfigurationManager.AppSettings["filePrefix"];

            string template = string.Empty;
            switch(mode)
            {
                case (AppModes.BatesNumbers):
                    template = batesNumbersFile;
                    break;
                case (AppModes.Misc):
                    template = miscellaneousFile;
                    break;
                default:
                    MessageBox.Show("Unsupported work mode!", "Bulk Certification Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            var parts = new TemplateParts
            {
                FileName = ConfigurationManager.AppSettings["fileNameTmpl"],
                ProjectNumber = ConfigurationManager.AppSettings["projectNumberTmpl"],
                SingleBates = ConfigurationManager.AppSettings["singleBatesTmpl"],
                SourceLanguage = ConfigurationManager.AppSettings["sourceLanguageTmpl"],
                TargetLanguage = ConfigurationManager.AppSettings["targetLanguageTmpl"],
                Manager = ConfigurationManager.AppSettings["managerTmpl"],
                ManagerSignature = ConfigurationManager.AppSettings["signTmpl"]
            };

            var arg = new GeneratorData
            {
                FilePrefix = filePrefix,
                FileNames = fileNames.Where(name => !string.IsNullOrWhiteSpace(name)),
                ExcelFile = excelFile,
                ExcelService = new ExcelService(),
                Mode = mode,
                Template = template,
                TemplatesPath = templatesPath,
                WordService = new WordService(parts)               
            };

            ICertificateGenerator generator = new CertificateGenerator(arg);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(generator));
        }
    }
}
