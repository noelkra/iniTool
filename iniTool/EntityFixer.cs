using System.Collections.Generic;

namespace iniTool
{
    /*
     * This class fixes wrong entries in the ini-Files.
     */
    internal class EntityFixer
    {
        private readonly ResourceEdit _resEdit;
        private readonly IEnumerable<string> _incorrectFiles;
        private string _tempModulesIniFile;
        private string _tempRootModulesDir;
        private string _tempRootSpecsDir;
        private string _correctRootSpecsDir;
        private string _correctRootModulesDir;
        private string _correctModulesIniFile;

        public EntityFixer(IEnumerable<string> incorrectFiles)
        {
            _resEdit = new ResourceEdit();
            _incorrectFiles = incorrectFiles;
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

        protected void SetCorrectValues()
        {
            _correctRootSpecsDir = _resEdit.GetRootSpecsDir();
            _correctRootModulesDir = _resEdit.GetRootModulesDir();
            _correctModulesIniFile = _resEdit.GetModulesIniFile();
        }
    }
}