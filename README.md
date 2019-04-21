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

Note: you will need to accept and hit the the share folder provided to you by project admin atleast once for this syncing to work. 
Note2: Because of the way google drive folder structure works. All files use for this syncing mechanic MUST have unique filenames.

# remix project
If you are remixing this project for your own, make sure you update credentials.json with your own app.
