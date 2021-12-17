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

namespace EstateAgency.Screens.ReportsMenu
{
    /// <summary>
    /// Логика взаимодействия для ReportsMenu.xaml
    /// </summary>
    public partial class ReportsMenu : Page
    {
        EstateAgencyEntities entities = EstateAgencyEntities.GetContext();
        public ReportsMenu()
        {
            InitializeComponent();
            PriceHistoryDataGrid.ItemsSource = entities.EstatePriceLogs.ToList();
            AgreementsDataGrid.ItemsSource = entities.Agreements.ToList();
        }
    }
}
