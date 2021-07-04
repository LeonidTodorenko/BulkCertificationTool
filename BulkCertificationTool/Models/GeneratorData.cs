using BulkCertificationTool.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCertificationTool.Models
{
    internal class GeneratorData
    {
        public IWordService WordService { get; set; }
        public IExcelService ExcelService { get; set; }
        public AppModes Mode { get; set; }
        public string TemplatesPath { get; set; }
        public string Template { get; set; }
        public string ExcelFile { get; set; }
        public string FilePrefix { get; set; }
        public IEnumerable<string> FileNames { get; set; }
    }
}
