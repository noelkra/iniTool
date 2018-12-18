using System.Collections.Generic;
using System.Windows;
using iniTool.helpers;
using iniTool.Views;

namespace iniTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly DialogHandler _dialogHandler;
        private readonly ResourceEdit _resEdit;
        private WaitingDialog _waitingDialog;
        private EntitySelector _entitySelector;
        private EntityFixer _entityFixer;
        private List<EntityContent> _entityContentList;

        public MainWindow()
        {
            _dialogHandler = new DialogHandler();
            _resEdit = new ResourceEdit();
            _entityContentList = new List<EntityContent>();
            InitializeComponent();
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            GridMainData.Visibility = Visibility.Hidden;
            UcSettings.Visibility = Visibility.Hidden;
            UcAbout.Visibility = Visibility.Visible;
        }
        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            GridMainData.Visibility = Visibility.Hidden;
            UcSettings.Visibility = Visibility.Visible;
            UcAbout.Visibility = Visibility.Hidden;
        }
        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            GridMainData.Visibility = Visibility.Visible;
            UcSettings.Visibility = Visibility.Hidden;
            UcAbout.Visibility = Visibility.Hidden;
        }
        private void BtnOpenNewWorkspace_Click(object sender, RoutedEventArgs e)
        {
            //If folder chosen successfully, save to ini-file
            var dir = _dialogHandler.OpenFolderDialog();
            _resEdit.SetWorkspace(dir);
            OpenFiles(dir);
        }
        private void Window_Loaded(object sender, System.EventArgs e)
        {
            if (!_resEdit.GetPreferencesStatus())
            {
                _resEdit.SetDefaultPreferences();
            }
        }
        private void btnConfirmAction_Click(object sender, RoutedEventArgs e)
        {
            _entityFixer = new EntityFixer(_entitySelector.GetIncorrectFilesList());
            var result = MessageBox.Show("This operation cannot be undone. Are you sure you want to continue?", "Confirm action", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes) return;
            //Repair the Files and reload them afterwards.
            _entityFixer.RepairFiles();
            OpenFiles(_resEdit.GetWorkspace());
        }

        /// <summary>
        /// Opens a new path
        /// </summary>
        /// <param name="dir"></param>
        public void OpenFiles(string dir)
        {
            //initialize a new instance of EntitySelector
            _entitySelector = new EntitySelector(dir);

            //create a new instance of WaitingDialog
            _waitingDialog = new WaitingDialog();
            
            //Test if dir is empty or null and set loading to true
            if (string.IsNullOrEmpty(dir)) return;

            //Read the files and set them as source of the data grid
            SetLoading(true);
            _entitySelector.SelectEntityContents();
            _entityContentList = _entitySelector.GetEntityContents();

            DgListFileContent.ItemsSource = null;
            DgListFileContent.ItemsSource = _entityContentList;

             SetLoading(false);
        }
        private void SetLoading(bool loading)
        {
            if (loading)
            {
                _waitingDialog.Show();
            }
            else
            {
                _waitingDialog.Close();
            }
        }
    }
}
