using BulkCertificationTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCertificationTool.Contracts
{
    internal interface IExcelService
    {
        IResult<bool> LoadSettings(string fileName);
        IEnumerable<User> Users { get; }
        IEnumerable<Language> Languages { get; }
    }
}
