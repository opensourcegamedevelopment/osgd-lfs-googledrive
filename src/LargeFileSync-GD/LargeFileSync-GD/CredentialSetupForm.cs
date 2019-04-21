using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace LargeFileSync_GD
{
    public partial class CredentialSetupForm : Form
    {
        RootObject credential;
        Form1 frm1;
        string projectid;

        public CredentialSetupForm(Form1 parent)
        {
            InitializeComponent();
            frm1 = parent;
            this.FormClosing += Form1_FormClosing;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            frm1.updateTxtProjectID(projectid);
            this.Close();
        }

        private void CredentialSetupForm_Load(object sender, EventArgs e)
        {
            using (StreamReader r = new StreamReader("data/credentials.json"))
            {
                string json = r.ReadToEnd();
                credential = JsonConvert.DeserializeObject<RootObject>(json);

                txtClientID.Text = credential.installed.client_id;
                txtProjectID.Text = credential.installed.project_id;
                txtClientSecret.Text = credential.installed.client_secret;
                projectid = credential.installed.project_id;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (credential.installed.client_id == "" ||
                credential.installed.project_id == "" ||
                credential.installed.client_secret == "")
            {
                Application.Exit();
            }

        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
           DialogResult dialogResult = openFileDialog1.ShowDialog();
           if (dialogResult == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                Console.WriteLine(fileName);
                File.Copy(fileName, "data/credentials.json",true);

                using (StreamReader r = new StreamReader("data/credentials.json"))
                {
                    string json = r.ReadToEnd();
                    credential = JsonConvert.DeserializeObject<RootObject>(json);
                    txtClientID.Text = credential.installed.client_id;
                    txtProjectID.Text = credential.installed.project_id;
                    txtClientSecret.Text = credential.installed.client_secret;
                    projectid = credential.installed.project_id;

                    frm1.updateTxtClientID(txtClientID.Text);
                    frm1.updateTxtProjectID(txtProjectID.Text);
                    frm1.updateTxtClientSecret(txtClientSecret.Text);
                }
            }
        }
    }
}
