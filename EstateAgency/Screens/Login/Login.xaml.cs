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

namespace EstateAgency.Screens.Login
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        EstateAgencyEntities entities = EstateAgencyEntities.GetContext();
        IEnumerable<User> users;
        public Login()
        {
            InitializeComponent();
            users = entities.Users;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<User> validEntries = users.ToList().Where(user => user.Login == LoginTextBox.Text && user.Password == PasswordTextBox.Text);
            if (validEntries.Count() == 1)
            {
                _ = Navigator.frame.Navigate(new Menu.Menu(validEntries.FirstOrDefault()));
            }
            else
            {
                _ = MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
