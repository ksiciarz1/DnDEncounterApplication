using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDEncounterApplication
{
    public class Enemy
    {
        public string Name { set; get; }
        public int HP { set; get; }
        public int AC { set; get; }
        public int CR { set; get; }
        public int EXP { set; get; }
        public int ProficencyBonus { set; get; }
        public int AttackBonus { set; get; }
        public int SpellAttack { set; get; }
        public int SaveDC { set; get; }
        public Dictionary<string, string> Attacks { set; get; }


        internal string WriteInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (Name != null)
                stringBuilder.AppendLine(Name);
            if (AC != 0)
                stringBuilder.AppendLine("AC: " + AC);
            if (AttackBonus != 0)
                stringBuilder.AppendLine("Attack Bonus: " + AttackBonus);
            if (CR != 0)
                stringBuilder.AppendLine("CR: " + CR);
            if (EXP != 0)
                stringBuilder.AppendLine("EXP: " + EXP);
            if (SaveDC != 0)
                stringBuilder.AppendLine("SaveDC: " + SaveDC);

            return stringBuilder.ToString();
        }
        internal string WriteAttackInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
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
    }
}
