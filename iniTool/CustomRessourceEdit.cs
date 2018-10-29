using System;
using System.Windows;

namespace iniTool
{
    class CustomRessourceEdit
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
        /// Get the path from Root_Specs_Dir
        /// </summary>
        /// <returns name="rootSpecsDir"></returns>
        /// Returns the path.
        public string GetRootSpecsDir()
        {
            string rootSpecsDir = iniHandler.IniReadValue("PATHVALUES", "Root_Specs_Dir");

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
        /// Saves the path of the Root_Specs_Dir
        /// </summary>
        /// <param name="dir"></param>
        /// Path of Root_Specs_Dir
        public void SetRootSpecsDir(string dir)
        {
            iniHandler.IniWriteValue("PATHVALUES", "Root_Specs_Dir", dir);
        }

        /// <summary>
        /// Get the path from Root_Modules_Dir
        /// </summary>
        /// <returns name="rootModulesDir"></returns>
        /// Returns the path.
        public string GetRootModulesDir()
        {
            string rootModulesDir = iniHandler.IniReadValue("PATHVALUES", "Root_Modules_Dir");

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
        /// Saves the path of the Root_Modules_Dir
        /// </summary>
        /// <param name="dir"></param>
        /// Path of Root_Modules_Dir
        public void SetRootModulesDir(string dir)
        {
            iniHandler.IniWriteValue("PATHVALUES", "Root_Modules_Dir", dir);
        }

        /// <summary>
        /// Get the path from Modules_Ini_File
        /// </summary>
        /// <returns name="modulesIniFile"></returns>
        /// Returns the path.
        public string GetModulesIniFile()
        {
            string modulesIniFile = iniHandler.IniReadValue("PATHVALUES", "Modules_Ini_File");

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
        /// Saves the path of the Modules_Ini_File
        /// </summary>
        /// <param name="dir"></param>
        /// Path of Modules_Ini_File
        public void SetModulesIniFile(string dir)
        {
            iniHandler.IniWriteValue("PATHVALUES", "Modules_Ini_File", dir);
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
        /// Creates a preferences.ini file and adds default values
        /// </summary>
        public void SetDefaultPreferences()
        {
            SetModulesIniFile(@"%CUSTOM_ROOT_PROD%\modules\modules.ini");
            SetRootModulesDir(@"%CUSTOM_ROOT_PROD%\modules");
            SetRootSpecsDir(@"%CUSTOM_ROOT_PROD%\Specs\Metric");
            SetPreferencesStatus();
        }
    }
}
