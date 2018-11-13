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
        RessourceEdit resEdit = new RessourceEdit();
        SettingsUserControl settingsUserControl = new SettingsUserControl();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            gridMainData.Visibility = Visibility.Hidden;
            ucSettings.Visibility = Visibility.Hidden;
            ucAbout.Visibility = Visibility.Visible;
        }
        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            gridMainData.Visibility = Visibility.Hidden;
            ucSettings.Visibility = Visibility.Visible;
            ucAbout.Visibility = Visibility.Hidden;
        }
        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            gridMainData.Visibility = Visibility.Visible;
            ucSettings.Visibility = Visibility.Hidden;
            ucAbout.Visibility = Visibility.Hidden;
        }
        private void BtnOpenNewWorkspace_Click(object sender, RoutedEventArgs e)
        {
            //If folder chosen successfully, save to ini-file
            string dir = dialogHandler.OpenFolderDialog();
            resEdit.SetWorkspace(dir);
            openFiles(dir);
        }

        private void Window_Loaded(object sender, System.EventArgs e)
        {
            if (!resEdit.GetPreferencesStatus())
            {
                resEdit.SetDefaultPreferences();
            }
        }
        private void btnConfirmAction_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This operation cannot be undone. Are you sure you want to continue?", "Confirm action", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                //Repair the Files and reload them afterwards.
                fileHandler.RepairFiles();
                openFiles(resEdit.GetWorkspace());
            }
        }
        public void openFiles(string dir)
        {
            //TODO Enable button to open in explorer and uncomment the Information dispatcher ↓↓↓↓
            if (dir != null && dir != "")
            {
                setLoading(true);
                fileHandler.LoadFileContent(dir);
                //Dispatcher.BeginInvoke(new Action(() => { MessageBox.Show(this, "Workspace is loading. Please be patient as it can take up to 2 minutes.", "Please wait...", MessageBoxButton.OK, MessageBoxImage.Information); })); 
                dgListFileContent.ItemsSource = null;
                dgListFileContent.ItemsSource = fileHandler.getFileContent();
                setLoading(false);
            }
        }
        private void setLoading(bool loading)
        {
            //TODO not working - gets executed but doesn't change stuff
            if (loading)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                dgListFileContent.IsEnabled = false;
                btnConfirmAction.IsEnabled = false;
                //mnuToolbarMenu.IsEnabled = false;
            }
            else
            {
                Mouse.OverrideCursor = null;
                dgListFileContent.IsEnabled = true;
                btnConfirmAction.IsEnabled = true;
                //mnuToolbarMenu.IsEnabled = true;
            }
        }
    }
}
