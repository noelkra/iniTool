using System;
using System.Windows;

namespace iniTool
{
    internal class ResourceEdit
    {
        private readonly IniHandler _iniHandler = new IniHandler(@".\preferences.ini");

        /// <summary>
        /// Gets the path of the workspace.
        /// </summary>
        /// <returns name="workspacePath"></returns>
        /// Returns the path.
        public string GetWorkspace()
        {
            var workspacePath = _iniHandler.IniReadValue("GENERAL", "workspacePath");

            if (!string.IsNullOrEmpty(workspacePath))
            {
                return workspacePath;
            }

            MessageBox.Show("No workspace selected!", "Error", MessageBoxButton.OK);
            return null;
        }

        /// <summary>
        /// Saves the workspacePath to the ini-File
        /// </summary>
        /// <param name="dir"></param>
        /// Path to the Workspace
        public void SetWorkspace(string dir)
        {
            try
            {
                _iniHandler.IniWriteValue("GENERAL", "workspacePath", dir);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops, something went wrong! Error: " + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Creates a preferences.ini file and adds default values
        /// </summary>
        public void SetDefaultPreferences()
        {
            _iniHandler.IniWriteValue("PATHVALUES", "ModulesIniFile", @"%CUSTOM_ROOT_PROD%\modules\modules.ini");
            _iniHandler.IniWriteValue("PATHVALUES", "RootModulesDir", @"%CUSTOM_ROOT_PROD%\modules");
            _iniHandler.IniWriteValue("PATHVALUES", "RootSpecsDir", @"%CUSTOM_ROOT_PROD%\Specs\Metric");
            _iniHandler.IniWriteValue("GENERAL", "FolderPrefix", "Proj");
            _iniHandler.IniWriteValue("GENERAL", "CUAE", "false");
            SetPreferencesStatus();
        }

        /// <summary>
        /// Get the path from Root_Specs_Dir
        /// </summary>
        /// <returns name="rootSpecsDir"></returns>
        /// Returns the path.
        public string GetRootSpecsDir()
        {
            var rootSpecsDir = _iniHandler.IniReadValue("PATHVALUES", "RootSpecsDir");

            if (!string.IsNullOrEmpty(rootSpecsDir))
            {
                return rootSpecsDir;
            }

            MessageBox.Show("No path defined! Please define a path and try again!", "Error", MessageBoxButton.OK);
            return null;
        }

        /// <summary>
        /// Get the path from Root_Modules_Dir
        /// </summary>
        /// <returns name="rootModulesDir"></returns>
        /// Returns the path.
        public string GetRootModulesDir()
        {
            var rootModulesDir = _iniHandler.IniReadValue("PATHVALUES", "RootModulesDir");

            if (!string.IsNullOrEmpty(rootModulesDir))
            {
                return rootModulesDir;
            }

            MessageBox.Show("No path defined! Please define path and try again!", "Error", MessageBoxButton.OK);
            return null;
        }

        /// <summary>
        /// Get the path from Modules_Ini_File
        /// </summary>
        /// <returns name="modulesIniFile"></returns>
        /// Returns the path.
        public string GetModulesIniFile()
        {
            var modulesIniFile = _iniHandler.IniReadValue("PATHVALUES", "ModulesIniFile");

            if (!string.IsNullOrEmpty(modulesIniFile))
            {
                return modulesIniFile;
            }

            MessageBox.Show("No path defined! Please define path and try again!", "Error", MessageBoxButton.OK);
            return null;
        }

        /// <summary>
        /// Sets isPreferencesLoaded to true
        /// </summary>
        public void SetPreferencesStatus()
        {
            _iniHandler.IniWriteValue("APPLICATION_DO_NOT_EDIT", "isPreferencesLoaded", "true");
        }

        /// <summary>
        /// Gets the Value of isPreferencesLoaded
        /// </summary>
        public bool GetPreferencesStatus()
        {
            var status = _iniHandler.IniReadValue("APPLICATION_DO_NOT_EDIT", "isPreferencesLoaded");
            return status == "true";
        }

        /// <summary>
        /// Gets the folder prefix.
        /// </summary>
        /// <returns name="prefix"></returns>
        /// Returns the prefix.
        public string GetPrefix()
        {
            var prefix = _iniHandler.IniReadValue("GENERAL", "FolderPrefix");

            if (!string.IsNullOrEmpty(prefix))
            {
                return prefix;
            }

            MessageBox.Show("No workspace selected!", "Error", MessageBoxButton.OK);
            return null;
        }
        public bool CanUserApproveEdits()
        {
            return _iniHandler.IniReadValue("GENERAL", "CUAE") == "True";
        }
    }
}
