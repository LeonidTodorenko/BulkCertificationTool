using BulkCertificationTool.Contracts;
using BulkCertificationTool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BulkCertificationTool
{
    class CertificateGenerator : ICertificateGenerator
    {
        public CertificateGenerator(GeneratorData data)
        {
            _word = data.WordService;
            _excel = data.ExcelService;
            _templateDir = data.TemplatesPath;
            _excelFile = data.ExcelFile;
            _templateFile = data.Template;
            Mode = data.Mode;
            _fileNames = data.FileNames;
            _filePrefix = data.FilePrefix;
        }

        public event EventHandler Started;
        public event EventHandler Ended;
        public event EventHandler<bool> FileCreated;

        public IResult<bool> LoadSettingsData()
        {
            var fullPath = Path.Combine(_templateDir, _excelFile);
            var fi = new FileInfo(fullPath);
            if (!fi.Exists)
                return new Result<bool>(false, string.Format("Settings file \"{0}\" doesn't exists. Application will be stopped.", fullPath));

            if (!fi.Extension.Equals(".xlsx"))
                return new Result<bool>(false, string.Format("Settings file \"{0}\" must be a \".xlsx\"-file. Application will be stopped.", fullPath));

            var res = _excel.LoadSettings(fullPath);
            return res;
        }

        public IResult<bool> LoadTemplate()
        {
            var fullPath = Path.Combine(_templateDir, _templateFile);
            var fi = new FileInfo(fullPath);
            if(!fi.Exists)
                return new Result<bool>(false, string.Format("Template file \"{0}\" doesn't exists. Application will be stopped.", fullPath));

            if(!fi.Extension.Equals(".docx"))
                return new Result<bool>(false, string.Format("Template file \"{0}\" must be a \".docx\"-file. Application will be stopped.", fullPath));

            var res = _word.LoadTemplate(fullPath);
            return res;
        }

        public async void StartProcess(string user, string sourceLang, string targetLang, string project, string bates)
        {
            _errors = new List<string>();
            _cts = new CancellationTokenSource();


            if(Started != null)
            {
                Started(this, new EventArgs());
            }

            var usr = _excel.Users.Single(u => u.Name == user);

            if(Mode == AppModes.BatesNumbers)
                await CreateBatesNumbersFile(usr, sourceLang, targetLang, project, bates);
            else
                await CreateMiscFile(usr, sourceLang, targetLang, project);

            if (Ended != null)
            {
                Ended(this, new EventArgs());
            }

            return;
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public AppModes Mode { get; private set; }
        public IEnumerable<Language> Languages { get { return _excel.Languages; } }
        public IEnumerable<User> Users { get { return _excel.Users; } }
        public int FilesCount { get { return _fileNames.Count(); } }
        public IEnumerable<string> Errors { get { return _errors; } }

        private async Task CreateBatesNumbersFile(User user, string sourceLang, string targetLang, string project, string bates)
        {
            if(string.IsNullOrWhiteSpace(user.ImagePath))
            {
                _errors.Add(string.Format("There is no signature for {0}", user.Name));
                return;
            }

            LoadTemplate();
            var apply = _word.ApplyBatesNumbersChanges(user.Name, user.ImagePath, sourceLang, targetLang, project, bates);
            if (apply.HasError)
            {
                _errors.Add(apply.Error);
                return;
            }

            foreach(var name in _fileNames)
            {
                if (_cts.IsCancellationRequested)
                    return;

                var newPath = Path.Combine(Path.GetDirectoryName(name), _filePrefix + Path.GetFileNameWithoutExtension(name) + ".docx");
                var res = await _word.SaveFileAsAsync(newPath);
                if (FileCreated != null)
                {
                    FileCreated(this, res.Value);
                }
                if(res.HasError)
                    _errors.Add(apply.Error);
            }
        }

        private async Task CreateMiscFile(User user, string sourceLang, string targetLang, string project)
        {
            if (string.IsNullOrWhiteSpace(user.ImagePath))
            {
                _errors.Add(string.Format("There is no signature for {0}", user.Name));
                return;
            }

            foreach(var name in _fileNames)
            {
                if (_cts.IsCancellationRequested)
                    return;

                LoadTemplate();
                var apply = _word.ApplyMiscChanges(user.Name, user.ImagePath, sourceLang, targetLang, project, name);
                if (apply.HasError)
                {
                    _errors.Add(apply.Error);
                    continue;
                }

                var newPath = Path.Combine(Path.GetDirectoryName(name), _filePrefix + Path.GetFileNameWithoutExtension(name) + ".docx");
                var res = await _word.SaveFileAsAsync(newPath);
                if (FileCreated != null)
                {
                    FileCreated(this, res.Value);
                }
                if(res.HasError)
                    _errors.Add(apply.Error);
            }
        }

        private readonly IWordService _word;
        private readonly IExcelService _excel;
        private readonly string _templateDir;
        private readonly string _excelFile;
        private readonly string _templateFile;
        private readonly string _filePrefix;
        private readonly IEnumerable<string> _fileNames;
        private CancellationTokenSource _cts;
        private List<string> _errors;
    }
}
