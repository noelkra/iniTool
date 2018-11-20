using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace iniTool
{
    class FileHandler
    {
        //Declaration
        private List<Content> contentList;
        private RessourceEdit resEdit;
        private ArrayList incorrectFiles;
        private string correctRootSpecsDir;
        private string correctRootModulesDir;
        private string correctModulesIniFile;
        private ArrayList pathToFile;
        private string tempProjectName, tempProjectID, tempProjectGUID, tempPWProject, tempPWProjectGUID, tempRootSpecsDir, tempRootModulesDir, tempModulesIniFile;
        private bool tempIsChecked;

        //Constructor
        public FileHandler()
        {
            resEdit = new RessourceEdit();
            incorrectFiles = new ArrayList();
            contentList = new List<Content>();
            pathToFile = new ArrayList();
        }
        /// <summary>
        /// Get content form chosen directory.
        /// </summary>
        /// <PARAM name="dir"></PARAM>
        /// Path to Directory
        public void LoadFileContent(string dir) //TODO shorten and split into multiple methods
        {
            correctRootSpecsDir = resEdit.GetRootSpecsDir();
            correctRootModulesDir = resEdit.GetRootModulesDir();
            correctModulesIniFile = resEdit.GetModulesIniFile();
            contentList.Clear();
            //Variables for temporary saved content from the .ini files

            //Variables for the correct Values
            string[] fileArray = null;
            //Get all Directories
            dir += @"\";
            try
            {
                fileArray = Directory.GetDirectories(dir);
            }
            catch (DirectoryNotFoundException dnfEX)
            {
                MessageBox.Show("Directory could not be found! Error: " + dnfEX, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Get correct values
            if (fileArray != null)
            {
                SetCorrectFiles(fileArray, dir);
            }
        }
        private void SetCorrectFiles(string[] fileArray, string dir)
        {
            string prefix = resEdit.GetPrefix();
            foreach (string filepath in fileArray)
            {

                if (filepath.Substring(dir.Length, prefix.Length) == (prefix)) //TODO error here
                {
                    IniHandler projIniHandler = new IniHandler(filepath + @"\project.ini");
                    IniHandler configIniHandler = new IniHandler(filepath + @"\Config\config.ini");

                    //get content form project.ini
                    tempProjectName = projIniHandler.IniReadValue("GENERAL", "PROJECTNAME");
                    tempProjectID = projIniHandler.IniReadValue("GENERAL", "PROJECTID");
                    tempProjectGUID = projIniHandler.IniReadValue("GENERAL", "PROJECTGUID");
                    tempPWProject = projIniHandler.IniReadValue("ProjectWise", "PWProject");
                    tempPWProjectGUID = projIniHandler.IniReadValue("ProjectWise", "PWProjectGUID");

                    //get content from config.ini
                    tempRootSpecsDir = configIniHandler.IniReadValue("System", "Root_Specs_Dir");
                    tempRootModulesDir = configIniHandler.IniReadValue("System", "Root_Modules_Dir");
                    tempModulesIniFile = configIniHandler.IniReadValue("System", "Modules_Ini_File");

                    //Search for files
                    if (tempRootSpecsDir != this.correctRootSpecsDir || tempRootModulesDir != this.correctRootModulesDir || tempModulesIniFile != this.correctModulesIniFile) //MODULES INI FILE
                    {
                        tempIsChecked = true;
                        incorrectFiles.Add(filepath + @"\Config\config.ini");
                    }
                    else { tempIsChecked = false; }

                    //Add content to contentList
                    setContentList(filepath, tempIsChecked, tempProjectName, tempProjectID, tempProjectGUID, tempPWProject, tempPWProjectGUID, tempRootSpecsDir, tempRootModulesDir, tempModulesIniFile);
                }
            }
        }

        /// <summary>
        ///     Changes the files content and therefore repairs it.
        /// </summary>
        /// Gets the correct values from the ini-File and compares the actual value from the files.
        /// If the values aren't the same, the wrong values get changed and corrected.
        public void RepairFiles()
        {
            //Get filepath for all files that need to get changed
            foreach (string filepath in incorrectFiles)
            {
                IniHandler configIniHandler = new IniHandler(filepath);
                tempRootSpecsDir = configIniHandler.IniReadValue("System", "Root_Specs_Dir");
                tempRootModulesDir = configIniHandler.IniReadValue("System", "Root_Modules_Dir");
                tempModulesIniFile = configIniHandler.IniReadValue("System", "Modules_Ini_File");
                
                //Get which variable needs to get changed

                if (tempRootSpecsDir != correctRootSpecsDir) 
                {
                    configIniHandler.IniWriteValue("System", "Root_Specs_Dir", correctRootSpecsDir);
                }
                if(tempRootModulesDir != correctRootModulesDir)
                {
                    configIniHandler.IniWriteValue("System", "Root_Modules_Dir", correctRootModulesDir);
                }
                if(tempModulesIniFile != correctModulesIniFile)
                {
                    configIniHandler.IniWriteValue("System", "Modules_Ini_File", correctModulesIniFile);
                }
            }
        }
        /// <summary>
        /// Returns the contentList
        /// </summary>
        /// <returns></returns>
        public List<Content> getFileContent()
        {
            return contentList;
        }

        /// <summary>
        /// Adds the entity to the ContentList
        /// </summary>
        private void setContentList(string directory, bool tempIsChecked, string tempProjectName, string tempProjectID, string tempProjectGUID, string tempPWProject, string tempPWProjectGUID, string tempRootSpecsDir, string tempRootModulesDir, string tempModulesIniFile)
        {
            contentList.Add(new Content()
            {
                folderPath = directory,
                isChecked = tempIsChecked,
                projectName = tempProjectName ?? "NoInformation",
                projectID = tempProjectID ?? "NoInformation",
                projectGUID = tempProjectGUID ?? "NoInformation",
                pwProject = tempPWProject ?? "NoInformation",
                pwProjectGUID = tempPWProjectGUID ?? "NoInformation",
                rootSpecsDir = tempRootSpecsDir ?? "NoInformation",
                rootModulesDir = tempRootModulesDir ?? "NoInformation",
                modulesIniFile = tempModulesIniFile ?? "NoInformation"
            });
        }
    }
}