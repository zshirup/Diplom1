using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PersonnelAccessSystem.Pages
{
    public partial class HomePage : Page
    {
        private string _role;

        public HomePage(string role)
        {
            InitializeComponent();
            _role = role;

            // Показываем кнопки только для администраторов
            if (_role == "Admin")
            {
                ManageUsersButton.Visibility = Visibility.Visible;
                ManageAccountsButton.Visibility = Visibility.Visible; // Новая кнопка
            }
        }

        private void ViewDatabase_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DatabasePage());
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddPersonPage());
        }

        private void ViewLogs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new LogsPage());
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу входа (LoginPage)
            NavigationService?.Navigate(new LoginPage());
        }

        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ManageUsersPage());
        }

        private void ManageAccounts_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу управления аккаунтами
            NavigationService?.Navigate(new ManageAccountsPage());
        }
    }
}