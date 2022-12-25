using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DnDEncounterApplication
{
    /// <summary>
    /// Interaction logic for Special.xaml
    /// </summary>
    public partial class Special : Window
    {
        private readonly AddingForm Parent;
        private Type type;
        public Special(Type type, AddingForm parent)
        {
            InitializeComponent();
            Parent = parent;
            this.type = type;

            switch (type)
            {
                case Type.Attack:
                    break;
                case Type.Spell:
                    break;
                case Type.Weapon:
                    break;
            }
        }

        public enum Type
        {
            Attack,
            Spell,
            Weapon
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != "")
            {
                Parent.AddSpecial(type, NameTextBox.Text, DescTextBox.Text);
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
