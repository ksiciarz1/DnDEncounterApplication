using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DnDEncounterApplication
{
    /// <summary>
    /// Interaction logic for AddingForm.xaml
    /// </summary>
    public partial class AddingForm : Page
    {
        private TextBox[] textBoxes;

        private Dictionary<string, string> attacks = new Dictionary<string, string>();
        private Dictionary<string, string> spells = new Dictionary<string, string>();
        private Dictionary<string, string> weapons = new Dictionary<string, string>();

        public AddingForm()
        {
            InitializeComponent();
            PlayerRadio.IsChecked = true;

            textBoxes = new TextBox[]
            {
                HPTextBox,
                LVLTextBox,
                ACTextBox,
                CRTextBox,
                EXPTextBox,
                ProficencyBonusTextBox,
                AttackBonusTextBox,
                SaveDCTextBox,
                SpellAttackTextBox
            };

            foreach (TextBox textBox in textBoxes)
            {
                textBox.TextChanged += TextBox_TextChanged;
            }
        }

        /// <summary>
        /// Used for validating
        /// </summary>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Regex regex = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);
            if (!regex.IsMatch(textBox.Text))
                textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 40, 40));
            else
                textBox.BorderBrush = null;
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

        private async void AddPlayer(bool saveToFile)
        {
            // TODO: Make adding weapons better
            List<Weapon> trueWeapons = new List<Weapon>();
            foreach (var weapon in weapons)
                trueWeapons.Add(new Weapon() { Name = weapon.Key });


            PlayerCharacter player = new PlayerCharacter()
            {
                Name = NameTextBox.Text,
                HP = Convert.ToInt32(HPTextBox.Text),
                LVL = Convert.ToInt32(LVLTextBox.Text),
                AC = Convert.ToInt32(ACTextBox.Text),
                ProficencyBonus = Convert.ToInt32(ProficencyBonusTextBox.Text),
                AttackBonus = Convert.ToInt32(AttackBonusTextBox.Text),
                SpellAttack = Convert.ToInt32(SpellAttackTextBox.Text),
                SaveDC = Convert.ToInt32(SaveDCTextBox.Text),
                Attacks = attacks,
                Spells = spells,
                Weapons = trueWeapons
            };

            AddingFormWindow parentWindow = Window.GetWindow(this) as AddingFormWindow;
            if (parentWindow != null) parentWindow.parentWindow.AddPlayerCharacterToDataGrid(player);
            ClearFields();
            if (saveToFile)
            {
                string name = GetPath(player.Name);
                if (FileManager.PlayerFileExists(name))
                {
                    PopUpWindow popUpWindow = new PopUpWindow();
                    popUpWindow.ShowDialog();
                    bool resoult = await popUpWindow.GetResoults();
                    if (resoult) FileManager.SavePlayerCharacter(player, true);
                }
                else
                    FileManager.SavePlayerCharacter(player);

            }
        }
        private async void AddEnemy(bool saveToFile)
        {
            Enemy enemy = new Enemy()
            {
                Name = NameTextBox.Text,
                HP = Convert.ToInt32(HPTextBox.Text),
                AC = Convert.ToInt32(ACTextBox.Text),
                CR = Convert.ToInt32(CRTextBox.Text),
                EXP = Convert.ToInt32(EXPTextBox.Text),
                ProficencyBonus = Convert.ToInt32(ProficencyBonusTextBox.Text),
                AttackBonus = Convert.ToInt32(AttackBonusTextBox.Text),
                SaveDC = Convert.ToInt32(SaveDCTextBox.Text),
                Attacks = attacks
            };

            AddingFormWindow parentWindow = Window.GetWindow(this) as AddingFormWindow;
            if (parentWindow != null) parentWindow.parentWindow.AddEnemyToDataGrid(enemy);
            ClearFields();
            if (saveToFile)
            {
                string name = GetPath(enemy.Name);
                if (FileManager.EnemyFileExists(name))
                {
                    PopUpWindow popUpWindow = new PopUpWindow();
                    popUpWindow.ShowDialog();
                    bool resoult = await popUpWindow.GetResoults();
                    if (resoult) FileManager.SaveEnemy(enemy, true);
                }
                else
                    FileManager.SaveEnemy(enemy);

            }
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

            attacks.Clear();
            spells.Clear();
            weapons.Clear();
        }
        private string GetPath(string name) => name.Trim().Replace(" ", "_") + ".json";


        private void ApplyButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool savingToFile = (bool)SaveToFile.IsChecked;
            if ((bool)PlayerRadio.IsChecked)
                AddPlayer(savingToFile);
            if ((bool)EnemyRadio.IsChecked)
                AddEnemy(savingToFile);
        }
        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e) => CloseWindow();

        private void CloseWindow() => Window.GetWindow(this).Close();

        private void AddSpecialButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button == AddAttackButton)
                new Special(Special.Type.Attack, this);
            else if (button == AddSpellButton)
                new Special(Special.Type.Spell, this);
            else if (button == AddWeaponButton)
                new Special(Special.Type.Weapon, this);
        }

        internal void AddSpecial(Special.Type type, string name, string description)
        {
            // TODO: Make adding weapons better
            switch (type)
            {
                case Special.Type.Attack:
                    attacks.Add(name, description);
                    break;
                case Special.Type.Spell:
                    spells.Add(name, description);
                    break;
                case Special.Type.Weapon:
                    weapons.Add(name, description);
                    break;
            }
        }
    }
}
