using PersonnelAccessSystem.Services;
using System.Windows;
using System.Windows.Controls;

namespace PersonnelAccessSystem.Pages
{
    public partial class DatabasePage : Page
    {
        private DatabaseService _dbService = new DatabaseService();

        public DatabasePage()
        {
            InitializeComponent();
            LoadAccessCards();
        }

        private void LoadAccessCards()
        {
            var cards = _dbService.GetAccessCards();
            AccessCardsGrid.ItemsSource = cards;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cardId = (int)button.Tag;
            NavigationService?.Navigate(new EditPersonPage(cardId));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cardId = (int)button.Tag;

            var result = MessageBox.Show("Вы уверены, что хотите удалить эту карту доступа?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _dbService.DeleteAccessCard(cardId);
                LoadAccessCards();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}