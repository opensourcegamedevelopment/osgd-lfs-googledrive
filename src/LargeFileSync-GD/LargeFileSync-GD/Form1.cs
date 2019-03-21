using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;

namespace LargeFileSync_GD
{
    public partial class Form1 : Form
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Large File Sync for Google Drive - Made By Open Source Game Dev";

        public Form1()
        {
            InitializeComponent();
        }

        public void updateTxtProjectID(string new_text)
        {
            txtProjectID.Text = new_text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists("ContentFolderLocation.txt"))
            {
                using (StreamWriter sw = System.IO.File.CreateText("ContentFolderLocation.txt"))
                {
                    string blankFile = "";
                    sw.WriteLine(blankFile);
                }

            }

            txtMyContentFileLocation.Text = System.IO.File.ReadAllLines(@"ContentFolderLocation.txt")[0];

            if (!System.IO.File.Exists("credentials.json"))
            {
                using (StreamWriter sw = System.IO.File.CreateText("credentials.json"))
                {
                    string blankJSON = "{\"installed\": {\"client_id\": \"\",\"project_id\": \"\",\"auth_uri\": \"\",\"token_uri\": \"\",\"auth_provider_x509_cert_url\": \"\",\"client_secret\": \"\",\"redirect_uris\": [ \"\", \"\"]}}";
                    sw.WriteLine(blankJSON);
                }
                
            }

            using (StreamReader r = new StreamReader("credentials.json"))
            {
                string json = r.ReadToEnd();
                RootObject credentialObject = JsonConvert.DeserializeObject<RootObject>(json);

                if (credentialObject.installed.client_id == "" ||
                    credentialObject.installed.project_id == "" ||
                    credentialObject.installed.client_secret == "")
                {
                    CredentialSetupForm credentialSetupForm = new CredentialSetupForm(this);
                    r.Close();
                    credentialSetupForm.ShowDialog();
                }
                else
                {
                    txtProjectID.Text = credentialObject.installed.project_id;
                }
            }
        }

        private void readFiles()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
            Console.Read();
        }

        private void btnReAuthenticate_Click(object sender, EventArgs e)
        {
            CredentialSetupForm credentialSetupForm = new CredentialSetupForm(this);
            credentialSetupForm.ShowDialog();
        }

        private void btnSyncFiles_Click(object sender, EventArgs e)
        {
            readFiles();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }
    }
}
