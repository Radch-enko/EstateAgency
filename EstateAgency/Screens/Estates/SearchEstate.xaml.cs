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

namespace EstateAgency.Screens.Estates
{
    /// <summary>
    /// Логика взаимодействия для SearchEstate.xaml
    /// </summary>
    public partial class SearchEstate : Page
    {
        EstateAgencyEntities entities = EstateAgencyEntities.GetContext();


        public SearchEstate()
        {
            InitializeComponent();
            DataContext = entities.Estates;
            FilteredEstatesDataGrid.ItemsSource = entities.Estates.ToList();
            ClientsDataGrid.ItemsSource = entities.Clients.ToList();
        }

        private void ButtonEditEstate_Click(object sender, RoutedEventArgs e)
        {
            var selectedEstate = FilteredEstatesDataGrid.SelectedItem as Estate;
            Navigator.frame.Navigate(new CreateNewEstate(selectedEstate));
        }

        private void ButtonDeleteEstate_Click(object sender, RoutedEventArgs e)
        {
            var selectedEstate = FilteredEstatesDataGrid.SelectedItem as Estate;
            try
            {
                entities.Estates.Remove(selectedEstate);
                entities.SaveChanges();
                MessageBox.Show("Объект удален", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                FilteredEstatesDataGrid.ItemsSource = entities.Estates.ToList();
            }
            catch (Exception exception)
            {
                _ = MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClientPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid comboBox = sender as DataGrid;
            Client client = comboBox.SelectedItem as Client;

            if (client != null)
            {
                var requirements = entities.Requirements.Where(requirement => requirement.ClientID == client.ID).ToList();

                bool isValidEstate = false;

                FilteredEstatesDataGrid.Items.Filter = new Predicate<object>(item =>
                {
                    Estate estate = item as Estate;

                    foreach (var requirement in requirements)
                    {
                        var isFloorValid = true;
                        if (requirement.MultiFloorBuildingType != null)
                        {
                            isFloorValid = estate.Floor >= requirement.MultiFloorBuildingType.Min &&
                            estate.Floor <= requirement.MultiFloorBuildingType.Max;
                        }

                        isValidEstate =
                        (requirement.DistrictID == estate.DistrictID || requirement.DistrictID == null) &&
                        (requirement.EstateTypeID == estate.EstateTypeID || requirement.EstateTypeID == null) &&
                        isFloorValid &&
                        (requirement.RoomNumber == estate.RoomNumber || requirement.RoomNumber == null);
                    }

                    return isValidEstate;
                });
            }

            
        }

        private void ClientSearchField_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            String query = textBox.Text;

            ClientsDataGrid.Items.Filter = new Predicate<object>(item =>
            {
                Client client = item as Client;

                return client.Name.Contains(query) || client.Surname.Contains(query) || client.MiddleName.Contains(query);
            });
        }
    }
}
