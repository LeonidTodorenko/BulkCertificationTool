using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCertificationTool.Models
{
    internal class TemplateParts
    {
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
        public string SingleBates { get; set; }
        public string ProjectNumber { get; set; }
        public string FileName { get; set; }
        public string Manager { get; set; }
        public string ManagerSignature { get; set; }
    }
}
