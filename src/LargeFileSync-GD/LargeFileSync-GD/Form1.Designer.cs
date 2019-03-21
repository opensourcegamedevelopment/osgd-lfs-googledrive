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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSyncFiles
            // 
            this.btnSyncFiles.Location = new System.Drawing.Point(452, 12);
            this.btnSyncFiles.Name = "btnSyncFiles";
            this.btnSyncFiles.Size = new System.Drawing.Size(187, 121);
            this.btnSyncFiles.TabIndex = 1;
            this.btnSyncFiles.Text = "Sync Content Files";
            this.btnSyncFiles.UseVisualStyleBackColor = true;
            this.btnSyncFiles.Click += new System.EventHandler(this.btnSyncFiles_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(230, 95);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(205, 38);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnReAuthenticate
            // 
            this.btnReAuthenticate.Location = new System.Drawing.Point(12, 95);
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
            this.label1.Location = new System.Drawing.Point(18, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "My Content File Location: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 14);
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
            this.txtProjectID.Size = new System.Drawing.Size(249, 20);
            this.txtProjectID.TabIndex = 6;
            // 
            // txtMyContentFileLocation
            // 
            this.txtMyContentFileLocation.Location = new System.Drawing.Point(148, 43);
            this.txtMyContentFileLocation.Name = "txtMyContentFileLocation";
            this.txtMyContentFileLocation.ReadOnly = true;
            this.txtMyContentFileLocation.Size = new System.Drawing.Size(249, 20);
            this.txtMyContentFileLocation.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMyContentFileLocation);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtProjectID);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 77);
            this.panel1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 146);
            this.Controls.Add(this.btnSyncFiles);
            this.Controls.Add(this.btnReAuthenticate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSettings);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

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
    }
}

