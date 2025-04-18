using PersonnelAccessSystem.Services;
using System.Windows;
using System.Windows.Controls;

namespace PersonnelAccessSystem.Pages
{
    public partial class AccessCardsPage : Page
    {
        private DatabaseService _dbService = new DatabaseService();

        public AccessCardsPage()
        {
            InitializeComponent();
            LoadAccessCards();
        }

        private void LoadAccessCards()
        {
            var cards = _dbService.GetAccessCards();
            AccessCardsGrid.ItemsSource = cards;
        }

        private void EditCard_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cardId = (int)button.Tag;
            NavigationService?.Navigate(new EditPersonPage(cardId)); // Передаем только cardId
        }

        private void DeleteCard_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cardId = (int)button.Tag;

            var result = MessageBox.Show("Вы уверены, что хотите удалить эту карту доступа?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _dbService.DeleteAccessCard(cardId);
                LoadAccessCards(); // Обновляем список пропусков
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}