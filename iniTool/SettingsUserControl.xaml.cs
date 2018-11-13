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
        public SettingsUserControl()
        {
            InitializeComponent();
        }
        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            resEdit.SetModulesIniFile(tbModulesIniFile.Text);
            resEdit.SetRootModulesDir(tbRootModulesDir.Text);
            resEdit.SetRootSpecsDir(tbRootSpecsDir.Text);
            MessageBox.Show("Settings saved!", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void settingsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbRootSpecsDir.Text = resEdit.GetRootSpecsDir();
            tbRootModulesDir.Text = resEdit.GetRootModulesDir();
            tbModulesIniFile.Text = resEdit.GetModulesIniFile();
        }
    }
}
