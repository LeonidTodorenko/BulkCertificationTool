namespace BulkCertificationTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblSourceLang = new System.Windows.Forms.Label();
            this.cbSourceLang = new System.Windows.Forms.ComboBox();
            this.lblTargetLang = new System.Windows.Forms.Label();
            this.cbTargetLang = new System.Windows.Forms.ComboBox();
            this.lblProject = new System.Windows.Forms.Label();
            this.tbProject = new System.Windows.Forms.TextBox();
            this.lblSingleBates = new System.Windows.Forms.Label();
            this.tbSingleBates = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.btnStop = new System.Windows.Forms.Button();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.lblNotCreatedCount = new System.Windows.Forms.Label();
            this.gbSettings.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSourceLang
            // 
            this.lblSourceLang.AutoSize = true;
            this.lblSourceLang.Location = new System.Drawing.Point(6, 71);
            this.lblSourceLang.Name = "lblSourceLang";
            this.lblSourceLang.Size = new System.Drawing.Size(92, 13);
            this.lblSourceLang.TabIndex = 0;
            this.lblSourceLang.Text = "Source Language";
            // 
            // cbSourceLang
            // 
            this.cbSourceLang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbSourceLang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSourceLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceLang.FormattingEnabled = true;
            this.cbSourceLang.Location = new System.Drawing.Point(6, 87);
            this.cbSourceLang.Name = "cbSourceLang";
            this.cbSourceLang.Size = new System.Drawing.Size(121, 21);
            this.cbSourceLang.TabIndex = 0;
            // 
            // lblTargetLang
            // 
            this.lblTargetLang.AutoSize = true;
            this.lblTargetLang.Location = new System.Drawing.Point(155, 71);
            this.lblTargetLang.Name = "lblTargetLang";
            this.lblTargetLang.Size = new System.Drawing.Size(89, 13);
            this.lblTargetLang.TabIndex = 0;
            this.lblTargetLang.Text = "Target Language";
            // 
            // cbTargetLang
            // 
            this.cbTargetLang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTargetLang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTargetLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTargetLang.FormattingEnabled = true;
            this.cbTargetLang.Location = new System.Drawing.Point(158, 87);
            this.cbTargetLang.Name = "cbTargetLang";
            this.cbTargetLang.Size = new System.Drawing.Size(121, 21);
            this.cbTargetLang.TabIndex = 1;
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(3, 121);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(50, 13);
            this.lblProject.TabIndex = 2;
            this.lblProject.Text = "Project #";
            // 
            // tbProject
            // 
            this.tbProject.Location = new System.Drawing.Point(6, 137);
            this.tbProject.Name = "tbProject";
            this.tbProject.Size = new System.Drawing.Size(121, 20);
            this.tbProject.TabIndex = 2;
            // 
            // lblSingleBates
            // 
            this.lblSingleBates.AutoSize = true;
            this.lblSingleBates.Location = new System.Drawing.Point(155, 120);
            this.lblSingleBates.Name = "lblSingleBates";
            this.lblSingleBates.Size = new System.Drawing.Size(76, 13);
            this.lblSingleBates.TabIndex = 2;
            this.lblSingleBates.Text = "Single Bates #";
            // 
            // tbSingleBates
            // 
            this.tbSingleBates.Location = new System.Drawing.Point(158, 137);
            this.tbSingleBates.Name = "tbSingleBates";
            this.tbSingleBates.Size = new System.Drawing.Size(121, 20);
            this.tbSingleBates.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(64, 251);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Generate";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(18, 210);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(84, 13);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Text = "Progress {0}/{1}";
            this.lblProgress.Visible = false;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(18, 226);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(273, 19);
            this.pbProgress.TabIndex = 6;
            this.pbProgress.Visible = false;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(170, 251);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.cbUsers);
            this.gbSettings.Controls.Add(this.cbSourceLang);
            this.gbSettings.Controls.Add(this.lblUser);
            this.gbSettings.Controls.Add(this.lblSourceLang);
            this.gbSettings.Controls.Add(this.lblTargetLang);
            this.gbSettings.Controls.Add(this.cbTargetLang);
            this.gbSettings.Controls.Add(this.lblProject);
            this.gbSettings.Controls.Add(this.tbSingleBates);
            this.gbSettings.Controls.Add(this.tbProject);
            this.gbSettings.Controls.Add(this.lblSingleBates);
            this.gbSettings.Location = new System.Drawing.Point(12, 12);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(285, 170);
            this.gbSettings.TabIndex = 0;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // cbUsers
            // 
            this.cbUsers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbUsers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(6, 38);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(273, 21);
            this.cbUsers.TabIndex = 0;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(9, 22);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(49, 13);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Manager";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(96, 189);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(117, 13);
            this.lblError.TabIndex = 7;
            this.lblError.Text = "Please fill all fields!";
            this.lblError.Visible = false;
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.rtbLog);
            this.gbLog.Location = new System.Drawing.Point(303, 12);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(319, 262);
            this.gbLog.TabIndex = 8;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(3, 16);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(313, 243);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // lblNotCreatedCount
            // 
            this.lblNotCreatedCount.AutoSize = true;
            this.lblNotCreatedCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblNotCreatedCount.ForeColor = System.Drawing.Color.Red;
            this.lblNotCreatedCount.Location = new System.Drawing.Point(140, 210);
            this.lblNotCreatedCount.Name = "lblNotCreatedCount";
            this.lblNotCreatedCount.Size = new System.Drawing.Size(151, 13);
            this.lblNotCreatedCount.TabIndex = 9;
            this.lblNotCreatedCount.Text = "{0} files were not created";
            this.lblNotCreatedCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblNotCreatedCount.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 286);
            this.Controls.Add(this.lblNotCreatedCount);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bulk Certification Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.gbLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSourceLang;
        private System.Windows.Forms.ComboBox cbSourceLang;
        private System.Windows.Forms.Label lblTargetLang;
        private System.Windows.Forms.ComboBox cbTargetLang;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.TextBox tbProject;
        private System.Windows.Forms.Label lblSingleBates;
        private System.Windows.Forms.TextBox tbSingleBates;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Label lblNotCreatedCount;
    }
}

