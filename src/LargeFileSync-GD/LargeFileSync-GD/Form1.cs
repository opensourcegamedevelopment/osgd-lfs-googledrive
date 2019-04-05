﻿using System;
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

        static string client_id = "";
        static string project_id = "";
        static string client_secret = "";
        static string ContentFolderLocation = "";
        static string ProjectName = "";

        public Form1()
        {
            InitializeComponent();
        }

        #region expose fields to other forms
        public void updateTxtClientID(string new_text)
        {
            client_id = new_text;
        }
        public void updateTxtProjectID(string new_text)
        {
            txtProjectID.Text = new_text;
            project_id = new_text;
        }
        public void updateTxtClientSecret(string new_text)
        {
            ContentFolderLocation = new_text;
        }
        public void updateTxtContentFolderLocation(string new_text)
        {
            txtMyContentFileLocation.Text = new_text;
            project_id = new_text;
        }
        public void updateTxtProjectName(string new_text)
        {
            txtProjectName.Text = new_text;
            ProjectName = new_text;
        }
        #endregion

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
            else
            {
                using (StreamReader r = new StreamReader("ContentFolderLocation.txt"))
                {
                    ContentFolderLocation = r.ReadLine();
                    txtProjectName.Text = ContentFolderLocation;
                }
            }

            if (!System.IO.File.Exists("ProjectName.txt"))
            {
                using (StreamWriter sw = System.IO.File.CreateText("ProjectName.txt"))
                {
                    string blankFile = "";
                    sw.WriteLine(blankFile);
                }

            }
            else
            {
                using (StreamReader r = new StreamReader("ProjectName.txt"))
                {
                    ProjectName = r.ReadLine();
                    txtProjectName.Text = ProjectName;
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
            else
            {
                using (StreamReader r = new StreamReader("ContentFolderLocation.txt"))
                {
                    ContentFolderLocation = r.ReadLine();
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
                    client_id = credentialObject.installed.client_id;
                    project_id = credentialObject.installed.project_id;
                    client_secret = credentialObject.installed.client_secret;
                }
            }
        }
        private void readGoogleDriveFiles()
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
            
            string pageToken = null;
            bool foundPrjectFolder = false;
            Console.WriteLine("Google Drive Files:");
            do
            {
                // Define parameters of request.
                // Get root folder
                //FilesResource.ListRequest listRequest = service.Files.List();
                //listRequest.PageSize = 10;
                //listRequest.Fields = "nextPageToken, files(id, name)";
                //listRequest.PageToken = pageToken;

                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.PageSize = 100;
                listRequest.Fields = "nextPageToken, files(id, name)";
                listRequest.PageToken = pageToken;
                //listRequest.IncludeTeamDriveItems = true;
                //listRequest.SupportsTeamDrives = true;
                //listRequest.Q = "sharedWithMe=true";

                // List files.
                var result = listRequest.Execute();
                IList<Google.Apis.Drive.v3.Data.File> files = result.Files;
                
                Console.WriteLine("Count:: " + result.Files.Count);

                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        Console.WriteLine("Files:: " + file.Name);
                        string fileName = "";
                        if (file.Name.Length >= ProjectName.Length)
                        {
                            fileName = file.Name.Substring(0, ProjectName.Length);
                        }
                        //Console.WriteLine("name: " + fileName);

                       // Console.WriteLine("{0} ({1})", file.Name, file.Id);

                        if (fileName == ProjectName)
                        {
                            foundPrjectFolder = true;
                            Console.WriteLine("{0} ({1})", file.Name, file.Id);

                        }
                    }
                }
                else
                {
                    MessageBox.Show("No files found.");
                }
                pageToken = result.NextPageToken;

                Console.WriteLine("\n");
            }
            while (pageToken != null);

            if (!foundPrjectFolder)
            {
                MessageBox.Show("No Project Folder found. \nDid you clicked on the share folder link and recieved the share folder?");
            }
        }

        private void readLocalFiles()
        {
            string LargeDataFolderLocation = txtMyContentFileLocation.Text + "\\LargeData";
            Console.WriteLine(LargeDataFolderLocation);

            string[] fileArray = Directory.GetFiles(LargeDataFolderLocation, "*", SearchOption.AllDirectories);

            Console.WriteLine("\nLocal Files:");
            foreach (string fileName in fileArray)
            {
                Console.WriteLine(fileName);
            }
        }

        private void createLocalFilesMetaData()
        {
            System.IO.FileInfo file = new System.IO.FileInfo("metadata\\important.txt");
            file.Directory.Create();

            using (StreamWriter writer = new StreamWriter("metadata\\important.txt"))
            {
                Metadata metadata = new Metadata();

                string LargeDataFolderLocation = txtMyContentFileLocation.Text + "\\LargeData";

                string[] fileArray = Directory.GetFiles(LargeDataFolderLocation, "*", SearchOption.AllDirectories);
                
                foreach (string fileName in fileArray)
                {
                    Console.WriteLine(fileName);

                    Data data = new Data();
                    data.fileName = fileName;
                    data.filePath = fileName;
                    metadata.data.Add(data);

                    //writer.WriteLine(fileName);
                }
               

                string newJson = JsonConvert.SerializeObject(metadata);
                writer.Write(newJson);

            }
        }

        #region buttonClicks()
        private void btnReAuthenticate_Click(object sender, EventArgs e)
        {
            CredentialSetupForm credentialSetupForm = new CredentialSetupForm(this);
            credentialSetupForm.ShowDialog();
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(this);
            settingsForm.ShowDialog();
        }
        private void btnSyncFiles_Click(object sender, EventArgs e)
        {
            if (correctSettings())
            {
                readGoogleDriveFiles();
                readLocalFiles();
                createLocalFilesMetaData();
            }
        }
        private bool correctSettings()
        {
            if (client_id == "")
            {
                MessageBox.Show("Missing client_id");
                return false;
            }
            else if (project_id == "")
            {
                MessageBox.Show("Missing project_id");
                return false;
            }
            else if (client_secret == "")
            {
                MessageBox.Show("Missing client_secret");
                return false;
            }
            else if (ContentFolderLocation == "")
            {
                MessageBox.Show("Missing ContentFolderLocation");
                return false;
            }
            else if (ProjectName == "")
            {
                MessageBox.Show("Missing ProjectName");
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
