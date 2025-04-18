using PersonnelAccessSystem.Services;
using System.Windows;
using System.Windows.Controls;

namespace PersonnelAccessSystem.Pages
{
    public partial class LogsPage : Page
    {
        private DatabaseService _dbService = new DatabaseService();

        public LogsPage()
        {
            InitializeComponent();
            LoadLogs();
        }

        private void LoadLogs()
        {
            var logs = _dbService.GetLogEntries();
            LogsGrid.ItemsSource = logs;
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