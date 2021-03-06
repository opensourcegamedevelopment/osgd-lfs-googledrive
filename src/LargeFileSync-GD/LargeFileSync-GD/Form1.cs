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

        private UserCredential credential;
        private DriveService service;
        
        long? fileSize;
        long? byteDownloaded;
        long totalByteDownloaded;

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
            client_secret = new_text;
        }
        public void updateTxtContentFolderLocation(string new_text)
        {
            txtMyContentFileLocation.Text = new_text;
            ContentFolderLocation = new_text;
        }
        public void updateTxtProjectName(string new_text)
        {
            txtProjectName.Text = new_text;
            ProjectName = new_text;
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
#if (RELEASE)
            btnGenerateMetaData.Visible = false;
            btnSyncFiles.Height = 147;
#endif
            

            if (!System.IO.File.Exists("data/ContentFolderLocation.txt"))
            {
                FileInfo file = new FileInfo("data/ContentFolderLocation.txt");
                file.Directory.Create();

                using (StreamWriter sw = System.IO.File.CreateText("data/ContentFolderLocation.txt"))
                {
                    string blankFile = "";
                    sw.WriteLine(blankFile);
                }

            }
            else
            {
                using (StreamReader r = new StreamReader("data/ContentFolderLocation.txt"))
                {
                    ContentFolderLocation = r.ReadLine();
                    txtProjectName.Text = ContentFolderLocation;
                }
            }

            if (!System.IO.File.Exists("data/ProjectName.txt"))
            {
                using (StreamWriter sw = System.IO.File.CreateText("data/ProjectName.txt"))
                {
                    string blankFile = "";
                    sw.WriteLine(blankFile);
                }

            }
            else
            {
                using (StreamReader r = new StreamReader("data/ProjectName.txt"))
                {
                    ProjectName = r.ReadLine();
                    txtProjectName.Text = ProjectName;
                }
            }

            txtMyContentFileLocation.Text = System.IO.File.ReadAllLines(@"data/ContentFolderLocation.txt")[0];

            if (!System.IO.File.Exists("data/credentials.json"))
            {
                using (StreamWriter sw = System.IO.File.CreateText("data/credentials.json"))
                {
                    string blankJSON = "{\"installed\": {\"client_id\": \"\",\"project_id\": \"\",\"auth_uri\": \"\",\"token_uri\": \"\",\"auth_provider_x509_cert_url\": \"\",\"client_secret\": \"\",\"redirect_uris\": [ \"\", \"\"]}}";
                    sw.WriteLine(blankJSON);
                }
                
            }
            else
            {
                using (StreamReader r = new StreamReader("data/ContentFolderLocation.txt"))
                {
                    ContentFolderLocation = r.ReadLine();
                }
            }

            using (StreamReader r = new StreamReader("data/credentials.json"))
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

            backgroundWorker1.RunWorkerAsync();
        }

        private void authenticate()
        {
            using (var stream =
                new FileStream("data/credentials.json", FileMode.Open, FileAccess.Read))
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
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private void readGoogleDriveFiles()
        {
            string pageToken = null;
            bool foundProjectFolder = false;
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

                        if (fileName == ProjectName)
                        {
                            foundProjectFolder = true;
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

            if (!foundProjectFolder)
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

        private void syncData()
        {
            //readGoogleDriveFiles();
            //readLocalFiles();

            //get metadata
            string timeStampFileName = getLatestTimeStampFileName();

            //Delete Removed Files
            string LargeDataFolderLocation = txtMyContentFileLocation.Text + "\\LargeData";
            using (StreamReader reader = new StreamReader(txtMyContentFileLocation.Text + "\\LFS\\timestamps\\" + timeStampFileName + ".json"))
            {
                string data = reader.ReadToEnd();
                TimeStamp metaDataFiles = JsonConvert.DeserializeObject<TimeStamp>(data);

                string[] LocalFilesArray = Directory.GetFiles(LargeDataFolderLocation, "*", SearchOption.AllDirectories);

                //Console.WriteLine("\nLocal Files:");

                //loop through current directory for each file check if exist in metadata, if not, delete
                OutputArea.Text += "Searching for deleted files\n\n";
                foreach (string LocalFile in LocalFilesArray)
                {
                    bool fileFound = false;
                    string LocalFileRelativePath = LocalFile.Substring(LargeDataFolderLocation.Length);
                    
                    foreach (Data metaDataFile in metaDataFiles.data)
                    {
                        if (metaDataFile.filePath == LocalFileRelativePath)
                        {
                            //Console.WriteLine("Found: " + relativePath);
                            fileFound = true;
                        }
                    }

                    if (!fileFound)
                    {
                        System.IO.File.Delete(LocalFile);

                        OutputArea.Text += "Local File Deleted: " + LocalFile + "\n";
                    }
                }
            }

            
            //loop through each directory delete folder if empty
            cleanUpEmptyFolders(LargeDataFolderLocation);

            //add all new assets
            syncNewFiles(LargeDataFolderLocation, timeStampFileName);

            //Done
            OutputArea.Text += "\nDone\n";
            
        }



        private void syncNewFiles(string LargeDataFolderLocation, string timeStampFileName)
        {
            using (StreamReader reader = new StreamReader(txtMyContentFileLocation.Text + "\\LFS\\timestamps\\" + timeStampFileName + ".json"))
            {
                string data = reader.ReadToEnd();
                TimeStamp metaDataFiles = JsonConvert.DeserializeObject<TimeStamp>(data);

                string[] LocalFilesArray = Directory.GetFiles(LargeDataFolderLocation, "*", SearchOption.AllDirectories);


                //loop through metadata and check if current directory contain the file. If not, Download
                OutputArea.Text += "Searching for files to download\n\n";

                int filesDownloaded = 0;
                foreach (Data metaDataFile in metaDataFiles.data)
                {
                    bool fileFound = false;
                    foreach (string LocalFile in LocalFilesArray)
                    {
                        string LocalFileRelativePath = LocalFile.Substring(LargeDataFolderLocation.Length);

                        if (metaDataFile.filePath == LocalFileRelativePath)
                        {
                            fileFound = true;
                        }
                    }

                    if (!fileFound)
                    {
                        OutputArea.Text += "File Not Found: " + metaDataFile.filePath + "\n";
                        //download file
                        string saveToLocation = LargeDataFolderLocation + metaDataFile.filePath;

                        FileInfo file = new FileInfo(saveToLocation);
                        
                        file.Directory.Create();

                        OutputArea.Text += "File will be Save to: " + saveToLocation + "\n";
                        getFileFromGDrive(metaDataFile.fileName);
                        Google.Apis.Drive.v3.Data.File gFile = getFileFromGDrive(metaDataFile.fileName);
                        downloadFile(service, gFile, saveToLocation);
                        filesDownloaded++;
                    }
                }

                if (filesDownloaded == 0)
                {
                    OutputArea.Text += "No New Files needed to download \n";
                }
                else
                {
                    OutputArea.Text += "Total Files Downloaded: " + filesDownloaded + "\n";
                }
            }
        }
        

        private Google.Apis.Drive.v3.Data.File getFileFromGDrive(string fileName)
        {
            //get metadata
            string timeStampFileName = getLatestTimeStampFileName();
            OutputArea.Text += "Google timeStampFileName: " + timeStampFileName + "\n";

            string fileId = "";
            using (StreamReader reader = new StreamReader(txtMyContentFileLocation.Text + "\\LFS\\timestamps\\" + timeStampFileName + ".json"))
            {
                string data = reader.ReadToEnd();
                TimeStamp metaDataFiles = JsonConvert.DeserializeObject<TimeStamp>(data);

                foreach (Data item in metaDataFiles.data)
                {
                    if (item.fileName == fileName)
                    {
                        fileId = item.fileId;
                        OutputArea.Text += "Google fileName: " + item.fileName + "\n";
                        OutputArea.Text += "Google File ID: " + item.fileId + "\n";
                        break;
                    }
                }
            }


            authenticate();
            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            FilesResource.GetRequest getRequest = service.Files.Get(fileId);

            // List files.
            var result = getRequest.Execute();
            Google.Apis.Drive.v3.Data.File file = result;

            OutputArea.Text += "Google File: " + file.Name + "\n\n";

            return file;
        }

        private void downloadFile(DriveService service, Google.Apis.Drive.v3.Data.File file, string saveTo)
        {
            var request = service.Files.Get(file.Id);
            request.Fields = "size"; 
            var stream = new MemoryStream();
            

            var result = request.Execute();
            fileSize = result.Size;
            Console.WriteLine("total file size: " + fileSize);
            totalByteDownloaded = 0;


            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case Google.Apis.Download.DownloadStatus.Downloading:
                        {
                            byteDownloaded = progress.BytesDownloaded;
                            totalByteDownloaded += progress.BytesDownloaded;

                            double roundedProgress = Math.Round((totalByteDownloaded / (double)fileSize),3);
                            int progressInt = Convert.ToInt32(roundedProgress * 100) ;
                            setProgress(progressInt);
                            Console.WriteLine(progress.BytesDownloaded);
                            Console.WriteLine(progressInt);
                            
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            SaveStream(stream, saveTo);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.DownloadAsync(stream);
            
        }
        

        public void setProgress(int value)
        {
            if (value <= 100)
            {
                DownloadProgressBar.Invoke(new Action(() => DownloadProgressBar.Value = value));
                LblDownloadProgress.Invoke(new Action(() => LblDownloadProgress.Text = totalByteDownloaded + " kb / " + fileSize + "kB"));
            }
        }
        

        private void SaveStream(MemoryStream stream, string saveTo)
        {
            using (FileStream file = new FileStream(saveTo, FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(file);
            }
        }

        private void cleanUpEmptyFolders(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                cleanUpEmptyFolders(directory);
                if (Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                    OutputArea.Text += "Empty Directory Deleted: " + directory + "\n";
                }
            }
        }

        private void createLocalFilesTimeStampData()
        {
            string timeStamp = DateTime.Now.ToString("yyyyMMddThhmm");
            //Console.WriteLine(timeStamp);

            FileInfo file = new FileInfo(txtMyContentFileLocation.Text + "\\LFS\\timestamps\\" + timeStamp + ".json");

            //Console.WriteLine(file);

            file.Directory.Create();

            using (StreamWriter writer = new StreamWriter(txtMyContentFileLocation.Text + "\\LFS\\timestamps\\" + timeStamp + ".json"))
            {
                TimeStamp metadata = new TimeStamp();

                string LargeDataFolderLocation = txtMyContentFileLocation.Text + "\\LargeData";

                string[] localFileArray = Directory.GetFiles(LargeDataFolderLocation, "*", SearchOption.AllDirectories);

                string currentDir = Directory.GetCurrentDirectory();

                foreach (string localFilePath in localFileArray)
                {
                    string localFileName = System.IO.Path.GetFileName(localFilePath);
                    string localFileRelativePath = localFilePath.Substring(LargeDataFolderLocation.Length);

                    Data data = new Data();
                    data.fileName = localFileName;
                    data.filePath = localFileRelativePath;
                    metadata.data.Add(data);
                }

                string newJson = JsonConvert.SerializeObject(metadata);
                writer.Write(newJson);
            }

            string newTimeStampFile = txtMyContentFileLocation.Text + "\\LFS\\timestamps\\" + timeStamp + ".json";
            string oldTimeStampFileName = "";

            if (System.IO.File.Exists(txtMyContentFileLocation.Text + "\\LFS\\metadata.json"))
            {

                using (StreamReader reader = new StreamReader(txtMyContentFileLocation.Text + "\\LFS\\metadata.json"))
                {
                    string data = reader.ReadToEnd();
                    Metadata metadata = JsonConvert.DeserializeObject<Metadata>(data);
                    oldTimeStampFileName = metadata.currentVersion;
                    metadata.currentVersion = newTimeStampFile;
                }

                OutputArea.Text += "oldTimeStampFileName: " + oldTimeStampFileName + "\n";
                OutputArea.Text += "newTimeStampFile: " + oldTimeStampFileName + "\n";

                string oldTimeStampFile = txtMyContentFileLocation.Text + "\\LFS\\timestamps\\" + oldTimeStampFileName + ".json";


                if (FileEquals(newTimeStampFile, oldTimeStampFile))//check if the new file actually contain new data
                {
                    //if not delete the newly generated timestamp file
                    System.IO.File.Delete(newTimeStampFile);
                    MessageBox.Show("No New data");
                }
                else
                {
                    updateMetaData(timeStamp);
                    updateFileID();
                    string msg = "New timestamp.json added: " + System.IO.Path.GetFileName(newTimeStampFile) + ".json \n";
                    msg = "metadata.json file updated";
                    MessageBox.Show(msg);
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(txtMyContentFileLocation.Text + "\\LFS\\metadata.json"))
                {

                    Metadata metadata = new Metadata();
                    metadata.currentVersion = timeStamp;
                    string newJson = JsonConvert.SerializeObject(metadata);
                    writer.Write(newJson);
                }
            }
        }

        private void updateFileID()
        {
            authenticate();
            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            string pageToken = null;
            OutputArea.Text += ("Google Drive Files:");
            do
            {
                // Define parameters of request.
                // Get root folder
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.PageSize = 100;
                listRequest.Fields = "nextPageToken, files(id, name)";
                listRequest.PageToken = pageToken;

                // List files.
                var result = listRequest.Execute();
                IList<Google.Apis.Drive.v3.Data.File> files = result.Files;

                OutputArea.Text += ("Count:: " + result.Files.Count);

                if (files != null && files.Count > 0)
                {
                    //foreach file in google drive
                    foreach (var file in files)
                    {
                        OutputArea.Text += ("Files:: " + file.Name + "\n");

                        string LargeDataFolderLocation = txtMyContentFileLocation.Text + "\\LargeData";

                        string[] localFileArray = Directory.GetFiles(LargeDataFolderLocation, "*", SearchOption.AllDirectories);
                        
                        //foreach file in local
                        foreach (string localFilePath in localFileArray)
                        {
                            string localFileName = System.IO.Path.GetFileName(localFilePath);

                            //look for if there is a file in the gDrive same as file in local (eg. actualy file we need)
                            //Then record its id in metaData
                            //if match
                            if (file.Name == localFileName)
                            {
                                OutputArea.Text += ("Matched File Found:: " + file.Name + "\n");
                                OutputArea.Text += ("Save ID:: " + file.Id + "\n");

                                //get metadata
                                string timeStampFileName = getLatestTimeStampFileName();
                                
                                TimeStamp metaDataFiles;
                                bool updateData = false;
                                using (StreamReader reader = new StreamReader(txtMyContentFileLocation.Text + "\\LFS\\timestamps\\" + timeStampFileName + ".json"))
                                {
                                    string data = reader.ReadToEnd();
                                    metaDataFiles = JsonConvert.DeserializeObject<TimeStamp>(data);

                                    foreach(Data item in metaDataFiles.data)
                                    {
                                        if (item.fileName == localFileName)
                                        {
                                            item.fileId = file.Id;
                                            updateData = true;
                                            break;
                                        }
                                    }
                                }

                                if (updateData)
                                {
                                    using (StreamWriter writer = new StreamWriter(txtMyContentFileLocation.Text + "\\LFS\\timestamps\\" + timeStampFileName + ".json"))
                                    {

                                        string newJson = JsonConvert.SerializeObject(metaDataFiles);
                                        writer.Write(newJson);
                                    }
                                }

                            }
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("No files found.");
                }
                pageToken = result.NextPageToken;

                OutputArea.Text += ("\n");
            }
            while (pageToken != null);
        }

        private string getLatestTimeStampFileName()
        {
            string timeStampFileName = "";
            using (StreamReader reader = new StreamReader(txtMyContentFileLocation.Text + "\\LFS\\metadata.json"))
            {
                string data = reader.ReadToEnd();
                Metadata metadata = JsonConvert.DeserializeObject<Metadata>(data);
                timeStampFileName = metadata.currentVersion;
            }

            return timeStampFileName;
        }

        private void updateMetaData(string timeStamp)
        {
            FileInfo file = new FileInfo(txtMyContentFileLocation.Text + "\\LFS\\metadata.json");
            file.Directory.Create();
            using (StreamWriter writer = new StreamWriter(txtMyContentFileLocation.Text + "\\LFS\\metadata.json"))
            {
                Metadata metadata = new Metadata();
                metadata.currentVersion = timeStamp;
                string newJson = JsonConvert.SerializeObject(metadata);
                writer.Write(newJson);
            }
        }

        private void btnGenerateMetaData_Click(object sender, EventArgs e)
        {
#if (DEBUG || ADMIN)
            createLocalFilesTimeStampData();
#else
            MessageBox.Show("Available for Debug Mode Only - For ppl that know what this is doing!");
#endif

        }

        static bool FileEquals(string path1, string path2)
        {
            byte[] file1 = System.IO.File.ReadAllBytes(path1);
            byte[] file2 = System.IO.File.ReadAllBytes(path2);
            if (file1.Length == file2.Length)
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    if (file1[i] != file2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
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
                OutputArea.Text = "";
                syncData();
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
