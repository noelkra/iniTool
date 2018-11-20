using System.Windows;

namespace iniTool
{
    /// <summary>
    /// Interaktionslogik für SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl
    {
        private readonly RessourceEdit _resEdit = new RessourceEdit();
        private readonly IniHandler _iniHandler;

        public SettingsUserControl()
        {
            _iniHandler = new IniHandler(@".\preferences.ini");
            InitializeComponent();
        }
        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            _iniHandler.IniWriteValue("PATHVALUES", "ModulesIniFile", tbModulesIniFile.Text);
            _iniHandler.IniWriteValue("PATHVALUES", "RootModulesDir", tbRootModulesDir.Text);
            _iniHandler.IniWriteValue("PATHVALUES", "RootSpecsDir", tbRootSpecsDir.Text);
            _iniHandler.IniWriteValue("GENERAL", "FolderPrefix", tbPrefix.Text);
            _iniHandler.IniWriteValue("GENERAL", "CUAE", cbCanUserApproveEdits.IsChecked.ToString());

            MessageBox.Show("Settings saved!", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void settingsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbRootSpecsDir.Text = _resEdit.GetRootSpecsDir();
            tbRootModulesDir.Text = _resEdit.GetRootModulesDir();
            tbModulesIniFile.Text = _resEdit.GetModulesIniFile();
            tbPrefix.Text = _resEdit.GetPrefix();
            cbCanUserApproveEdits.IsChecked = _resEdit.CanUserApproveEdits();
        }
    }
}
