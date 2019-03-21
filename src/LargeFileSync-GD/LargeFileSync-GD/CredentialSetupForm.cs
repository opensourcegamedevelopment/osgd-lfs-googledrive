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
            /*
            if (txtProjectID.Text == "")
            {
                MessageBox.Show("Please Enter Project ID");
            }
            else if (txtClientID.Text == "")
            {
                MessageBox.Show("Please Enter Clicen ID");
            }
            else if (txtClientSecret.Text == "")
            {
                MessageBox.Show("Please Enter Clicen Secret");
            }
            else
            {
                credential.installed.project_id = txtProjectID.Text;
                credential.installed.client_id = txtClientID.Text;
                credential.installed.client_secret = txtClientSecret.Text;
                    
                using (StreamWriter streamWriter = new StreamWriter("credentials.json"))
                {
                    string newJson = JsonConvert.SerializeObject(credential);
                    streamWriter.Write(newJson);
                }
                this.Close();
            }*/
        }

        private void CredentialSetupForm_Load(object sender, EventArgs e)
        {
            using (StreamReader r = new StreamReader("credentials.json"))
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
                File.Copy(fileName, "credentials.json",true);

                using (StreamReader r = new StreamReader("credentials.json"))
                {
                    string json = r.ReadToEnd();
                    credential = JsonConvert.DeserializeObject<RootObject>(json);
                    txtClientID.Text = credential.installed.client_id;
                    txtProjectID.Text = credential.installed.project_id;
                    txtClientSecret.Text = credential.installed.client_secret;
                    projectid = credential.installed.project_id;
                }
            }
        }
    }
}
