using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace iniTool
{
    /// <summary>
    /// Interaktionslogik für MainUserControl.xaml
    /// </summary>
    public partial class MainUserControl : UserControl
    {
        FileHandler fileHandler = new FileHandler();
        DialogHandler dialogHandler = new DialogHandler();
        CustomRessourceEdit resEdit = new CustomRessourceEdit();
        public MainUserControl()
        {
            InitializeComponent();
        }

        //Methods
        private void btnConfirmAction_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This operation cannot be undone. Are you sure you want to continue?", "Confirm action", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                fileHandler.repairFiles();
            }
        }
        public void openFiles()
        {
            string dir = dialogHandler.openFolderDialog();
            //If folder chosen successfully, save to regx file and activate btnOpenInExplorer
            if (dir != null && dir != "")
            {
                setLoading(true);
                //Dispatcher.BeginInvoke(new Action(() => { MessageBox.Show(this, "Workspace is loading. Please be patient as it can take up to 2 minutes.", "Please wait...", MessageBoxButton.OK, MessageBoxImage.Information); })); 
                resEdit.setWorkspace(dir);
                dgListFiles.ItemsSource = null;
                dgListFiles.ItemsSource = fileHandler.getContentFromFiles(dir);
                setLoading(false);
            }
        }
        private void setLoading(bool loading)
        {
            //TODO not working - gets executed but doesn't change stuff
            if (loading)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                dgListFiles.IsEnabled = false;
                btnConfirmAction.IsEnabled = false;
                //mnuToolbarMenu.IsEnabled = false;
            }
            else
            {
                Mouse.OverrideCursor = null;
                dgListFiles.IsEnabled = true;
                btnConfirmAction.IsEnabled = true;
                //mnuToolbarMenu.IsEnabled = true;
            }
        }
    }
}
