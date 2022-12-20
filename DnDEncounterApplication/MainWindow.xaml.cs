﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public void SetImage(string file)
        {
            file = "Images/" + file;
            ImageSource? imageSource = FileManager.GetImageSource(file);
            if (imageSource != null)
            {
                SelectedImage.Source = imageSource;
                SelectedImage.Visibility = Visibility.Visible;
            }
            else
            {
                SelectedImage.Visibility = Visibility.Hidden;
            }
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
        private void Dnd_Encounter_Builder_Click(object sender, RoutedEventArgs e)
        {
            // Opens a website in default browser
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://www.aidedd.org/dnd-encounter/index.php?l=1",
                    UseShellExecute = true
                });
            }
            catch { }
        }
        /// <summary>
        /// Adds all player characters from standard party array
        /// </summary>
        private void Add_Standard_Party_Click(object sender, RoutedEventArgs e)
        {
            string[] playerCharactersFiles = FileManager.GetAllPlayerCharacterPaths();

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
                        playerCharacters.Add(FileManager.ReadPlayer(playerCharactersFiles[i]));
                        CreatureData.Add(new CreatureViewContext(playerCharacters[i]));
                        SetImage(playerCharacters);
                        contained++;
                        break;
                    }
                }
            }
        }
        private void Remove_Creature_Click(object sender, RoutedEventArgs e)
        {
            if (MyDataGrid.SelectedItem != null)
            {
                Enemy enemy = MyDataGrid.SelectedItem as Enemy;
                if (enemy != null)
                    enemies.Remove(enemy);

                PlayerCharacter playerCharacter = MyDataGrid.SelectedItem as PlayerCharacter;
                if (playerCharacter != null)
                    playerCharacters.Remove(playerCharacter);

                CreatureData.Remove((CreatureViewContext)MyDataGrid.SelectedItem);
            }
        }
        /// <summary>
        /// Moves items in datagrid up and down based on sender
        /// </summary>
        private void Direction_Button_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = MyDataGrid.SelectedIndex;
            // Clears Sort in data grid
            ICollectionView view = CollectionViewSource.GetDefaultView(CreatureData);
            if (view.SortDescriptions.Any(description =>
            {
                return description != null;
            }))
            {
                view.SortDescriptions.Clear();

                // Moves creature in Creature Data collection in its place in Data grid
                for (int i = 0; i < MyDataGrid.Items.Count; i++)
                    for (int j = 0; j < CreatureData.Count; j++)
                    {
                        if (CreatureData[j] == MyDataGrid.Items[i])
                        {
                            CreatureData.Move(j, i);
                            break;
                        }
                    }

                // Clears sorting arrow in columns headers
                foreach (var column in MyDataGrid.Columns)
                    column.SortDirection = null;

                MyDataGrid.SelectedIndex = selectedIndex;
            }

            if (sender == UpButton)
            {
                if (MyDataGrid.SelectedIndex != 0)
                    CreatureData.Move(MyDataGrid.SelectedIndex, MyDataGrid.SelectedIndex - 1);

            }
            else if (sender == DownButton)
            {
                if (MyDataGrid.SelectedIndex != MyDataGrid.Items.Count - 1)
                    CreatureData.Move(MyDataGrid.SelectedIndex, MyDataGrid.SelectedIndex + 1);

            }
            MyDataGrid.Items.Refresh();
        }

    }
}
