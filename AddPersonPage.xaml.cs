using PersonnelAccessSystem.Models;
using PersonnelAccessSystem.Services;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System;

namespace PersonnelAccessSystem.Pages
{
    public partial class AddPersonPage : Page
    {
        private string _photoPath;
        private DatabaseService _dbService = new DatabaseService();

        public AddPersonPage()
        {
            InitializeComponent();
        }

        // Обработчик выбора уровня доступа
        private void AccessLevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Проверяем, что SelectedItem существует и является ComboBoxItem
                if (AccessLevelComboBox?.SelectedItem is ComboBoxItem selectedItem && selectedItem.Content != null)
                {
                    var selectedValue = selectedItem.Content.ToString();

                    // Проверяем, что EndDatePanel существует
                    if (EndDatePanel != null)
                    {
                        EndDatePanel.Visibility = selectedValue == "Temporary" ? Visibility.Visible : Visibility.Collapsed;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Загрузка фото
        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _photoPath = openFileDialog.FileName;
                PhotoPreview.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri(_photoPath));
                PhotoPreview.Visibility = Visibility.Visible;
                MessageBox.Show("Фото успешно загружено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Добавление человека
        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            var firstName = FirstNameBox.Text;
            var lastName = LastNameBox.Text;
            var birthDate = BirthDatePicker.SelectedDate ?? DateTime.Now;
            var accessLevel = ((ComboBoxItem)AccessLevelComboBox.SelectedItem)?.Content?.ToString();
            var endDate = accessLevel == "Temporary" ? EndDatePicker.SelectedDate : null;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (accessLevel == "Temporary" && endDate == null)
            {
                MessageBox.Show("Для временного пропуска необходимо указать дату окончания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newCard = new AccessCard
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                AccessLevel = accessLevel,
                PhotoPath = _photoPath,
                EndDate = endDate
            };

            _dbService.AddAccessCard(newCard);
            _dbService.LogAction($"Добавлен человек: {firstName} {lastName}");

            MessageBox.Show("Человек успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            ClearFields();
        }

        // Очистка полей
        private void ClearFields()
        {
            FirstNameBox.Text = "";
            LastNameBox.Text = "";
            BirthDatePicker.SelectedDate = null;
            AccessLevelComboBox.SelectedIndex = 0;
            EndDatePanel.Visibility = Visibility.Collapsed;
            EndDatePicker.SelectedDate = null;
            _photoPath = null;
            PhotoPreview.Visibility = Visibility.Collapsed;
        }

        // Возврат назад
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}