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

namespace EstateAgency.Screens.Clients
{
    /// <summary>
    /// Логика взаимодействия для CreateNewClient.xaml
    /// </summary>
    public partial class CreateNewClient : Page
    {
        Client newClient = new Client();
        EstateAgencyEntities entities = EstateAgencyEntities.GetContext();
        User employer;
        public CreateNewClient()
        {
            InitializeComponent();
            DataContext = newClient;
            newClient.BranchID = AuthorizedEmployer.user.BranchID;
            employer = AuthorizedEmployer.user;

            //Navigator.frame.Navigate(new Estates.CreateNewEstate(employer));
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(NameTextBox.Text) ||
                 String.IsNullOrEmpty(SurnameTextBox.Text) ||
                 String.IsNullOrEmpty(PassportSeriesTextBox.Text) ||
                 String.IsNullOrEmpty(PassportNumberTextBox.Text) ||
                 String.IsNullOrEmpty(IssuedByWhomTextBox.Text) ||
                 DateIssuePicker.SelectedDate == null ||
                 String.IsNullOrEmpty(DivisionCodeTextBox.Text) ||
                 String.IsNullOrEmpty(RegistrationTextBox.Text) ||
                 String.IsNullOrEmpty(PhoneTextBox.Text) ||
                 (WantToBuyRadioButton.IsChecked == false && WantToStoreRadioButton.IsChecked == false)
                 )
                {
                    _ = MessageBox.Show("Заполните обязательные поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    entities.Clients.Add(newClient);
                    entities.SaveChanges();

                    if (WantToBuyRadioButton.IsChecked.Value)
                    {
                        Navigator.frame.Navigate(new Requirements.CreateRequirements(newClient));
                    }
                    else if (WantToStoreRadioButton.IsChecked.Value)
                    {
                        Navigator.frame.Navigate(new Estates.CreateNewEstate());
                    }
                }

            }
            catch (Exception exception)
            {
                _ = MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                foreach (var dataItem in exception.Data.Values)
                {
                    Console.WriteLine(dataItem.ToString());
                }
            }
        }
    }
}


