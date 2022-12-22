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
using System.Windows.Shapes;

namespace DnDEncounterApplication
{
    /// <summary>
    /// Interaction logic for PopUpWindow.xaml
    /// </summary>
    public partial class PopUpWindow : Window
    {
        public bool? Confirmed { get; set; }

        public PopUpWindow()
        {
            InitializeComponent();
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            Confirmed = true;
            Close();
        }

        private void Cancel_Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Confirmed = false;
            Close();
        }

        internal async Task<bool> GetResoults()
        {
            Task<bool> task = new Task<bool>(() =>
            {
                while (Confirmed == null)
                {
                    Task.Delay(10);
                }
                return (bool)Confirmed;
            });

            return await task;
        }
    }
}
