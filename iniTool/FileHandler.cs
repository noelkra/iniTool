using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace iniTool
{
    class FileHandler
    {
        //Declaration
        public static List<Content> contentList = new List<Content>();
        private CustomRessourceEdit resEdit = new CustomRessourceEdit();
        private ArrayList incorrectFiles = new ArrayList();

        /// <summary>
        /// Get content form chosen directory.
        /// </summary>
        /// <PARAM name="dir"></PARAM>
        /// Path to Directory
        public List<Content> GetContentFromFiles(string dir) //TODO shorten and split into multiple methods
        {
            contentList.Clear();
            //Variables for temporary saved content from the .ini files
            string tempProjectName, tempProjectID, tempProjectGUID, tempPWProject, tempPWProjectGUID, tempRootSpecsDir, tempRootModulesDir, tempModulesIniFile;
            bool tempIsChecked;
            int increment = 0;

            //Variables for the correct Values
            string[] fileArray = null;
            //Get all Directories
            dir += @"\";
            try
            {
                fileArray = Directory.GetDirectories(dir);
            }
            catch(DirectoryNotFoundException dnfEX)
            {
                MessageBox.Show("Directory could not be found! Error: " + dnfEX, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Get correct values
            if (fileArray != null)
            {
                foreach (string directory in fileArray)
                {
                    if (directory.Substring(dir.Length, 4) == ("Proj")) //TODO add support for folders with different names and folders missing files
                    {
                        increment++;
                        IniHandler projIniHandler = new IniHandler(directory + @"\project.ini");
                        IniHandler configIniHandler = new IniHandler(directory + @"\Config\config.ini");

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
                        if (tempRootSpecsDir != resEdit.GetRootSpecsDir() || tempRootModulesDir != resEdit.GetRootModulesDir() || tempModulesIniFile != resEdit.GetModulesIniFile()) //MODULES INI FILE
                        {
                            tempIsChecked = true;
                            incorrectFiles.Add(directory + @"\Config\config.ini");
                        }
                        else { tempIsChecked = false; }


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
                        //Add content to contentList
                        //setContentList(directory, tempIsChecked, tempProjectName, tempProjectID, tempProjectGUID, tempPWProject, tempPWProjectGUID, tempRootSpecsDir, tempRootModulesDir, tempModulesIniFile);
                    }
                }
            }
            return contentList;
        }
        /// <summary>
        /// Changes the files content and therefore repairs it.
        /// </summary>
        public void RepairFiles()
        {
            //TODO many things same as in GetContentFromFiles (Maybe combine to new Method?)
            string tempRootSpecsDir, tempRootModulesDir, tempModulesIniFile;

            string correctRootSpecsDir = resEdit.GetRootSpecsDir();
            string correctRootModulesDir = resEdit.GetRootModulesDir();
            string correctModulesIniFile = resEdit.GetModulesIniFile();
            //Get path for all files that need to get changed
            foreach (string path in incorrectFiles)
            {
                IniHandler configIniHandler = new IniHandler(path);
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
        /// Gets the checked entries in the DataGrid and returns them
        /// </summary>
        private void getIsChecked()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the contentList
        /// </summary>
        /// <returns></returns>
        public List<Content> getContentList()
        {
            return contentList; //TODO remove just for testing
        }

        //TODO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// <summary>
        /// Adds the entity to the ContentList
        /// </summary>
        public void setContentList(string directory, bool tempIsChecked, string tempProjectName, string tempProjectID, string tempProjectGUID, string tempPWProject, string tempPWProjectGUID, string tempRootSpecsDir, string tempRootModulesDir, string tempModulesIniFile)
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