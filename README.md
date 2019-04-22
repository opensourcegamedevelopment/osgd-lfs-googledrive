# osgd-lfs-googledrive
Custom Large File Sync for Google Drive

# About
This application is created for projectMMW (https://github.com/opensourcegamedevelopment/ProjectMMW-UE) to sync Large file assets from google drive. It is Open-Source and under MIT license so feel free to remix for you own uses. 

# configuration
1. Open LargeFileSync-GD.exe

![Image of step1](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/step1.PNG)

2.On first time loading a popup for CredentialSetup will appear, simply click on the button to load the credentials.json file provided by your Project Admin. (This is the credential file for google drive).
![Image of step2](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/step2.PNG)

3. When loading for the first time, you will also need to setup projectName as well as content file location. Simple click on the settings button and add the corresponding information.
![Image of step3](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/step3.PNG)

4.Content file location is the path to the content directory of the Project's Content folder (For UE4). If you are using and working on a unity3D project you can do the same to navigate to the asset folder. 

5. Project Name is the name of the root project folder in the share google drive which is used to store the projects's large data asset files. 

6. Once done (if the project is configured correctly), simply click on Sync Content files button to automatically download the required content files. 

#### Note: This will only work for project that is setup correctly to uses lfs, for more information contact me on zerokiri or on our Discord channel at: https://discord.gg/A2tnTSH

# Syncing workflow
Once setup completed, for future workflow. Simply switch project branch to the branch you are working on. Then open this lfs-gd app and click sync content files.

# Authorisation
The first time you run the app, it will prompt you to authorize access to your google account:

1. The app will attempt to open a new window or tab in your default browser.

2. If you are not already logged into your Google account, you will be prompted to log in. If you are logged into multiple Google accounts, you will be asked to select one account to use for the authorization.

3. Click the Accept button.
The app will proceed automatically, and you may close the window/tab.

#### Note: you will need to accept and hit the the share folder provided to you by project admin atleast once for this syncing to work. 
#### Note2: Because of the way google drive folder structure works. All files use for this syncing mechanic MUST have unique filenames.

# Adding New Large Asset Data Files (Project Admin Use only)
Adding new Large Asset Data files should be done by Project Admin only (For those that know what they are doing) as to prevent breaking the whole syncing process. I had setup so that this can only be done using the debug version of this app try prevent users accidently breaking the syncing.

The step are actually quite simple:

1. Downlaod/Clone the latest Tagged Release version (To ensure you are working on a stable version).
2. Upload the New Large assets file(s) (eg. New 3D models) to your google drive share folder (I am assuming you are the project admin and have permission to upload to the shared google drive folder). 
3. Open/Load this app using visual studio and switch to debug version. 
![Image of step1](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/upload-step1.PNG)

4. When running the app in debug version, you should be able to see a new button "GenerateMetaData". All you have to do is click on this button after new files are uploaded to google drive in the correct folder. New timestamp.json file will be generate in the ue4 project's content/lfs/timestamp folder and the metadata.json file in the content/lfs folder should be updated to point to this new timestamp file.
![Image of step2](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/upload-step2.PNG)

5. Finally, commit and push the ue4 project's updates (It is recommend to do this in its own commit rahter than along with project's other code changes.)

# How it work


# remix project
If you are remixing this project for your own, make sure you update credentials.json with your own app.
