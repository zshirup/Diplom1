using PersonnelAccessSystem.Models;
using PersonnelAccessSystem.Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PersonnelAccessSystem.Pages
{
    public partial class EditUserPage : Page
    {
        private int _userId;
        private DatabaseService _dbService = new DatabaseService();

        public EditUserPage(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadUserData();
        }

        private void LoadUserData()
        {
            var user = _dbService.GetUsers().FirstOrDefault(u => u.UserId == _userId);
            if (user != null)
            {
                UsernameBox.Text = user.Username;
                RoleComboBox.SelectedIndex = user.Role == "Admin" ? 1 : 0;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text.Trim();
            var password = PasswordBox.Password;
            var role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var hashedPassword = _dbService.HashPassword(password);

            var updatedUser = new User
            {
                UserId = _userId,
                Username = username,
                PasswordHash = hashedPassword,
                Role = role
            };

            _dbService.UpdateUser(updatedUser);
            MessageBox.Show("Пользователь успешно обновлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService?.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}