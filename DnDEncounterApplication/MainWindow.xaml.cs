﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private PlayerCharacter[] playerCharacters;
        private Enemy[] enemies;
        private ObservableCollection<CreatureViewContext> CreatureData = new ObservableCollection<CreatureViewContext>();
        private string[] standardParty = new string[] { "Flint_Torunn", "Dorn_GreyCastle", "Vathumal_Sheshen", "Rurik_Rumnaheim", "Ithil", "Snus_Of_The_Watchfull_Eye" };

        public MainWindow()
        {
            InitializeComponent();
            //string[] playerCharactersFiles = Directory.GetFiles(@"PlayerCharacters");
            //string[] enemiesFiles = Directory.GetFiles(@"Enemies");

            MyTabControl.Items.Clear();

            //playerCharacters = new PlayerCharacter[playerCharactersFiles.Length];
            //enemies = new Enemy[enemiesFiles.Length];

            // Getting Data From files
            //for (int i = 0; i < playerCharactersFiles.Length; i++)
            //{
            //    playerCharacters[i] = JsonSerializer.Deserialize<PlayerCharacter>(File.ReadAllText(playerCharactersFiles[i]));
            //    CreatureData.Add(new CreatureViewContext(playerCharacters[i]));
            //}
            //for (int i = 0; i < enemiesFiles.Length; i++)
            //{
            //    enemies[i] = JsonSerializer.Deserialize<Enemy>(File.ReadAllText(enemiesFiles[i]));
            //    CreatureData.Add(new CreatureViewContext(enemies[i]));
            //}

            MyDataGrid.ItemsSource = CreatureData;
            MyDataGrid.SelectedCellsChanged += DataGrid_SelectedCellsChanged;
            MyDataGrid.AutoGeneratedColumns += (s, e) => { MyDataGrid.Columns.RemoveAt(MyDataGrid.Columns.Count - 1); };
        }

        private void SetUpTabForEnemy(Enemy enemy)
        {
            TabItem EnemyTabItem = new TabItem();
            EnemyTabItem.Header = "Enemy";
            TextBox characterTextBox = new TextBox();
            characterTextBox.Text = enemy.WriteInfo();
            characterTextBox.IsReadOnly = true;
            EnemyTabItem.Content = characterTextBox;
            MyTabControl.Items.Add(EnemyTabItem);


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
            TabItem characterTabItem = new TabItem();
            characterTabItem.Header = "Character";
            TextBox characterTextBox = new TextBox();
            characterTextBox.Text = playerCharacter.WriteInfo();
            characterTextBox.IsReadOnly = true;
            characterTabItem.Content = characterTextBox;
            MyTabControl.Items.Add(characterTabItem);

            TabItem spellsTabItem = new TabItem();
            spellsTabItem.Header = "Spells";
            TextBox spellsTextBox = new TextBox();
            spellsTextBox.Text = playerCharacter.WriteSpellsInfo();
            spellsTextBox.IsReadOnly = true;
            spellsTabItem.Content = spellsTextBox;
            MyTabControl.Items.Add(spellsTabItem);

            TabItem weaponsTabItem = new TabItem();
            weaponsTabItem.Header = "Weapons";
            TextBox weaponTextBox = new TextBox();
            weaponTextBox.IsReadOnly = true;
            weaponTextBox.Text = playerCharacter.WriteWeaponsInfo();
            MyTabControl.Items.Add(weaponsTabItem);
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
                else
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
        private void Add_Standart_Party_Click(object sender, RoutedEventArgs e)
        {
            string[] playerCharactersFiles = Directory.GetFiles(@"PlayerCharacters");
            playerCharacters = new PlayerCharacter[playerCharactersFiles.Length];

            int contained = 0;
            for (int i = 0; i < playerCharactersFiles.Length; i++)
            {
                if (contained == standardParty.Length)
                    break;

                bool contains = false;

                foreach (var standardPlayer in standardParty)
                {
                    if (playerCharactersFiles[i].Contains(standardPlayer))
                    {
                        contains = true;
                        contained++;
                        break;
                    }
                }
                if (contains)
                {
                    string playerFileContent = File.ReadAllText(playerCharactersFiles[i]);
                    playerCharacters[i] = JsonSerializer.Deserialize<PlayerCharacter>(playerFileContent);
                    CreatureData.Add(new CreatureViewContext(playerCharacters[i]));
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
