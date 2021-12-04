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

namespace EstateAgency.Screens.Clients.CreateClient
{
    /// <summary>
    /// Логика взаимодействия для CreateClientForm.xaml
    /// </summary>
    public partial class CreateClientForm : Page
    {
        Estate estate = new Estate();
        EstateAgencyEntities entities = EstateAgencyEntities.GetContext();
        public CreateClientForm()
        {
            InitializeComponent();
            DataContext = estate;
            SetupPickers();
        }

        private void SetupPickers()
        {
            EstateTypePicker.ItemsSource = entities.EstateTypes.ToList();
            BuildingTypePicker.ItemsSource = entities.MultiFloorBuildingTypes.ToList();
            DistrictPicker.ItemsSource = entities.Districts.ToList();
        }

        private void SaveEstate_Click(object sender, RoutedEventArgs e)
        {
            entities.Estates.Add(estate);
            entities.SaveChanges();
        }
    }
}
