using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace iniTool
{
    internal class FileHandler
    {
        /*
         * TODO Will get replaced by classes EntitySelector.cs and EntityFixer.cs
         * They will handle the same stuff as this class but more organized and
         * ready for dynamic settings and dynamic content.
         */
        //Declaration
        private readonly List<EntityContent> _contentList;
        private readonly ArrayList _incorrectFiles;
        private readonly ResourceEdit _resEdit;
        private bool _tempIsChecked;

        private string _tempProjectName,
            _tempProjectId,
            _tempProjectGuid,
            _tempPwProject,
            _tempPwProjectGuid,
            _tempRootSpecsDir,
            _tempRootModulesDir,
            _tempModulesIniFile,
            _correctModulesIniFile,
            _correctRootModulesDir,
            _correctRootSpecsDir;

        //Constructor
        public FileHandler()
        {
            _resEdit = new ResourceEdit();
            _incorrectFiles = new ArrayList();
            _contentList = new List<EntityContent>();
        }

        /// <summary>
        ///     Get content form chosen directory.
        /// </summary>
        /// <PARAM name="dir"></PARAM>
        /// Path to Directory
        public void LoadFileContent(string dir)
        {
            _correctRootSpecsDir = _resEdit.GetRootSpecsDir();
            _correctRootModulesDir = _resEdit.GetRootModulesDir();
            _correctModulesIniFile = _resEdit.GetModulesIniFile();
            _contentList.Clear();

            //Variables for the correct Values
            string[] fileArray = null;
            //Get all Directories
            dir += @"\";
            try
            {
                fileArray = Directory.GetDirectories(dir);
            }
            catch (DirectoryNotFoundException dnfEx)
            {
                MessageBox.Show("Directory could not be found! Error: " + dnfEx, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            //Get correct values
            if (fileArray != null) SetCorrectFiles(fileArray, dir);
        }

        private void SetCorrectFiles(IEnumerable<string> fileArray, string dir)
        {
            var prefix = _resEdit.GetPrefix();
            foreach (var filepath in fileArray)
            {
                if (filepath.Substring(dir.Length, prefix.Length) != prefix) continue;
                var projIniHandler = new IniHandler($@"{filepath}\project.ini");
                var configIniHandler = new IniHandler($@"{filepath}\Config\config.ini");

                //get content form project.ini
                _tempProjectName = projIniHandler.IniReadValue("GENERAL", "PROJECTNAME");
                _tempProjectId = projIniHandler.IniReadValue("GENERAL", "PROJECTID");
                _tempProjectGuid = projIniHandler.IniReadValue("GENERAL", "PROJECTGUID");
                _tempPwProject = projIniHandler.IniReadValue("ProjectWise", "PWProject");
                _tempPwProjectGuid = projIniHandler.IniReadValue("ProjectWise", "PWProjectGUID");

                //get content from config.ini
                _tempRootSpecsDir = configIniHandler.IniReadValue("System", "Root_Specs_Dir");
                _tempRootModulesDir = configIniHandler.IniReadValue("System", "Root_Modules_Dir");
                _tempModulesIniFile = configIniHandler.IniReadValue("System", "Modules_Ini_File");

                //Search for files
                if (_tempRootSpecsDir != _correctRootSpecsDir || _tempRootModulesDir != _correctRootModulesDir ||
                    _tempModulesIniFile != _correctModulesIniFile) //MODULES INI FILE
                {
                    _tempIsChecked = true;
                    _incorrectFiles.Add($@"{filepath}\Config\config.ini"); //TODO was here
                }
                else
                {
                    _tempIsChecked = false;
                }

                //Add content to contentList
                SetContentList(filepath, _tempIsChecked, _tempProjectName, _tempProjectId, _tempProjectGuid,
                    _tempPwProject, _tempPwProjectGuid, _tempRootSpecsDir, _tempRootModulesDir, _tempModulesIniFile);
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
            foreach (string filepath in _incorrectFiles)
            {
                var configIniHandler = new IniHandler(filepath);
                _tempRootSpecsDir = configIniHandler.IniReadValue("System", "Root_Specs_Dir");
                _tempRootModulesDir = configIniHandler.IniReadValue("System", "Root_Modules_Dir");
                _tempModulesIniFile = configIniHandler.IniReadValue("System", "Modules_Ini_File");

                //Get which variable needs to get changed

                if (_tempRootSpecsDir != _correctRootSpecsDir)
                    configIniHandler.IniWriteValue("System", "Root_Specs_Dir", _correctRootSpecsDir);
                if (_tempRootModulesDir != _correctRootModulesDir)
                    configIniHandler.IniWriteValue("System", "Root_Modules_Dir", _correctRootModulesDir);
                if (_tempModulesIniFile != _correctModulesIniFile)
                    configIniHandler.IniWriteValue("System", "Modules_Ini_File", _correctModulesIniFile);
            }
        }

        /// <summary>
        ///     Returns the contentList
        /// </summary>
        /// <returns></returns>
        public List<EntityContent> GetFileContent()
        {
            return _contentList;
        }

        /// <summary>
        ///     Adds the entity to the ContentList
        /// </summary>
        private void SetContentList(string directory, bool tempIsChecked, string tempProjectName, string tempProjectId,
            string tempProjectGuid, string tempPwProject, string tempPwProjectGuid, string tempRootSpecsDir,
            string tempRootModulesDir, string tempModulesIniFile)
        {
            _contentList.Add(new EntityContent
            {
                FolderPath = directory,
                IsChecked = tempIsChecked,
                ProjectName = tempProjectName ?? "NoInformation",
                ProjectId = tempProjectId ?? "NoInformation",
                ProjectGuid = tempProjectGuid ?? "NoInformation",
                PwProject = tempPwProject ?? "NoInformation",
                PwProjectGuid = tempPwProjectGuid ?? "NoInformation",
                RootSpecsDir = tempRootSpecsDir ?? "NoInformation",
                RootModulesDir = tempRootModulesDir ?? "NoInformation",
                ModulesIniFile = tempModulesIniFile ?? "NoInformation"
            });
        }

        public ArrayList SetCorrectValues(string iniPath, string[] section, string key) //TODO get arrays for section and key to make application dynamic. 
        {
            /*
             * This Method should get the path to the Files.
             * On calling of the method, the section and the keys get read out.
             * With this information given (assumed that the key and section are related), the content from the
             * corresponding ini-files gets saved to the correctValueList. This gets returned
             */
            //TODO the values should get added to the correctValueList. The return parameters should be iterated with a loop to add them to SetContentList.
            var iniHandler = new IniHandler(iniPath);
            ArrayList correctValueList = new ArrayList();
            return correctValueList;
        }
    }
}