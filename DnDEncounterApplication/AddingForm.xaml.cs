using System;
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


        private void AddPlayer()
        {
            // TODO: Validation
            PlayerCharacter player = new PlayerCharacter()
            {
                Name = NameTextBox.Text,
                HP = Convert.ToInt32(HPTextBox.Text),
                LVL = Convert.ToInt32(LVLTextBox.Text),
                AC = Convert.ToInt32(ACTextBox.Text),
                ProficencyBonus = Convert.ToInt32(ProficencyBonusTextBox.Text),
                AttackBonus = Convert.ToInt32(AttackBonusTextBox.Text),
                SaveDC = Convert.ToInt32(SaveDCTextBox.Text),
                SpellAttack = Convert.ToInt32(SpellAttackTextBox.Text)
            };
            AddingFormWindow parentWindow = Window.GetWindow(this) as AddingFormWindow;
            if (parentWindow != null)
                parentWindow.parentWindow.AddPlayerCharacterToDataGrid(player);

            ClearFields();
        }
        private void AddEnemy()
        {
            throw new NotImplementedException();
        }
        private void ClearFields()
        {
            NameTextBox.Text = "";
            HPTextBox.Text = "";
            LVLTextBox.Text = "";
            ACTextBox.Text = "";
            ProficencyBonusTextBox.Text = "";
            AttackBonusTextBox.Text = "";
            SaveDCTextBox.Text = "";
            SpellAttackTextBox.Text = "";
        }


        private void ApplyButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((bool)PlayerRadio.IsChecked)
                AddPlayer();
            if ((bool)EnemyRadio.IsChecked)
                AddEnemy();
        }
        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e) => Window.GetWindow(this).Close();

        private void SaveToFile_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
