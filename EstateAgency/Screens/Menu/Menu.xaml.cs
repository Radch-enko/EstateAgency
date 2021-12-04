﻿using EstateAgency.Navigation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EstateAgency.Screens.Menu
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu(User authorizedUser)
        {
            InitializeComponent();
        }

        private void CreateClientContract_Click(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Navigate(new Clients.CreateClient.CreateClientForm());
        }

        private void SearchEstate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowReports_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
