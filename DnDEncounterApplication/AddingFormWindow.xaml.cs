﻿using System;
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
    /// Interaction logic for AddingFormWindow.xaml
    /// </summary>
    public partial class AddingFormWindow : Window
    {
        public AddingFormWindow()
        {
            InitializeComponent();
            AddingForm form = new AddingForm();
            MyFrame.Navigate(form);
        }
    }
}