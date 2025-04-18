using PersonnelAccessSystem.Services;
using System.Windows;
using System.Windows.Controls;

namespace PersonnelAccessSystem.Pages
{
    public partial class ManageAccountsPage : Page
    {
        private DatabaseService _dbService = new DatabaseService();

        public ManageAccountsPage()
        {
            InitializeComponent();
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            var users = _dbService.GetUsers();
            AccountsGrid.ItemsSource = users;
        }

        private void EditAccount_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var userId = (int)button.Tag;
            NavigationService?.Navigate(new EditAccountPage(userId));
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var userId = (int)button.Tag;

            var result = MessageBox.Show("Вы уверены, что хотите удалить эту учетную запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _dbService.DeleteUser(userId);
                LoadAccounts(); // Обновляем список аккаунтов
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}