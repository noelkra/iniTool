using System.Collections;
using System.Collections.Generic;

namespace iniTool
{
    /*
     * This class fixes wrong entries in the ini-Files.
     */
    internal class EntityFixer
    {
        private readonly ResourceEdit _resEdit;
        private readonly ArrayList _incorrectFiles;
        private string _tempModulesIniFile;
        private string _tempRootModulesDir;
        private string _tempRootSpecsDir;
        private string _correctRootSpecsDir;
        private string _correctRootModulesDir;
        private string _correctModulesIniFile;

        public EntityFixer(ArrayList incorrectFiles)
        {
            _resEdit = new ResourceEdit();
            _incorrectFiles = incorrectFiles;
            SetCorrectValues();
        }

        /// <summary>
        ///     Gets the wrong values from the files and changes them if needed.
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

                /*
                 * TODO Check if user wants to correct file
                 * Maybe take the whole EntityList as parameter so that the User can choose
                 * which Item to change by clicking on the checkbox.
                 *
                 * [Important]
                 * Check if value updates on changes to checkbox.IsChecked → Is this value also updated here (static method?)
                 */

                if (_tempRootSpecsDir != _correctRootSpecsDir)
                    configIniHandler.IniWriteValue("System", "Root_Specs_Dir", _correctRootSpecsDir);
                if (_tempRootModulesDir != _correctRootModulesDir)
                    configIniHandler.IniWriteValue("System", "Root_Modules_Dir", _correctRootModulesDir);
                if (_tempModulesIniFile != _correctModulesIniFile)
                    configIniHandler.IniWriteValue("System", "Modules_Ini_File", _correctModulesIniFile);
            }
        }

        protected void SetCorrectValues()
        {
            /*
             * This Method should get the path to the Files.
             * On calling of the method, the section and the keys get read out.
             * With this information given (assumed that the key and section are related), the content from the
             * corresponding ini-files gets saved to the correctValueList. This gets returned
             */
            //TODO the values should get added to the correctValueList. The return parameters should be iterated with a loop to add them to SetContentList.
            _correctRootSpecsDir = _resEdit.GetRootSpecsDir();
            _correctRootModulesDir = _resEdit.GetRootModulesDir();
            _correctModulesIniFile = _resEdit.GetModulesIniFile();
        }
    }
}