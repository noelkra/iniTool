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

        internal EntitySelector(string workingDirectory)
        {
            _workingDirectory = workingDirectory + @"\";
            _entityContent = new List<EntityContent>();
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
            //if (fileArray != null) SetCorrectFiles(fileArray);
        }

        public List<EntityContent> GetEntityContents()
        {
            return _entityContent;
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
    }
}
