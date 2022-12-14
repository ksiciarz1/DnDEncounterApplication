﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace DnDEncounterApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<PlayerCharacter> playerCharacters = new List<PlayerCharacter>();
        private List<Enemy> enemies = new List<Enemy>();
        private ObservableCollection<CreatureViewContext> CreatureData = new ObservableCollection<CreatureViewContext>();

        private string[] standardParty = new string[] { "Flint_Torunn", "Dorn_GreyCastle", "Vathumal_Sheshen", "Rurik_Rumnaheim", "Ithil", "Snus_Of_The_Watchfull_Eye" };

        public MainWindow()
        {
            InitializeComponent();

            MyTabControl.Items.Clear();

            MyDataGrid.ItemsSource = CreatureData;
            MyDataGrid.SelectedCellsChanged += DataGrid_SelectedCellsChanged;
            MyDataGrid.AutoGeneratedColumns += (s, e) =>
            {
                MyDataGrid.Columns.RemoveAt(MyDataGrid.Columns.Count - 1); // Remove Enemy
                MyDataGrid.Columns.RemoveAt(MyDataGrid.Columns.Count - 1); // Remove PlayerCharacter

                // Only HP and Iniciative column should be editable
                foreach (var column in MyDataGrid.Columns)
                {
                    if (!column.Header.ToString().Contains("Iniciative") && !column.Header.ToString().Contains("HP"))
                        column.IsReadOnly = true;
                }
            };
        }

        private void SetUpTabForEnemy(Enemy enemy)
        {
            // Enemy Info
            TabItem EnemyTabItem = new TabItem();
            EnemyTabItem.Header = "Enemy";
            TextBox characterTextBox = new TextBox();
            characterTextBox.Text = enemy.WriteInfo();
            characterTextBox.IsReadOnly = true;
            EnemyTabItem.Content = characterTextBox;
            MyTabControl.Items.Add(EnemyTabItem);

            // Attacks Info
            TabItem AttackTabItem = new TabItem();
            AttackTabItem.Header = "Attacks";
            TextBox attackTextBox = new TextBox();
            attackTextBox.Text = enemy.WriteAttackInfo();
            attackTextBox.IsReadOnly = true;
            AttackTabItem.Content = attackTextBox;
            MyTabControl.Items.Add(AttackTabItem);
        }
        private void SetUpTabForPlayerCharacter(PlayerCharacter playerCharacter)
        {
            // Character Info
            TabItem characterTabItem = new TabItem();
            characterTabItem.Header = "Character";
            TextBox characterTextBox = new TextBox();
            characterTextBox.Text = playerCharacter.WriteInfo();
            characterTextBox.IsReadOnly = true;
            characterTabItem.Content = characterTextBox;
            MyTabControl.Items.Add(characterTabItem);

            // Attack Info
            TabItem attackTabItem = new TabItem();
            attackTabItem.Header = "Attacks";
            TextBox attackTextBox = new TextBox();
            attackTextBox.Text = playerCharacter.WriteAttacks();
            attackTextBox.IsReadOnly = true;
            attackTabItem.Content = attackTextBox;
            MyTabControl.Items.Add(attackTabItem);

            // Spells Info
            TabItem spellsTabItem = new TabItem();
            spellsTabItem.Header = "Spells";
            TextBox spellsTextBox = new TextBox();
            spellsTextBox.Text = playerCharacter.WriteSpellsInfo();
            spellsTextBox.IsReadOnly = true;
            spellsTabItem.Content = spellsTextBox;
            MyTabControl.Items.Add(spellsTabItem);

            // Weapon Info
            TabItem weaponsTabItem = new TabItem();
            weaponsTabItem.Header = "Weapons";
            TextBox weaponTextBox = new TextBox();
            weaponTextBox.IsReadOnly = true;
            weaponTextBox.Text = playerCharacter.WriteWeaponsInfo();
            MyTabControl.Items.Add(weaponsTabItem);
        }

        public void AddEnemyToDataGrid(Enemy enemy)
        {
            enemies.Add(enemy);
            CreatureData.Add(new CreatureViewContext(enemy));
        }
        public void AddPlayerCharacterToDataGrid(PlayerCharacter player)
        {
            playerCharacters.Add(player);
            CreatureData.Add(new CreatureViewContext(player));
        }

        // Events
        private void DataGrid_SelectedCellsChanged(object sender, EventArgs e)
        {
            MyTabControl.Items.Clear();

            var view = MyDataGrid.SelectedItem as CreatureViewContext;
            if (view != null)
            {
                if (view.PlayerCharacter != null)
                    SetUpTabForPlayerCharacter(view.PlayerCharacter);
                else if (view.Enemy != null)
                    SetUpTabForEnemy(view.Enemy);
            }
        }
        private void Dnd_Encounter_Calculator_Click(object sender, RoutedEventArgs e)
        {
            // Opens a website in default browser
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://kastark.co.uk/rpgs/encounter-calculator-5th/",
                    UseShellExecute = true
                });
            }
            catch { }
        }
        private void Dnd_Random_Encounter_Click(object sender, RoutedEventArgs e)
        {
            // Opens a website in default browser
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "http://tools.goblinist.com/5enc",
                    UseShellExecute = true
                });
            }
            catch { }
        }
        /// <summary>
        /// Adds all player characters from standard party array
        /// </summary>
        private void Add_Standart_Party_Click(object sender, RoutedEventArgs e)
        {
            string[] playerCharactersFiles = Directory.GetFiles(@"PlayerCharacters");

            int contained = 0;
            // Loop all files
            for (int i = 0; i < playerCharactersFiles.Length; i++)
            {
                // If all standard party players were founds brake
                if (contained == standardParty.Length)
                    break;

                // Look for any party player
                foreach (var standardPlayer in standardParty)
                {
                    if (playerCharactersFiles[i].Contains(standardPlayer))
                    {
                        string playerFileContent = File.ReadAllText(playerCharactersFiles[i]);
                        playerCharacters.Add(JsonSerializer.Deserialize<PlayerCharacter>(playerFileContent));
                        CreatureData.Add(new CreatureViewContext(playerCharacters[i]));
                        contained++;
                        break;
                    }
                }
            }
        }
        private void Remove_Creature_Click(object sender, RoutedEventArgs e)
        {
            if (MyDataGrid.SelectedItem != null)
                CreatureData.Remove((CreatureViewContext)MyDataGrid.SelectedItem);
        }
    }
}
