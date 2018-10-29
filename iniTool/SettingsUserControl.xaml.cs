using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iniTool
{
    /// <summary>
    /// Interaktionslogik für SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        CustomRessourceEdit resEdit = new CustomRessourceEdit();
        public SettingsUserControl()
        {
            InitializeComponent();
        }
        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            resEdit.setModulesIniFile(tbModulesIniFile.Text);
            resEdit.setRootModulesDir(tbRootModulesDir.Text);
            resEdit.setRootSpecsDir(tbRootSpecsDir.Text);
        }
        private void settingsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbRootSpecsDir.Text = resEdit.getRootSpecsDir();
            tbRootModulesDir.Text = resEdit.getRootModulesDir();
            tbModulesIniFile.Text = resEdit.getModulesIniFile();
        }
    }
}
