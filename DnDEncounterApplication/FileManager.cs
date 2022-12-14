using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DnDEncounterApplication
{
    internal static class FileManager
    {
        public const string playerDirectory = @"PlayerCharacters";
        public const string enemyDirectory = @"Enemies";

        public static PlayerCharacter[] ReadAllPlayerCharacters()
        {
            string[] playerCharactersFiles = Directory.GetFiles(playerDirectory);
            PlayerCharacter[] players = new PlayerCharacter[playerCharactersFiles.Length];

            for (int i = 0; i < playerCharactersFiles.Length; i++)
            {
                players[i] = ReadPlayer(playerCharactersFiles[i]);
            }

            return players;
        }
        public static Enemy[] ReadAllEnemies()
        {
            string[] enemiesFiles = Directory.GetFiles(enemyDirectory);
            Enemy[] enemies = new Enemy[enemiesFiles.Length];

            for (int i = 0; i < enemiesFiles.Length; i++)
            {
                enemies[i] = ReadEnemy(enemiesFiles[i]);
            }

            return enemies;
        }

        public static PlayerCharacter ReadPlayer(string file)
        {
            PlayerCharacter player = JsonSerializer.Deserialize<PlayerCharacter>(file);
            if (player != null)
                return player;
            throw new Exception();

        }
        public static Enemy ReadEnemy(string file)
        {
            Enemy enemy = JsonSerializer.Deserialize<Enemy>(file);
            if (enemy != null)
                return enemy;
            throw new Exception();
        }

        public static string[] GetAllPlayerCharacterPaths()
        {
            return Directory.GetFiles(playerDirectory);
        }
        public static string[] GetAllEnemyCharacterPaths()
        {
            return Directory.GetFiles(enemyDirectory);
        }


        /// <summary>
        /// Add a new file with player character
        /// </summary>
        /// <returns>Return false if file already exists, true otherwise</returns>
        public static bool AddNewPlayerCharacter(PlayerCharacter character)
        {
            string filename = character.Name.Trim().Replace(" ", "_");
            string characterJson = JsonSerializer.Serialize(character, new JsonSerializerOptions() { WriteIndented = true });

            if (File.Exists(playerDirectory + filename))
                return false;

            File.WriteAllText(playerDirectory + filename, characterJson);
            return true;
        }

        /// <summary>
        /// Add a new file with enemy
        /// </summary>
        /// <returns>Return false if file already exists, true otherwise</returns>
        public static bool AddNewEnemy(Enemy enemy)
        {
            string filename = enemy.Name.Trim().Replace(" ", "_");
            string characterJson = JsonSerializer.Serialize(enemy, new JsonSerializerOptions() { WriteIndented = true });

            if (File.Exists(enemyDirectory + filename))
                return false;

            File.WriteAllText(enemyDirectory + filename, characterJson);
            return true;
        }
    }
}
