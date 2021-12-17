using EstateAgency.Common;
using EstateAgency.Navigation;
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
        public Menu()
        {
            InitializeComponent();
            if (AuthorizedEmployer.user.UsersRole.Name == "Администратор")
            {
                ShowReports.Visibility = Visibility.Visible;
            }
            else
            {
                ShowReports.Visibility = Visibility.Hidden;
            }
        }


        private void SearchEstate_Click(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Navigate(new Estates.SearchEstate());
        }

        private void ShowReports_Click(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Navigate(new ReportsMenu.ReportsMenu());
        }

        private void AddNewClient_Click(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Navigate(new Clients.CreateNewClient());
        }

        private void AddNewRequirement_Click(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Navigate(new Requirements.CreateRequirements());
        }

        private void AddNewEstate_Click(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Navigate(new Estates.CreateNewEstate());
        }
        private void CreateAgreement_Click(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Navigate(new Agreements.CreateAgreement());
        }

        private void AddFeedback_Click(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Navigate(new FeedbackForm.CreateFeedback());
        }
    }
}
