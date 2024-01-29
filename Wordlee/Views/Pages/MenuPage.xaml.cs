using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wordlee.Services;
using Wordlee.ViewModels;

namespace Wordlee.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private void OnStart()
        {
            InitializeComponent();
            Application.Current.MainWindow.Width = 600;
            Application.Current.MainWindow.Height = 350;
            DataContext = new WordViewModel();
        }

        public MenuPage(int userId)
        {
            OnStart();
        }

        public MenuPage()
        {
            OnStart();
            spLvl.SetValue(Grid.ColumnSpanProperty, 2);
            spResults.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => MyFrame.Exit();
    }
}
