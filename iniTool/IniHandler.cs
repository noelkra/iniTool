using System.Runtime.InteropServices;
using System.Text;

namespace iniTool
{
    /*  About this class
    *   This class was copied and modified from following Website
    *   https://www.codeproject.com/Articles/1966/An-INI-file-handling-class-using-C
    *   All credits go to the amazing folks on CodeProject
    */
    internal class IniHandler 
    {
        private readonly string _path;

        [DllImport("KERNEL32.DLL")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("KERNEL32.DLL")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>
        /// iniHandler Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniHandler(string iniPath)
        {
            _path = iniPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string section, string key)
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, _path);
            return temp.ToString();

        }
    }
}
