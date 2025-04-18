using PersonnelAccessSystem.Models;
using PersonnelAccessSystem.Services;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PersonnelAccessSystem.Pages
{
    public partial class EditPersonPage : Page
    {
        private int _cardId;
        private DatabaseService _dbService = new DatabaseService();

        public EditPersonPage(int cardId)
        {
            InitializeComponent();
            _cardId = cardId;
            LoadCardData();
        }

        private void LoadCardData()
        {
            var card = _dbService.GetAccessCards().FirstOrDefault(c => c.CardId == _cardId);
            if (card != null)
            {
                FirstNameBox.Text = card.FirstName;
                LastNameBox.Text = card.LastName;
                BirthDatePicker.SelectedDate = card.BirthDate;
                AccessLevelComboBox.SelectedIndex = card.AccessLevel == "Temporary" ? 0 : 1;
                EndDatePanel.Visibility = card.AccessLevel == "Temporary" ? Visibility.Visible : Visibility.Collapsed;
                EndDatePicker.SelectedDate = card.EndDate;
            }
        }

        private void AccessLevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что SelectedItem существует и является ComboBoxItem
            if (AccessLevelComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                // Получаем текст выбранного элемента
                var selectedValue = selectedItem.Content?.ToString();

                // Проверяем, что EndDatePanel существует
                if (EndDatePanel != null)
                {
                    // Обновляем видимость панели EndDatePanel
                    EndDatePanel.Visibility = selectedValue == "Temporary" ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var firstName = FirstNameBox.Text;
            var lastName = LastNameBox.Text;
            var birthDate = BirthDatePicker.SelectedDate ?? DateTime.Now;
            var accessLevel = ((ComboBoxItem)AccessLevelComboBox.SelectedItem).Content.ToString();
            var endDate = accessLevel == "Temporary" ? EndDatePicker.SelectedDate : null;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var updatedCard = new AccessCard
            {
                CardId = _cardId,
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                AccessLevel = accessLevel,
                EndDate = endDate
            };

            _dbService.UpdateAccessCard(updatedCard);
            _dbService.LogAction($"Обновлен человек: {firstName} {lastName}");
            MessageBox.Show("Запись успешно обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            NavigationService?.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}