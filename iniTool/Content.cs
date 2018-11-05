using System.Collections;

namespace iniTool
{
    class Content
    {
        public string folderPath { get; set; }
        public string projectName { get; set; }
        public string projectID { get; set; }
        public string projectGUID { get; set; }
        public string pwProject { get; set; }
        public string pwProjectGUID { get; set; }
        public string rootSpecsDir { get; set; }
        public string rootModulesDir { get; set; }
        public string modulesIniFile { get; set; }
        public bool isChecked { get; set; }
    }
}
