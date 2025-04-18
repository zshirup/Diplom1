using PersonnelAccessSystem.Pages;
using System.Windows;

namespace PersonnelAccessSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Pages.LoginPage());
        }
    }
}