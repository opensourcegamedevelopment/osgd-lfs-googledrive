namespace LargeFileSync_GD
{
    partial class Form1
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
            this.btnSyncFiles = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnReAuthenticate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProjectID = new System.Windows.Forms.TextBox();
            this.txtMyContentFileLocation = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenerateMetaData = new System.Windows.Forms.Button();
            this.OutputArea = new System.Windows.Forms.RichTextBox();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.LblDownloadProgress = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSyncFiles
            // 
            this.btnSyncFiles.Location = new System.Drawing.Point(452, 12);
            this.btnSyncFiles.Name = "btnSyncFiles";
            this.btnSyncFiles.Size = new System.Drawing.Size(187, 103);
            this.btnSyncFiles.TabIndex = 1;
            this.btnSyncFiles.Text = "Sync Content Files";
            this.btnSyncFiles.UseVisualStyleBackColor = true;
            this.btnSyncFiles.Click += new System.EventHandler(this.btnSyncFiles_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(230, 121);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(205, 38);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnReAuthenticate
            // 
            this.btnReAuthenticate.Location = new System.Drawing.Point(12, 121);
            this.btnReAuthenticate.Name = "btnReAuthenticate";
            this.btnReAuthenticate.Size = new System.Drawing.Size(212, 38);
            this.btnReAuthenticate.TabIndex = 3;
            this.btnReAuthenticate.Text = "ReAuthenticate";
            this.btnReAuthenticate.UseVisualStyleBackColor = true;
            this.btnReAuthenticate.Click += new System.EventHandler(this.btnReAuthenticate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "My Content File Location: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Project ID:";
            // 
            // txtProjectID
            // 
            this.txtProjectID.Location = new System.Drawing.Point(148, 14);
            this.txtProjectID.Name = "txtProjectID";
            this.txtProjectID.ReadOnly = true;
            this.txtProjectID.Size = new System.Drawing.Size(272, 20);
            this.txtProjectID.TabIndex = 6;
            // 
            // txtMyContentFileLocation
            // 
            this.txtMyContentFileLocation.Location = new System.Drawing.Point(148, 69);
            this.txtMyContentFileLocation.Name = "txtMyContentFileLocation";
            this.txtMyContentFileLocation.ReadOnly = true;
            this.txtMyContentFileLocation.Size = new System.Drawing.Size(272, 20);
            this.txtMyContentFileLocation.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtProjectName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMyContentFileLocation);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtProjectID);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 103);
            this.panel1.TabIndex = 10;
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(148, 40);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(272, 20);
            this.txtProjectName.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "ProjectName: ";
            // 
            // btnGenerateMetaData
            // 
            this.btnGenerateMetaData.Location = new System.Drawing.Point(452, 122);
            this.btnGenerateMetaData.Name = "btnGenerateMetaData";
            this.btnGenerateMetaData.Size = new System.Drawing.Size(187, 37);
            this.btnGenerateMetaData.TabIndex = 11;
            this.btnGenerateMetaData.Text = "GenerateMetaData";
            this.btnGenerateMetaData.UseVisualStyleBackColor = true;
            this.btnGenerateMetaData.Click += new System.EventHandler(this.btnGenerateMetaData_Click);
            // 
            // OutputArea
            // 
            this.OutputArea.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.OutputArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputArea.ForeColor = System.Drawing.SystemColors.Info;
            this.OutputArea.Location = new System.Drawing.Point(12, 165);
            this.OutputArea.Name = "OutputArea";
            this.OutputArea.ReadOnly = true;
            this.OutputArea.Size = new System.Drawing.Size(627, 215);
            this.OutputArea.TabIndex = 13;
            this.OutputArea.Text = "";
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(12, 409);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(627, 23);
            this.DownloadProgressBar.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 387);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Downloading: ";
            // 
            // LblDownloadProgress
            // 
            this.LblDownloadProgress.AutoSize = true;
            this.LblDownloadProgress.Location = new System.Drawing.Point(83, 387);
            this.LblDownloadProgress.Name = "LblDownloadProgress";
            this.LblDownloadProgress.Size = new System.Drawing.Size(33, 13);
            this.LblDownloadProgress.TabIndex = 16;
            this.LblDownloadProgress.Text = "None";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 444);
            this.Controls.Add(this.LblDownloadProgress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DownloadProgressBar);
            this.Controls.Add(this.OutputArea);
            this.Controls.Add(this.btnGenerateMetaData);
            this.Controls.Add(this.btnSyncFiles);
            this.Controls.Add(this.btnReAuthenticate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSettings);
            this.Name = "Form1";
            this.Text = "GoogleDrive-Large File Sync";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSyncFiles;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnReAuthenticate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProjectID;
        private System.Windows.Forms.TextBox txtMyContentFileLocation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGenerateMetaData;
        private System.Windows.Forms.RichTextBox OutputArea;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LblDownloadProgress;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

