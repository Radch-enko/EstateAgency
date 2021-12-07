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

namespace EstateAgency.Screens.FeedbackForm
{
    /// <summary>
    /// Логика взаимодействия для CreateFeedback.xaml
    /// </summary>
    public partial class CreateFeedback : Page
    {
        EstateAgencyEntities entities = EstateAgencyEntities.GetContext();
        Feedback feedback = new Feedback();
        public CreateFeedback()
        {
            InitializeComponent();
            DataContext = feedback;
            SetupPickers();
        }

        private void SetupPickers()
        {
            ClientIDComboBox.ItemsSource = entities.Clients.ToList();
            EstateIDComboBox.ItemsSource = entities.Estates.ToList();
            VisitDatePicker.SelectedDate = DateTime.Today;
        }

        private void ClientIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            Client selectedClient = comboBox.SelectedItem as Client;

            feedback.Client = selectedClient;
        }

        private void EstateIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            Estate selectedEstate = comboBox.SelectedItem as Estate;

            feedback.Estate = selectedEstate;
        }

        private void VisitDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            feedback.VisitDate = datePicker.SelectedDate.Value;
        }

        private void AddFeedback_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (
                    !String.IsNullOrEmpty(CommentTextBox.Text) &&
                    VisitDatePicker.SelectedDate != null &&
                    ClientIDComboBox.SelectedItem != null &&
                    EstateIDComboBox.SelectedItem != null
                   )
                {
                    if (feedback.ID == 0)
                    {
                        entities.Feedbacks.Add(feedback);
                    }

                    entities.SaveChanges();
                    MessageBox.Show("Отзыв добавлен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    Navigator.frame.Navigate(new Menu.Menu());
                }
                else
                {
                    _ = MessageBox.Show("Заполните обязательные поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
            catch (Exception exception)
            {
                _ = MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
}
    }
}
