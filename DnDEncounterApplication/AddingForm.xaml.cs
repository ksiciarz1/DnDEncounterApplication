using System;
using System.Numerics;
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
            LVL.Visibility = Visibility.Collapsed;
            CR.Visibility = Visibility.Visible;
            EXP.Visibility = Visibility.Visible;
            SpellAttack.Visibility = Visibility.Collapsed;
            AddSpellButton.Visibility = Visibility.Collapsed;
            AddWeaponButton.Visibility = Visibility.Collapsed;
            SpellsTreeView.Visibility = Visibility.Collapsed;
            WeaponsTreeView.Visibility = Visibility.Collapsed;
        }
        private void PlayerRadio_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            LVL.Visibility = Visibility.Visible;
            CR.Visibility = Visibility.Collapsed;
            EXP.Visibility = Visibility.Collapsed;
            SpellAttack.Visibility = Visibility.Visible;
            AddSpellButton.Visibility = Visibility.Visible;
            AddWeaponButton.Visibility = Visibility.Visible;
            SpellsTreeView.Visibility = Visibility.Visible;
            WeaponsTreeView.Visibility = Visibility.Visible;
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
                SpellAttack = Convert.ToInt32(SpellAttackTextBox.Text),
                SaveDC = Convert.ToInt32(SaveDCTextBox.Text)
            };
            AddingFormWindow parentWindow = Window.GetWindow(this) as AddingFormWindow;
            if (parentWindow != null) parentWindow.parentWindow.AddPlayerCharacterToDataGrid(player);
            ClearFields();
            CloseWindow();
        }
        private void AddEnemy()
        {
            //throw new NotImplementedException();
            Enemy enemy = new Enemy()
            {
                Name = NameTextBox.Text,
                HP = Convert.ToInt32(HPTextBox.Text),
                AC = Convert.ToInt32(LVLTextBox.Text),
                CR = Convert.ToInt32(ACTextBox.Text),
                EXP = Convert.ToInt32(ACTextBox.Text),
                ProficencyBonus = Convert.ToInt32(ProficencyBonusTextBox.Text),
                AttackBonus = Convert.ToInt32(AttackBonusTextBox.Text),
                SaveDC = Convert.ToInt32(SaveDCTextBox.Text),
                SpellAttack = Convert.ToInt32(SpellAttackTextBox.Text)
            };
            AddingFormWindow parentWindow = Window.GetWindow(this) as AddingFormWindow;
            if (parentWindow != null) parentWindow.parentWindow.AddEnemyToDataGrid(enemy);
            ClearFields();
            CloseWindow();
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
            if ((bool)SaveToFile.IsChecked)
            {

            }
            if ((bool)PlayerRadio.IsChecked)
                AddPlayer();
            if ((bool)EnemyRadio.IsChecked)
                AddEnemy();
        }
        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e) => CloseWindow();

        private void CloseWindow() => Window.GetWindow(this).Close();
    }
}
