README
-----------------------------------------------------------------
**Setting Up Unity**
------------------------------------------------------------------
Go to the Unity Store and download Unity Version 6.000.3.5f1.

If you do not already have the Unity Hub, download the most recent version of the Hub as well (will not affect anything within the project). This is what you will use to access Unity itself. 

Once both are downloaded, you can open the Hub to see all the projects you will work on. You are now ready to set up the project. 

**Setting Up Project and GitHub Sync**
------------------------------------------------------------------
Go to the main GitHub Page for MildEvilMadness (branch: Final-Game), and clone the repository via SSH. 

Make a Github Account if you don’t have one already (different from GitLab!)

For the most seamless experience, Download GitHub Desktop as it is very difficult to configure the gitignore via the command line only. 

Login to your Github account on Desktop.

Make a Repository in GitHub named accordingly and set gitignore to “Unity.” There should now be a "GitHub" file in your File Explorer with a folder containing the name of the repository that you just created. Ensure Unity Project is in the right GitHub File in your File Explorer. Then Everything should show up. Commit on the bottom left hand corner. **This is the only time you will use GitHub Desktop. You should be able to link your GitHub account with your own personal Command Line in order to make changes to the repository in the future**

WARNING. There should be a gitignore file in the Home Directory of the project (There should already be one). But in case there isn’t, look up “gitignore unity file” online and copy paste that into the Home Directory. (Or find the gitignore file and copy-paste it into the right spot). This makes sure that all the temporary Unity files aren’t included when you Push, because they aren’t needed - Unity generates them automatically when you open the project. If you open Github Desktop and see something like 22000+ files need to be pushed, your gitignore is MISSING! Do NOT commit those files! 

Then, push to the branch (should show up in the middle of the screen)

To open the project in Unity itself, open the Unity Hub and hit "Add." When your file explorer opens, click on the folder of the Repository you just made. 
You can then open the project in Unity. It will take some time to load due to the game's many files. 

**GitLab Sync**
------------------------------------------------------------------
Because Unity does not Sync directly with GitLab, you will need to sync your GitHub Repository with GitLab. The restrictions of the CCI's GitLab membership do not allow GitLab repositories to read GitHub repositories directly, so you will have to do it manually using the below sync.yml file. 

Details on setting up Sync with GitLab:

Syncing with GitLab:
Create personal access token in GITLAB (must have write_repository scope)
Go to GitHub
Go To Secrets and create Repository Secret titled GITLAB_TOKEN 

Create this yaml file with proper adjustment at the REMOTE part titled sync.yml exactly and add to main (home) part of the GitHub repo

When naming the file - paste this with the following file path: .github/workflows/sync.yml: 

```yaml
name: Sync to GitLab
on: [push, delete]
jobs:
  gitlab-sync:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
        with:
          fetch-depth: 0
      - uses: yesolutions/mirror-action@master
        with:
          REMOTE: "https://oauth2:${{ secrets.GITLAB_TOKEN }} @gitlab.cci.drexel.edu/sr3745/NewGameTest.git"
          GIT_PUSH_ARGS: "--tags --force --prune"
```

Now Commit and everything should update. 

Note that your personal access token will likely expire eventually, so to fix the problem, follow the exact same steps - create a new Personal access token with write_repository, update the Secrets in GitHub by adding this in (it will ask for your password), and then re-commit sync.yml by just going to the file, opening it, making some insignificant change (like by adding a space) and then re-committing. To avoid doing this frequently, ensure that when you make the token, pick a date as far off from the current date so you do not have to continuously update the sync.yml file. 

**Problems With Version Control: Scene Merge Conflicts**
------------------------------------------------------------------
If you are ever making changes to a project  and Unity happens to crash before you get the chance to save, it is likely that when you attempt to re-open the project, Unity will ask you if you want to Recover the changes you made. 

While you can choose to do this if you want, note that it will cause problems if you attempt to pull files later on / merge changes, etc. Because within Unity you are not only making changes to the program but also to the Scenes, Scene Conflicts are a lot more difficult to resolve and the  exact course of action is very specific to the changes you have made. Unless you have made major changes, it is often better to not retrieve your unsaved changes and instead manually re-do them within the scene to avoid Scene Merge Conflicts. 

**Problems With Unity: DPC Watchdog Violation**
------------------------------------------------------------------
If you are ever making changes to a project and Unity happens to crash, either on its own or even crashing your entire system with the words "DPC Watchdog Violation" showing, it is likely that you have corrupted files on your device. These do not affect most other Unity versions but affect the version of Unity this project runs on (6.000.3.5f1) and have observed to occur specifically on a Surface Pro 11 running Windows specifically, so you will need to appropriately fix your corrupted files. 

On Windows, type 
```
cmd
```
to open the Command Prompt. Click "Run As Administrator" and enter your system password. 
Next, type 
```
DISM.exe /Online /Cleanup-image /Restorehealth
```
in the Command Prompt. A process to fix your files will now begin, and it will likely take several minutes. Do not close the Command Prompt before the process has finished. 
Finally, to really ensure no more corrupted files will cause problems, type 
```
sfc /scannow
```
and wait for the process to finish. 
You should no longer have any issues with the DPC Watchdog Violation and Unity crashing. 
