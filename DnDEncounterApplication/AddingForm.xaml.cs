using System.Windows;
using System.Windows.Controls;

namespace DnDEncounterApplication
{
    /// <summary>
    /// Interaction logic for AddingForm.xaml
    /// </summary>
    public partial class AddingForm : Page
    {
        public AddingForm()
        {
            InitializeComponent();
        }

        private void EnemyRadio_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            LVL.Visibility = System.Windows.Visibility.Collapsed;

            SpellAttack.Visibility = System.Windows.Visibility.Collapsed;
            AddSpellButton.Visibility = System.Windows.Visibility.Collapsed;
            AddWeaponButton.Visibility = System.Windows.Visibility.Collapsed;
            SpellsTreeView.Visibility = System.Windows.Visibility.Collapsed;
            WeaponsTreeView.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void PlayerRadio_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            LVL.Visibility = System.Windows.Visibility.Visible;

            SpellAttack.Visibility = System.Windows.Visibility.Visible;
            AddSpellButton.Visibility = System.Windows.Visibility.Visible;
            AddWeaponButton.Visibility = System.Windows.Visibility.Visible;
            SpellsTreeView.Visibility = System.Windows.Visibility.Visible;
            WeaponsTreeView.Visibility = System.Windows.Visibility.Visible;
        }

        private void ApplyButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var parentWindow = this.Parent as Window;

            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void SaveToFile_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
