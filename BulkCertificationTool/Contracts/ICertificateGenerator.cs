using BulkCertificationTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCertificationTool.Contracts
{
    internal interface ICertificateGenerator
    {
        event EventHandler Started;
        event EventHandler Ended;
        event EventHandler<bool> FileCreated;

        IResult<bool> LoadTemplate();
        IResult<bool> LoadSettingsData();
        AppModes Mode { get; }
        IEnumerable<Language> Languages { get; }
        IEnumerable<User> Users { get; }
        void StartProcess(string user, string sourceLang, string targetLang, string project, string bates);
        int FilesCount { get; }
        void Stop();
        IEnumerable<string> Errors { get; }
    }
}
