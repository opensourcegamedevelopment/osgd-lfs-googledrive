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
            this.SuspendLayout();
            // 
            // btnSyncFiles
            // 
            this.btnSyncFiles.Location = new System.Drawing.Point(24, 56);
            this.btnSyncFiles.Name = "btnSyncFiles";
            this.btnSyncFiles.Size = new System.Drawing.Size(187, 84);
            this.btnSyncFiles.TabIndex = 1;
            this.btnSyncFiles.Text = "Sync Content Files";
            this.btnSyncFiles.UseVisualStyleBackColor = true;
            this.btnSyncFiles.Click += new System.EventHandler(this.btnSyncFiles_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(24, 12);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(187, 38);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnReAuthenticate
            // 
            this.btnReAuthenticate.Location = new System.Drawing.Point(24, 146);
            this.btnReAuthenticate.Name = "btnReAuthenticate";
            this.btnReAuthenticate.Size = new System.Drawing.Size(187, 27);
            this.btnReAuthenticate.TabIndex = 3;
            this.btnReAuthenticate.Text = "ReAuthenticate";
            this.btnReAuthenticate.UseVisualStyleBackColor = true;
            this.btnReAuthenticate.Click += new System.EventHandler(this.btnReAuthenticate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 186);
            this.Controls.Add(this.btnReAuthenticate);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnSyncFiles);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSyncFiles;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnReAuthenticate;
    }
}

