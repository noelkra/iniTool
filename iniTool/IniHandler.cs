using System.Runtime.InteropServices;
using System.Text;

namespace iniTool
{
    /*  
    *   This class was copied and modified from following Website
    *   https://www.codeproject.com/Articles/1966/An-INI-file-handling-class-using-C
    *   All credits go to the amazing folks on CodeProject
    */
    class IniHandler 
    {
        public string path;

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
        public IniHandler(string INIPath)
        {
            path = INIPath;
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
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();

        }
    }
}
