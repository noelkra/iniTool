using System.Collections.Generic;
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

       
    }
}
