using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCertificationTool.Contracts
{
    internal interface IWordService
    {
        IResult<bool> LoadTemplate(string fileName);
        IResult<bool> ApplyBatesNumbersChanges(string user, string userSignPath, string sourceLang, string targetLang, string project, string bates);
        IResult<bool> ApplyMiscChanges(string user, string userSignPath, string sourceLang, string targetLang, string project, string fileName);
        Task<IResult<bool>> SaveFileAsAsync(string newPath);
    }
}
