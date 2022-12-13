using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDEncounterApplication
{
    public class Weapon
    {
        public string Name { set; get; }
        public string DMG { set; get; }
        public bool Magical { set; get; }
        public WeaponType WeaponType { set; get; }
        public Dictionary<string, string> Modifiers { set; get; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (Name != null)
                stringBuilder.AppendLine($"Name: {Name}");
            if (DMG != null)
                stringBuilder.AppendLine($"DMG: {DMG}");
            stringBuilder.AppendLine($"Magical: {Magical}");
            stringBuilder.AppendLine($"Weapon Type: {WeaponType}");
            stringBuilder.AppendLine($"Modifiers: ");
            foreach (var modifier in Modifiers)
            {
                stringBuilder.AppendLine($"\t {modifier.Key}: {modifier.Value}");
            }


            return stringBuilder.ToString();
        }
    }

    public enum WeaponType
    {
        Axe,
        Sword,
        Spectre,
        Spear,
        Battleaxe,
        Hammer,
        Other
    }
}
