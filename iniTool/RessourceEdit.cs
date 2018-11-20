using System;
using System.Windows;

namespace iniTool
{
    class RessourceEdit
    {
        IniHandler iniHandler = new IniHandler(@".\preferences.ini");

        /// <summary>
        /// Gets the path of the workspace.
        /// </summary>
        /// <returns name="workspacePath"></returns>
        /// Returns the path.
        public string GetWorkspace()
        {
            string workspacePath = iniHandler.IniReadValue("GENERAL", "workspacePath");

            if (workspacePath != null && workspacePath != "")
            {
                return workspacePath;
            }
            else
            {
                MessageBox.Show("No workspace selected!", "Error", MessageBoxButton.OK);
                return null;
            }
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
                iniHandler.IniWriteValue("GENERAL", "workspacePath", dir);
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
            iniHandler.IniWriteValue("PATHVALUES", "ModulesIniFile", @"%CUSTOM_ROOT_PROD%\modules\modules.ini");
            iniHandler.IniWriteValue("PATHVALUES", "RootModulesDir", @"%CUSTOM_ROOT_PROD%\modules");
            iniHandler.IniWriteValue("PATHVALUES", "RootSpecsDir", @"%CUSTOM_ROOT_PROD%\Specs\Metric");
            iniHandler.IniWriteValue("GENERAL", "FolderPrefix", "Proj");
            iniHandler.IniWriteValue("GENERAL", "CUAE", "false");
            SetPreferencesStatus();
        }

        /// <summary>
        /// Get the path from Root_Specs_Dir
        /// </summary>
        /// <returns name="rootSpecsDir"></returns>
        /// Returns the path.
        public string GetRootSpecsDir()
        {
            string rootSpecsDir = iniHandler.IniReadValue("PATHVALUES", "RootSpecsDir");

            if (rootSpecsDir != null && rootSpecsDir != "")
            {
                return rootSpecsDir;
            }
            else
            {
                MessageBox.Show("No path defined! Please define a path and try again!", "Error", MessageBoxButton.OK);
                return null;
            }
        }

        /// <summary>
        /// Get the path from Root_Modules_Dir
        /// </summary>
        /// <returns name="rootModulesDir"></returns>
        /// Returns the path.
        public string GetRootModulesDir()
        {
            string rootModulesDir = iniHandler.IniReadValue("PATHVALUES", "RootModulesDir");

            if (rootModulesDir != null && rootModulesDir != "")
            {
                return rootModulesDir;
            }
            else
            {
                MessageBox.Show("No path defined! Please define path and try again!", "Error", MessageBoxButton.OK);
                return null;
            }
        }

        /// <summary>
        /// Get the path from Modules_Ini_File
        /// </summary>
        /// <returns name="modulesIniFile"></returns>
        /// Returns the path.
        public string GetModulesIniFile()
        {
            string modulesIniFile = iniHandler.IniReadValue("PATHVALUES", "ModulesIniFile");

            if (modulesIniFile != null && modulesIniFile != "")
            {
                return modulesIniFile;
            }
            else
            {
                MessageBox.Show("No path defined! Please define path and try again!", "Error", MessageBoxButton.OK);
                return null;
            }
        }

        /// <summary>
        /// Sets isPreferencesLoaded to true
        /// </summary>
        public void SetPreferencesStatus()
        {
            iniHandler.IniWriteValue("APPLICATION_DO_NOT_EDIT", "isPreferencesLoaded", "true");
        }

        /// <summary>
        /// Gets the Value of isPreferencesLoaded
        /// </summary>
        public bool GetPreferencesStatus()
        {
            string status = iniHandler.IniReadValue("APPLICATION_DO_NOT_EDIT", "isPreferencesLoaded");
            if (status == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the folder prefix.
        /// </summary>
        /// <returns name="prefix"></returns>
        /// Returns the prefix.
        public string GetPrefix()
        {
            string prefix = iniHandler.IniReadValue("GENERAL", "FolderPrefix");

            if (prefix != null && prefix != "")
            {
                return prefix;
            }
            else
            {
                MessageBox.Show("No workspace selected!", "Error", MessageBoxButton.OK);
                return null;
            }
        }
        public bool CanUserApproveEdits()
        {
            if (iniHandler.IniReadValue("GENERAL", "CUAE") == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
