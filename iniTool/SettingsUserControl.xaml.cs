using System.Windows;

namespace iniTool
{
    /// <summary>
    /// Interaktionslogik für SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl
    {
        private readonly ResourceEdit _resEdit = new ResourceEdit();
        private readonly IniHandler _iniHandler;

        public SettingsUserControl()
        {
            _iniHandler = new IniHandler(@".\preferences.ini");
            InitializeComponent();
        }
        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            _iniHandler.IniWriteValue("PATHVALUES", "ModulesIniFile", TbModulesIniFile.Text);
            _iniHandler.IniWriteValue("PATHVALUES", "RootModulesDir", TbRootModulesDir.Text);
            _iniHandler.IniWriteValue("PATHVALUES", "RootSpecsDir", TbRootSpecsDir.Text);
            _iniHandler.IniWriteValue("GENERAL", "FolderPrefix", TbPrefix.Text);
            _iniHandler.IniWriteValue("GENERAL", "CUAE", CbCanUserApproveEdits.IsChecked.ToString());

            MessageBox.Show("Settings saved!", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void settingsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TbRootSpecsDir.Text = _resEdit.GetRootSpecsDir();
            TbRootModulesDir.Text = _resEdit.GetRootModulesDir();
            TbModulesIniFile.Text = _resEdit.GetModulesIniFile();
            TbPrefix.Text = _resEdit.GetPrefix();
            CbCanUserApproveEdits.IsChecked = _resEdit.CanUserApproveEdits();
        }
    }
}
