using EstateAgency.Common;
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

namespace EstateAgency.Screens.Agreements
{
    /// <summary>
    /// Логика взаимодействия для CreateAgreement.xaml
    /// </summary>
    public partial class CreateAgreement : Page
    {
        EstateAgencyEntities entities = EstateAgencyEntities.GetContext();
        Agreement agreement = new Agreement();
        public CreateAgreement()
        {
            InitializeComponent();
            agreement.BranchID = AuthorizedEmployer.user.BranchID;
            DataContext = agreement;
            SetupPickers();
        }

        private void SetupPickers()
        {
            BuyerIDPicker.ItemsSource = entities.Clients.ToList();
            EstatePicker.ItemsSource = entities.Estates.ToList();
            DatePicker.SelectedDate = DateTime.Today;
        }

        private void SaveAgreement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BuyerIDPicker.SelectedItem != null &&
                                EstatePicker.SelectedItem != null &&
                                DatePicker.SelectedDate != null &&
                                !String.IsNullOrEmpty(CostTextBox.Text))
                {
                    entities.Agreements.Add(agreement);
                    entities.SaveChanges();
                    _ = MessageBox.Show("Акт успешно сохранен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _ = MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception exception)
            {
                _ = MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void BuyerIDPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            agreement.BuyerID = (comboBox.SelectedItem as Client).ID;
        }

        private void EstatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            agreement.EstateID = (comboBox.SelectedItem as Estate).ID;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            agreement.Date = datePicker.SelectedDate.Value;
        }
    }
}
