using System.Windows;

namespace iniTool
{
    class DialogHandler
    {
        public DialogHandler()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        }
        /// <summary>
        /// Creates and Shows an "Open File"-Dialog
        /// </summary>
        /// <returns name="dir"></returns>
        /// Returns the path of the chosen Folder
        public string OpenFolderDialog()
        {
            string dir;

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                dir = dialog.SelectedPath;
            }
            return dir;
        }
    }
}
