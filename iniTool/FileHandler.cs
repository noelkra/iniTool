using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace iniTool
{
    internal class FileHandler
    {
        //Declaration
        private readonly List<Content> _contentList;
        private readonly ResourceEdit _resEdit;
        private readonly ArrayList _incorrectFiles;
        private string _correctRootSpecsDir;
        private string _correctRootModulesDir;
        private string _correctModulesIniFile;
        private string _tempProjectName, _tempProjectId, _tempProjectGuid, _tempPwProject, _tempPwProjectGuid, _tempRootSpecsDir, _tempRootModulesDir, _tempModulesIniFile;
        private bool _tempIsChecked;
        //Constructor
        public FileHandler()
        {
            _resEdit = new ResourceEdit();
            _incorrectFiles = new ArrayList();
            _contentList = new List<Content>();
        }
        /// <summary>
        /// Get content form chosen directory.
        /// </summary>
        /// <PARAM name="dir"></PARAM>
        /// Path to Directory
        public void LoadFileContent(string dir) //TODO shorten and split into multiple methods
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
                MessageBox.Show("Directory could not be found! Error: " + dnfEx, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Get correct values
            if (fileArray != null)
            {
                SetCorrectFiles(fileArray, dir);
            }
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
                if (_tempRootSpecsDir != _correctRootSpecsDir || _tempRootModulesDir != _correctRootModulesDir || _tempModulesIniFile != _correctModulesIniFile) //MODULES INI FILE
                {
                    _tempIsChecked = true;
                    _incorrectFiles.Add(filepath + @"\Config\config.ini");
                }
                else { _tempIsChecked = false; }

                //Add content to contentList
                SetContentList(filepath, _tempIsChecked, _tempProjectName, _tempProjectId, _tempProjectGuid, _tempPwProject, _tempPwProjectGuid, _tempRootSpecsDir, _tempRootModulesDir, _tempModulesIniFile);
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
                {
                    configIniHandler.IniWriteValue("System", "Root_Specs_Dir", _correctRootSpecsDir);
                }
                if(_tempRootModulesDir != _correctRootModulesDir)
                {
                    configIniHandler.IniWriteValue("System", "Root_Modules_Dir", _correctRootModulesDir);
                }
                if(_tempModulesIniFile != _correctModulesIniFile)
                {
                    configIniHandler.IniWriteValue("System", "Modules_Ini_File", _correctModulesIniFile);
                }
            }
        }
        /// <summary>
        /// Returns the contentList
        /// </summary>
        /// <returns></returns>
        public List<Content> GetFileContent()
        {
            return _contentList;
        }

        /// <summary>
        /// Adds the entity to the ContentList
        /// </summary>
        private void SetContentList(string directory, bool tempIsChecked, string tempProjectName, string tempProjectId, string tempProjectGuid, string tempPwProject, string tempPwProjectGuid, string tempRootSpecsDir, string tempRootModulesDir, string tempModulesIniFile)
        {
            _contentList.Add(new Content
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
    }
}