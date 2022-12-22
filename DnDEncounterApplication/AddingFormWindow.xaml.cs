using System.ComponentModel;
using System.Windows;

namespace DnDEncounterApplication
{
    /// <summary>
    /// Interaction logic for AddingFormWindow.xaml
    /// </summary>
    public partial class AddingFormWindow : Window
    {
        public MainWindow parentWindow;
        public AddingFormWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            parentWindow = mainWindow;
        }
    }
}
