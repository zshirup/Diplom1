using PersonnelAccessSystem.Models;
using PersonnelAccessSystem.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PersonnelAccessSystem.Pages
{
    public partial class ManageUsersPage : Page
    {
        private AuthService _authService = new AuthService();

        public ManageUsersPage()
        {
            InitializeComponent();
        }

        // Создание нового пользователя
        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем значения из полей
                string username = UsernameBox.Text?.Trim();
                string password = PasswordBox.Password; // Используем свойство Password
                string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

                // Проверяем обязательные поля
                if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(role))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Хэшируем пароль
                string hashedPassword = _authService.HashPassword(password);

                // Создаем нового пользователя
                var user = new User
                {
                    Username = username,
                    PasswordHash = hashedPassword,
                    Role = role == "Администратор" ? "Admin" : "User"
                };

                // Сохраняем пользователя в базе данных
                var dbService = new DatabaseService();
                dbService.AddUser(user);

                MessageBox.Show("Пользователь успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Очищаем поля
                UsernameBox.Text = "";
                PasswordBox.Clear();
                RoleComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Возвращение на предыдущую страницу
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}