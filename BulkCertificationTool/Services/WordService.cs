using BulkCertificationTool.Contracts;
using BulkCertificationTool.Models;
using Novacode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Doc;
using Spire.Doc.Documents;

namespace BulkCertificationTool.Services
{
    internal class WordService : IWordService
    {
        public WordService(TemplateParts parts)
        {
            _parts = parts;
        }

        public IResult<bool> LoadTemplate(string fileName)
        {
            try
            {
                _doc = DocX.Load(fileName);
                return new Result<bool>(true);
            }
            catch(Exception ex)
            {
                return new Result<bool>(false, ex.Message);
            }
        }

        public IResult<bool> ApplyBatesNumbersChanges(string user, string userSignPath, string sourceLang, string targetLang, string project, string bates)
        {
            try
            {
                Replace(_parts.SourceLanguage, sourceLang);
                Replace(_parts.TargetLanguage, targetLang);
                Replace(_parts.ProjectNumber, project);
                Replace(_parts.SingleBates, bates);
                Replace(_parts.Manager, user);
                ReplaceImage(userSignPath);

                return new Result<bool>(true);
            }
            catch(Exception ex)
            {
                return new Result<bool>(false, ex.Message);
            }
        }

        public IResult<bool> ApplyMiscChanges(string user, string userSignPath, string sourceLang, string targetLang, string project, string fileName)
        {
            try
            {
                Replace(_parts.SourceLanguage, sourceLang);
                Replace(_parts.TargetLanguage, targetLang);
                Replace(_parts.ProjectNumber, project);
                Replace(_parts.FileName, Path.GetFileName(fileName));
                Replace(_parts.Manager, user);
                ReplaceImage(userSignPath);

                return new Result<bool>(true);
            }
            catch(Exception ex)
            {
                return new Result<bool>(false, ex.Message);
            }
        }

        public Task<IResult<bool>> SaveFileAsAsync(string newPath)
        {
            return Task.Factory.StartNew<IResult<bool>>(() => SaveFileAs(newPath));
        }

        private IResult<bool> SaveFileAs(string newPath)
        {
            try
            {
                //save file to temp directory
                //open in Spire.Doc and save as Pdf
                var temp = Path.Combine(Path.GetTempPath(), Path.GetFileName(newPath));
                _doc.SaveAs(temp);
                var doc = new Document();
                doc.LoadFromFile(temp);
                doc.SaveToFile(newPath.Replace(".docx", ".pdf"), FileFormat.PDF);
                System.IO.File.Delete(temp);
                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, ex.Message);
            }
        }

        private void Replace(string template, string newValue)
        {
            var items = _doc.Paragraphs.Where(p => p.Text.Contains(template));
            foreach(var item in items)
            {
                item.ReplaceText(template, newValue, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
        }

        private void ReplaceImage(string userSignPath)
        {
            //var newBmp = new Bitmap(userSignPath);
            //var img = _doc.Images[1];
            //var b = new Bitmap(img.GetStream(FileMode.Open, FileAccess.ReadWrite));
            //var g = Graphics.FromImage(b);

            //g.DrawImage(newBmp, 0, 0);
            //b.Save(img.GetStream(FileMode.Create, FileAccess.Write), ImageFormat.Jpeg);

            /***********/


            var image = _doc.AddImage(userSignPath);

            var prg = _doc.Paragraphs.FirstOrDefault(p => p.Pictures.Any());
            if(prg != null)
            {
                prg.Pictures[0].Remove();
                prg.InsertPicture(image.CreatePicture());
            }
        }

        private DocX _doc;
        private readonly TemplateParts _parts;
    }
}
