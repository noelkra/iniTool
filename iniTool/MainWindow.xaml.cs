using System;
using System.Deployment.Application;
using System.Windows;
using System.Windows.Input;

namespace iniTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileHandler fileHandler = new FileHandler();
        DialogHandler dialogHandler = new DialogHandler();
        CustomRessourceEdit resEdit = new CustomRessourceEdit();
        SettingsUserControl settingsUserControl = new SettingsUserControl();
        MainUserControl mainUserControl = new MainUserControl();
        
        public MainWindow()
        {
            InitializeComponent();
        } 
        private void miOpenLastWorkspace_Click(object sender, RoutedEventArgs e) //TODO remove if not needed
        {
            //dgListFiles.ItemsSource = null;
            //dgListFiles.ItemsSource = fileHandler.getContentFromFiles(resEdit.getWorkspace().ToString());
        }
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            ucMain.Visibility = Visibility.Hidden;
            ucSettings.Visibility = Visibility.Hidden;
            ucAbout.Visibility = Visibility.Visible;
        }
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            ucMain.Visibility = Visibility.Hidden;
            ucSettings.Visibility = Visibility.Visible;
            ucAbout.Visibility = Visibility.Hidden;
        }
        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            ucMain.Visibility = Visibility.Visible;
            ucSettings.Visibility = Visibility.Hidden;
            ucAbout.Visibility = Visibility.Hidden;
        }
        private void btnOpenNewWorkspace_Click(object sender, RoutedEventArgs e)
        {
            mainUserControl.openFiles();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(!resEdit.getPreferencesStatus())
            {
                resEdit.setDefaultPreferences();
            }
        }
    }
}
