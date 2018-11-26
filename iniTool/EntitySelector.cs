using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace iniTool
{
    /*
     * This class gets all the entities from the ini files needed in the application.
     */
    internal class EntitySelector
    {
      
        private readonly string _workingDirectory;
        private readonly List<EntityContent> _entityContent;
        private string[] _directoryOfFoldersList;
        private readonly ResourceEdit _resEdit;
        static ArrayList _incorrectFiles;

        private string _tempProjectName;
        private string _tempProjectId;
        private string _tempProjectGuid;
        private string _tempPwProject;
        private string _tempPwProjectGuid;
        private string _tempRootSpecsDir;
        private string _tempRootModulesDir;
        private string _tempModulesIniFile;
        private string _correctRootSpecsDir;
        private string _correctRootModulesDir;
        private string _correctModulesIniFile;
        private bool _tempIsChecked;


        internal EntitySelector(string workingDirectory)
        {
            _workingDirectory = workingDirectory + @"\";
            _entityContent = new List<EntityContent>();
            _resEdit = new ResourceEdit();
            SetCorrectValues();
        }

        public void SelectEntityContents() //TODO shorten and split into multiple methods
        {
            //Get all Directories
            try
            {
                _directoryOfFoldersList = Directory.GetDirectories(_workingDirectory);
            }
            catch (DirectoryNotFoundException dnfEx)
            {
                MessageBox.Show("Directory could not be found! Error: " + dnfEx, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            //TODO Call a method which gets the content of the ini files and calls SetEntityContents with the given values.
            if (_directoryOfFoldersList != null) ReadFileContent(_directoryOfFoldersList);
        }

        public List<EntityContent> GetEntityContents()
        {
            return _entityContent;
        }

        private void ReadFileContent(IEnumerable<string> directoryOfFoldersList)
        {
            var prefix = _resEdit.GetPrefix();
            foreach (var filepath in directoryOfFoldersList)
            {
                if (filepath.Substring(_workingDirectory.Length, prefix.Length) != prefix) continue;
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

                //Search for wrong values
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

                //Add content to _entityContent
                SetEntityContents(filepath, _tempIsChecked, _tempProjectName, _tempProjectId, _tempProjectGuid,
                    _tempPwProject, _tempPwProjectGuid, _tempRootSpecsDir, _tempRootModulesDir, _tempModulesIniFile);
            }
        }

        protected void SetEntityContents(string directory, bool tempIsChecked, string tempProjectName, string tempProjectId,
            string tempProjectGuid, string tempPwProject, string tempPwProjectGuid, string tempRootSpecsDir,
            string tempRootModulesDir, string tempModulesIniFile)
        {
            _entityContent.Add(new EntityContent
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

        protected void SetCorrectValues()
        {
            _correctRootSpecsDir = _resEdit.GetRootSpecsDir();
            _correctRootModulesDir = _resEdit.GetRootModulesDir();
            _correctModulesIniFile = _resEdit.GetModulesIniFile();
        }
    }
}
