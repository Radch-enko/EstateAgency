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

namespace EstateAgency.Screens.Requirements
{
    /// <summary>
    /// Логика взаимодействия для CreateRequirements.xaml
    /// </summary>
    public partial class CreateRequirements : Page
    {
        EstateAgencyEntities entities = EstateAgencyEntities.GetContext();
        Requirement requirement = new Requirement();

        public CreateRequirements()
        {
            InitializeComponent();
            SetupPickers();
            DataContext = requirement;
        }
        public CreateRequirements(Client createdClient)
        {
            InitializeComponent();
            
            if (createdClient != null)
            {
                requirement.ClientID = createdClient.ID;
                SetupPickers(createdClient);
                DataContext = requirement;
            }
        }

        private void SetupPickers(Client createdClient = null)
        {
            PossibleClientPicker.ItemsSource = entities.Clients.ToList();

            if (createdClient != null)
            {
                PossibleClientPicker.SelectedItem = createdClient;
            }

            EstateTypePicker.ItemsSource = entities.EstateTypes.ToList();
            MultiFlooBuildingTypePicker.ItemsSource = entities.MultiFloorBuildingTypes.ToList();
            DistrictPicker.ItemsSource = entities.Districts.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PossibleClientPicker.SelectedItem != null)
                {
                    entities.Requirements.Add(requirement);
                    entities.SaveChanges();
                    MessageBox.Show("Требование зарегистрировано", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void PossibleClientPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            requirement.ClientID = (comboBox.SelectedItem as Client).ID;
        }

        private void DistrictPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            requirement.DistrictID = (comboBox.SelectedItem as District).ID;
        }

        private void EstateTypePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            requirement.EstateTypeID = (comboBox.SelectedItem as EstateType).ID;
        }

        private void MultiFlooBuildingTypePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            requirement.MultiFloorBuildingTypeID = (comboBox.SelectedItem as MultiFloorBuildingType).ID;
        }
    }
}
