using BulkCertificationTool.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkCertificationTool
{
    internal partial class MainForm : Form
    {
        public MainForm(ICertificateGenerator generator)
        {
            InitializeComponent();

            _generator = generator;
            _generator.Started += (o, e) =>
                GeneratorStarted();

            _generator.Ended += (o, e) =>
                GeneratorEnded();

            _generator.FileCreated += (o, res) =>
                FileCreated(res);

            if(_generator.Mode == AppModes.Misc)
            {
                tbSingleBates.Visible =
                    lblSingleBates.Visible = false;
            }
            _progressLabel = lblProgress.Text;
            _notCreated = lblNotCreatedCount.Text;
            pbProgress.Maximum = _generator.FilesCount;
            lblProgress.Text = string.Format(_progressLabel, pbProgress.Value, _generator.FilesCount);

            //by default log items are hidden
            gbLog.Visible = false;
            Width = Width / 2;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var res = _generator.LoadTemplate();
            if(res.HasError)
            {
                btnStart.Enabled = false;
                WriteLog(res.Error, true);
                return;
            }

            res = _generator.LoadSettingsData();
            if(res.HasError)
            {
                btnStart.Enabled = false;
                WriteLog(res.Error, true);
                return;
            }

            cbSourceLang.DataSource = _generator.Languages.ToList();
            cbSourceLang.DisplayMember = "Name";
            cbSourceLang.ValueMember = "Name";

            cbTargetLang.DataSource = _generator.Languages.ToList();
            cbTargetLang.DisplayMember = "Name";
            cbTargetLang.ValueMember = "Name";

            cbUsers.DataSource = _generator.Users.ToList();
            cbUsers.DisplayMember = "Name";
            cbUsers.ValueMember = "Name";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var validate = ValidateFields();
            lblError.Visible = !validate;

            if(validate)
            {
                btnStart.Enabled = false;
                lblProgress.Visible = pbProgress.Visible = true;
                pbProgress.Value = 0;

                _generator.StartProcess
                    (
                        user: cbUsers.SelectedValue.ToString(),
                        sourceLang: cbSourceLang.SelectedValue.ToString(),
                        targetLang: cbTargetLang.SelectedValue.ToString(),
                        project: tbProject.Text.Trim(),
                        bates: tbSingleBates.Text.Trim()
                    );
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            _generator.Stop();
        }

        private bool ValidateFields()
        {
            if(cbUsers.SelectedValue == null)
            {
                cbUsers.Select();
                return false;
            }

            if(cbSourceLang.SelectedValue == null)
            {
                cbSourceLang.Select();
                return false;
            }

            if(cbTargetLang.SelectedValue == null)
            {
                cbTargetLang.Select();
                return false;
            }

            if(tbProject.Text.Trim().Length == 0)
            {
                tbProject.Select();
                return false;
            }

            if(_generator.Mode == AppModes.BatesNumbers && tbSingleBates.Text.Trim().Length == 0)
            {
                tbSingleBates.Select();
                return false;
            }

            return true;
        }

        private void GeneratorStarted()
        {
            btnStop.Enabled = true;
        }

        private void GeneratorEnded()
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            if (_generator.Errors != null && _generator.Errors.Any())
            {
                var text = _generator.Errors.Aggregate((a, b) => a + Environment.NewLine + b);
                //MessageBox.Show(text, "Bulk Certification Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (var error in _generator.Errors)
                    WriteLog(error, true);

                lblNotCreatedCount.Visible = true;
                lblNotCreatedCount.Text = string.Format(_notCreated, _generator.Errors.Count());
            }
            else
            {
                Application.Exit();
            }
        }

        private void FileCreated(bool res)
        {
            pbProgress.Value++;
            lblProgress.Text = string.Format(_progressLabel, pbProgress.Value, _generator.FilesCount);
        }

        private void WriteLog(string text, bool isError = false)
        {
            if(!gbLog.Visible)
            {
                Width = Width * 2;
                gbLog.Visible = true;
            }

            rtbLog.SelectionColor = isError ? Color.Red : SystemColors.ControlText;
            rtbLog.AppendText(rtbLog.TextLength > 0 ? Environment.NewLine + text : text);
            //MessageBox.Show(text, "Bulk Certification Tool", MessageBoxButtons.OK, isError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
        }

        private readonly ICertificateGenerator _generator;
        private readonly string _progressLabel;
        private readonly string _notCreated;
    }
}
