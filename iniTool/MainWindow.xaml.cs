using System.Windows;

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
        private void MiOpenLastWorkspace_Click(object sender, RoutedEventArgs e) //TODO remove if not needed
        {
            //dgListFiles.ItemsSource = null;
            //dgListFiles.ItemsSource = fileHandler.GetContentFromFiles(resEdit.getWorkspace().ToString());
        }
        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            ucMain.Visibility = Visibility.Hidden;
            ucSettings.Visibility = Visibility.Hidden;
            ucAbout.Visibility = Visibility.Visible;
        }
        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            ucMain.Visibility = Visibility.Hidden;
            ucSettings.Visibility = Visibility.Visible;
            ucAbout.Visibility = Visibility.Hidden;
        }
        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            ucMain.Visibility = Visibility.Visible;
            ucSettings.Visibility = Visibility.Hidden;
            ucAbout.Visibility = Visibility.Hidden;
        }
        private void BtnOpenNewWorkspace_Click(object sender, RoutedEventArgs e)
        {
            mainUserControl.openFiles();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(!resEdit.GetPreferencesStatus())
            {
                resEdit.SetDefaultPreferences();
            }
        }

        private void Window_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
