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
            Application.Current.MainWindow.Width = 600;
            Application.Current.MainWindow.Height = 350;
            var vm = new WordViewModel();
            DataContext = vm;
            try
            {
                vm.SelectedIdWord = Convert.ToInt32(ComboBox.SelectedItem);
            }
            catch
            {
                vm.SelectedIdWord = 1;
            }
        }

        public MenuPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => MyFrame.Exit();

        private void MenuPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            if (CurrentUser.UserId == null)
            {
                spResults.Visibility = Visibility.Hidden;
                spLvl.SetValue(Grid.ColumnSpanProperty, 2);
            }
            OnStart();
        }
    }
}
