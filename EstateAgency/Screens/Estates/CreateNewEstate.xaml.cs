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

namespace EstateAgency.Screens.Estates
{
    /// <summary>
    /// Логика взаимодействия для CreateNewEstate.xaml
    /// </summary>
    public partial class CreateNewEstate : Page
    {
        Estate estate = new Estate();
        EstateAgencyEntities entities = EstateAgencyEntities.GetContext();

        public CreateNewEstate()
        {
            InitializeComponent();
            estate.BranchID = AuthorizedEmployer.user.BranchID;
            estate.Date = DateTime.Now;
            DataContext = estate;
            SetupPickers();
        }

        public CreateNewEstate(Client createdOwner)
        {
            InitializeComponent();
            estate.BranchID = AuthorizedEmployer.user.BranchID;
            estate.Date = DateTime.Now;
            estate.OwnerID = createdOwner.ID;
            DataContext = estate;
            SetupPickers();
        }

        public CreateNewEstate(Estate wantoToOpenEstate)
        {
            InitializeComponent();

            if (wantoToOpenEstate != null)
            {
                estate = wantoToOpenEstate;
            }
            DataContext = estate;
            SetupPickers();
        }

        private void SetupPickers()
        {
            EstateTypePicker.ItemsSource = entities.EstateTypes.ToList();
            EstateTypePicker.SelectedItem = estate.EstateType;

            BuildingTypePicker.ItemsSource = entities.BuildingTypes.ToList();
            BuildingTypePicker.SelectedItem = estate.BuildingType;

            DistrictPicker.ItemsSource = entities.Districts.ToList();
            DistrictPicker.SelectedItem = estate.District;

            OwnerPicker.ItemsSource = entities.Clients.ToList();
            OwnerPicker.SelectedItem = entities.Clients.Where(client => client.ID == estate.OwnerID).FirstOrDefault();
        }

        private void SaveEstate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (
               OwnerPicker.SelectedItem != null &&
               DistrictPicker.SelectedItem != null &&
               AddressTextBox.Text != "" &&
               TotalAreaTextBox.Text != "" &&
               FloorNumberTextBox.Text != "" &&
               CostTextBox.Text != "")
                {
                    if (estate.ID == 0)
                    {
                        entities.Estates.Add(estate);
                    }

                    entities.SaveChanges();
                    MessageBox.Show("Недвижимость сохранена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void EstateTypePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            estate.EstateTypeID = (comboBox.SelectedItem as EstateType).ID;

            switch (estate.EstateTypeID)
            {
                case 1:
                    KitchenAreaContainer.Visibility = Visibility.Hidden;
                    KicthenAreaTextBox.Text = null;

                    FloorFieldContainer.Visibility = Visibility.Hidden;
                    FloorNumberTextBox.Text = null;

                    RoomFieldContainer.Visibility = Visibility.Hidden;
                    RoomNumberTextBox.Text = null;

                    BuildingTypeContainer.Visibility = Visibility.Hidden;
                    BuildingTypePicker.SelectedItem = null;

                    BalconyFieldsContainer.Visibility = Visibility.Hidden;
                    BalconyNumberTextBox.Text = null;

                    PrivatePlotFieldsContainer.Visibility = Visibility.Visible;
                    PrivatePlotTextBox.Text = estate.PrivatePlot;

                    AdditionalBuildingsFieldsContainer.Visibility = Visibility.Visible;
                    AdditionalBuildingsTextBox.Text = estate.AdditionalBuildings;

                    TotalAreaLabel.Content = "Общая площадь (сот.)*";
                    break;

                case 3:
                    KitchenAreaContainer.Visibility = Visibility.Hidden;
                    KicthenAreaTextBox.Text = null;

                    AdditionalBuildingsFieldsContainer.Visibility = Visibility.Visible;
                    AdditionalBuildingsTextBox.Text = estate.AdditionalBuildings;
                    break;
                default:
                    KitchenAreaContainer.Visibility = Visibility.Visible;
                    FloorFieldContainer.Visibility = Visibility.Visible;
                    RoomFieldContainer.Visibility = Visibility.Visible;
                    BuildingTypeContainer.Visibility = Visibility.Visible;
                    BalconyFieldsContainer.Visibility = Visibility.Visible;
                    TotalAreaLabel.Content = "Общая площадь м²*";

                    PrivatePlotFieldsContainer.Visibility = Visibility.Hidden;
                    PrivatePlotTextBox.Text = null;

                    AdditionalBuildingsFieldsContainer.Visibility = Visibility.Hidden;
                    AdditionalBuildingsTextBox.Text = null;
                    break;
            }
        }

        private void BuildingTypePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            estate.BuildingTypeID = (comboBox.SelectedItem as BuildingType).ID;
        }

        private void OwnerPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            estate.OwnerID = (comboBox.SelectedItem as Client).ID;
        }

        private void DistrictPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            estate.DistrictID = (comboBox.SelectedItem as District).ID;
        }

        private void FloorNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text != "" && textBox.Text != "0")
            {
                int floorNumber = int.Parse(textBox.Text);

                if (floorNumber >= 6)
                {
                    BuildingTypeContainer.Visibility = Visibility.Visible;
                }
            }
            else
            {
                BuildingTypeContainer.Visibility = Visibility.Hidden;
            }
        }
    }
}
