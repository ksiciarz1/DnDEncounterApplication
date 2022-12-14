using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDEncounterApplication
{
    public class CreatureViewContext
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int AC { set; get; }
        public int AttackBonus { set; get; }
        public int SaveDC { set; get; }
        public int SpellAttack { set; get; }
        public int Iniciative { set; get; }
        public PlayerCharacter? PlayerCharacter { set; get; }
        public Enemy? Enemy { set; get; }

        public CreatureViewContext(PlayerCharacter player)
        {
            PlayerCharacter = player;
            Name = player.Name;
            HP = player.HP;
            AC = player.AC;
            AttackBonus = player.AttackBonus;
            SaveDC = player.SaveDC;
            SpellAttack = player.SpellAttack;
        }

        public CreatureViewContext(Enemy enemy)
        {
            Enemy = enemy;
            Name = enemy.Name;
            HP = enemy.HP;
            AC = enemy.AC;
            AttackBonus = enemy.AttackBonus;
            SaveDC = enemy.SaveDC;
            SpellAttack = enemy.SpellAttack;
        }

        public CreatureViewContext(string name, int hp, int ac, int attackBonus, int saveDC, int spellAttack, int iniciative)
        {
            Name = name;
            HP = hp;
            AC = ac;
            AttackBonus = attackBonus;
            SaveDC = saveDC;
            SpellAttack = spellAttack;
            Iniciative = iniciative;
        }
    }
}
