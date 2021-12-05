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

namespace EstateAgency
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigator.frame = MainContainer;
            Navigator.frame.Navigate(new Screens.Login.Login());
        }

        private void MainContainer_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Frame frame = sender as Frame;
            String pageTitle = (frame.Content as Page).Title;
            AppTitle.Content = pageTitle;
            if (Navigator.frame.CanGoBack && pageTitle != "Главное меню")
            {
                ButtonReturn.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonReturn.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Navigate(new Screens.Menu.Menu());
        }
    }
}
