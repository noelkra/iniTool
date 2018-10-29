using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace iniTool
{
    class FileHandler
    {
        //Declaration
        private List<Content> contentList = new List<Content>();
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
            string correctRootSpecsDir, correctRootModulesDir, correctModulesIniFile;
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
            ArrayList pathToFile = new ArrayList();

            //Get correct values
            correctRootSpecsDir = resEdit.GetRootSpecsDir();
            correctRootModulesDir = resEdit.GetRootModulesDir();
            correctModulesIniFile = resEdit.GetModulesIniFile();
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
                        if (tempRootSpecsDir != correctRootSpecsDir || tempRootModulesDir != correctRootModulesDir || tempModulesIniFile != correctModulesIniFile) //MODULES INI FILE
                        {
                            tempIsChecked = true;
                            incorrectFiles.Add(directory + @"\Config\config.ini");
                        }
                        else { tempIsChecked = false; }

                        //Add content to contentList
                        contentList.Add(new Content()
                        {
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
            return contentList;
        }
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
                //Change variable
            }
        }
    }
}