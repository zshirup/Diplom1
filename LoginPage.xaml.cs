using PersonnelAccessSystem.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PersonnelAccessSystem.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var authService = new AuthService();
            var result = authService.Authenticate(UsernameBox.Text, PasswordBox.Password);

            if (result.IsAuthenticated)
            {
                
                NavigationService?.Navigate(new HomePage(result.Role));
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}