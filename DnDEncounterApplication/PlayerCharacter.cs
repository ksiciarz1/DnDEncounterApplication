using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DnDEncounterApplication
{
    public class PlayerCharacter
    {
        public string Name { set; get; }
        public int HP { set; get; }
        public int LVL { set; get; }
        public int AC { set; get; }
        public int ProficencyBonus { set; get; }
        public int AttackBonus { set; get; }
        public int SpellAttack { set; get; }
        public int SaveDC { set; get; }
        public Dictionary<string, string> Attacks { set; get; }
        public Dictionary<string, string> Spells { set; get; }
        public List<Weapon> Weapons { set; get; }

        public PlayerCharacter(string name, int hp, int lvl, int ac, int proficencyBonus, int attackBonus, int saveDC, int spellAttack, Dictionary<string, string> attacks, Dictionary<string, string> spells, List<Weapon> weapons)
        {
            Name = name;
            HP = hp;
            LVL = lvl;
            AC = ac;
            ProficencyBonus = proficencyBonus;
            AttackBonus = attackBonus;
            SaveDC = saveDC;
            SpellAttack = spellAttack;
            Attacks = attacks;
            Spells = spells;
            Weapons = weapons;
        }

        internal string WriteInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (Name != null)
                stringBuilder.AppendLine(Name);
            if (AC != 0)
                stringBuilder.AppendLine("AC: " + AC);
            if (AttackBonus != 0)
                stringBuilder.AppendLine("Attack Bonus: " + AttackBonus);
            if (SaveDC != 0)
                stringBuilder.AppendLine("SaveDC: " + SaveDC);

            return stringBuilder.ToString();
        }
        internal string WriteAttacks()
        {
            StringBuilder stringBuilder = new();
            if (Attacks != null)
            {
                stringBuilder.AppendLine("Attacks: ");
                foreach (var attack in Attacks)
                {
                    stringBuilder.AppendLine($"{attack.Key}: {attack.Value}");
                }
            }
            return stringBuilder.ToString();
        }
        internal string WriteSpellsInfo()
        {
            StringBuilder stringBuilder = new();
            if (Spells != null)
            {
                stringBuilder.AppendLine("Spells: ");
                foreach (var spell in Spells)
                {
                    stringBuilder.AppendLine($"{spell.Key}: {spell.Value}");
                }
            }
            return stringBuilder.ToString();
        }
        internal string WriteWeaponsInfo()
        {
            StringBuilder stringBuilder = new();
            if (Weapons != null)
            {
                foreach (var weapon in Weapons)
                {
                    stringBuilder.AppendLine(weapon.ToString());
                }
            }
            return stringBuilder.ToString();
        }
    }
}
