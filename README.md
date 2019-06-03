# osgd-lfs-googledrive
Custom Large File Sync for Google Drive

# About
This application is created for projectMMW (https://github.com/opensourcegamedevelopment/ProjectMMW-UE) to sync Large file assets from google drive. It is Open-Source and under MIT license so feel free to remix for you own uses. 

## Guide Line:
Project Manager/Admin: Use Admin.rar version
Project Members: User Release.rar Version

## Configuration (For project Members)
1. Open LargeFileSync-GD.exe

![Image of step1](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/step1.PNG)

2.On first time loading a popup for CredentialSetup will appear, simply click on the button to load the credentials.json file provided by your Project Admin. (This is the credential file for google drive).
![Image of step2](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/step2.PNG)

3. When loading for the first time, you will also need to setup projectName as well as content file location. Simple click on the settings button and add the corresponding information.
![Image of step3](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/step3.PNG)

4.Content file location is the path to the content directory of the Project's Content folder (For UE4). If you are using and working on a unity3D project you can do the same to navigate to the asset folder. 

5. Project Name is the name of the root project folder in the share google drive which is used to store the projects's large data asset files. 

6. Once done (if the project is configured correctly), simply click on Sync Content files button to automatically download the required content files. 

#### Note: This will only work for project that is setup correctly to uses lfs, for more information or if you have any issues contact me on our Discord channel at: https://discord.gg/eE4vjFN

## Syncing workflow
Once setup completed, for future workflow. Simply switch project branch to the branch you are working on. Then open this lfs-gd app and click sync content files.

## Authorisation
The first time you run the app, it will prompt you to authorize access to your google account:

1. The app will attempt to open a new window or tab in your default browser.

2. If you are not already logged into your Google account, you will be prompted to log in. If you are logged into multiple Google accounts, you will be asked to select one account to use for the authorization.

3. Click the Accept button.
The app will proceed automatically, and you may close the window/tab.

#### Note: you will need to accept and hit the the share folder provided to you by project admin atleast once for this syncing to work. 
#### Note2: Because of the way google drive folder structure works. All files use for this syncing mechanic MUST have unique filenames.

# Configuration (For project Admin)

## First Time configuration (Getting Credentials.json)
Follow the steps below if you are a project admin using this for your own project. If there are any issues contact me on our Discord channel at: https://discord.gg/eE4vjFN 

1. First you need to register for a google account to store the Large Files. This can be any basic account. Google offer 15GB for free storage. (I would recommend to create a seperate account for the project only as we are going to share the credential file for other member of the project to use, however, personal account will also work fine.)  
https://one.google.com/storage

2. Next you will need to login to the google account and click on the enable drive api button below:
https://developers.google.com/drive/api/v3/quickstart/dotnet
There should be instrcution on the process including choosing a project/app name. Once that is done you should be albe to download the credentail file for the app. (Make sure you store this in a safe place.)
![Image of credentials](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/credentials.PNG)

## Adding New Large Asset Data Files (Project Admin Use only)
Adding new Large Asset Data files should be done by Project Admin only (For those that know what they are doing) as to prevent breaking the whole syncing process. I had setup so that this can only be done using the debug version of this app to prevent users accidently breaking the syncing.

The step are actually quite simple:

1. Downlaod/Clone the latest Tagged Release version's Source Code (To ensure you are working on a stable version).
2. Upload the New Large assets file(s) (eg. New 3D models) to your google drive share folder (I am assuming you are the project admin and have permission to upload to the shared google drive folder). 
3. Open/Load this app using visual studio and switch to debug version. 
![Image of step1](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/upload-step1.PNG)

4. When running the app in debug version, you should be able to see a new button "GenerateMetaData". All you have to do is click on this button after new files are uploaded to google drive in the correct folder. New timestamp.json file will be generate in the ue4 project's content/lfs/timestamp folder and the metadata.json file in the content/lfs folder should be updated to point to this new timestamp file.
![Image of step2](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/upload-step2.PNG)

5. Finally, commit and push the ue4 project's updates (It is recommend to do this in its own commit rahter than along with project's other code changes.)

## Project Admin - Setup Instruction summary
#### First-time setup (For project Admin):
1. Create google account
2. Activate Drive API and download credentials.json

#### Setup for Project Admin
1. Download visual studio
2. Download source code from github.
3. Open project in visual studio and Switch to debug mode then compile/run the app. 
(I will write a separate app for admin later when I have time so that the above 3 steps wont be necessary)
4. load in the credentials.json. (If popup don't appear, you can click on the Re-authenticate button.)
5.  Click on the Setting button to enter in your settings. 

Content file location is the path to the content directory of the Project's Content folder (For UE4). If you are using and working on a unity3D project you can do the same to navigate to the asset folder. This App will create a Folder called LargeData under the Content Folder where all your large data to be synced to google drive should be stored in.
(eg. for my setup I have D:\user\ProjectMMW-UE\Content\LargeData while I have other data in the content file that are sync directly on github - such as unreal's blueprints and map files [.uasset files] D:\user\ProjectMMW-UE\Content\Blueprints) 

Project Name is the name of the root project folder in the share google drive which is used to store the projects's large data asset files. 

6. Add your large data files in the content folder (you should already had them added as this is the local folder of your asset file for your unity/unreal project.)

7. Upload the content files into your google drive folder. You will have to share this folder with your project members, this folder should also be the same name as the project Name you had entered in step 5 (recommend no spaces).
I would also recommend to upload all files to the google drive in this default root folder rather than sub-folders and google drive api doesn't treat sub-folders in a folder structure. (eg. ProjectMMW/rifle.uasset instead of ProjectMMW/3DModels/Weapons/rifle.uasset)

Note: Due to the way google drive api sync data, project member need to access each shared folder because google drive api can access content from them. 
Eg. If the shared conent folder conatins ProjectMMW/3DModels/Weapons/rifle.uasset and ProjectMMW/3DModels/Armors/plateArmor.uasset 
Project member need to open/view those 2 folders in the google drive webbrowser before the api can assess the two files (rifle.uasset and plateArmor.uasset). Thus I would recommend to place them all in the root folder ProjectMMW/rifle.uasset and ProjectMMW/plateArmor.uasset so that project member only need to view ProjectMMW once to ensure all data is sync-able.  

8. Click on the GenerateMetaData button, this will create the LFS folder along with the timestamp files inside the content folder. 

9. You will then need to commit/push this LFS folder to your github project repo.

10. Once this is done, your project members only need to click that big sync content files button to download/sync all the required Large binary files from google drive directly on the project's content folder!


# How it work
This App works by syncing files in the share google drive folder/files with the corresponding folders/files in your local ue4 project.
It works as below:
1. Lookup the metadata.json file in the ue4's content/lfs folder to find the corresponding timestamp.json file for the current active branch. The metadata.json file will only contain reference to it's current active timestamp.json file.
![Image of step1](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/about-step1.PNG)

![Image of step2](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/about-step2.PNG)

2. Lookup the corresponding timestamp.json file in the ue4's content/lfs folder. 
![Image of step3](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/about-step3.PNG)

timestamp.json file will contain a list of filesname (related to the unique filename in google drive) and it's corresponding path (related to local file's relative path)
![Image of step4](https://raw.githubusercontent.com/opensourcegamedevelopment/osgd-lfs-googledrive/master/images/about-step4.PNG)

3. Search corresponding files in google drive and download new files if not exist in the current local folder. (Will remove first then download new).

# remix project
If you are remixing this project for your own, make sure you update credentials.json with your own app.
