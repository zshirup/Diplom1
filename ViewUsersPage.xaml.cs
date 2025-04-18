using PersonnelAccessSystem.Services;
using System.Windows;
using System.Windows.Controls;

namespace PersonnelAccessSystem.Pages
{
    public partial class ViewUsersPage : Page
    {
        private DatabaseService _dbService = new DatabaseService();

        public ViewUsersPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _dbService.GetUsers();
            UsersGrid.ItemsSource = users;
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var userId = (int)button.Tag;
            NavigationService?.Navigate(new EditUserPage(userId));
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var userId = (int)button.Tag;

            var result = MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _dbService.DeleteUser(userId);
                LoadUsers(); // Обновляем список пользователей
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}