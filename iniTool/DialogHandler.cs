namespace iniTool
{
    internal class DialogHandler
    {
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
                dialog.ShowDialog();
                dir = dialog.SelectedPath;
            }
            return dir;
        }
    }
}
