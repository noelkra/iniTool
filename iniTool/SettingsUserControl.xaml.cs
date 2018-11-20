using System.Windows;
using System.Windows.Controls;

namespace iniTool
{
    /// <summary>
    /// Interaktionslogik für SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        RessourceEdit resEdit = new RessourceEdit();
        IniHandler iniHandler;

        public SettingsUserControl()
        {
            iniHandler = new IniHandler(@".\preferences.ini");
            InitializeComponent();
        }
        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            iniHandler.IniWriteValue("PATHVALUES", "ModulesIniFile", tbModulesIniFile.Text);
            iniHandler.IniWriteValue("PATHVALUES", "RootModulesDir", tbRootModulesDir.Text);
            iniHandler.IniWriteValue("PATHVALUES", "RootSpecsDir", tbRootSpecsDir.Text);
            iniHandler.IniWriteValue("GENERAL", "FolderPrefix", tbPrefix.Text);
            iniHandler.IniWriteValue("GENERAL", "CUAE", cbCanUserApproveEdits.IsChecked.ToString());

            MessageBox.Show("Settings saved!", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void settingsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbRootSpecsDir.Text = resEdit.GetRootSpecsDir();
            tbRootModulesDir.Text = resEdit.GetRootModulesDir();
            tbModulesIniFile.Text = resEdit.GetModulesIniFile();
            tbPrefix.Text = resEdit.GetPrefix();
        }
    }
}
