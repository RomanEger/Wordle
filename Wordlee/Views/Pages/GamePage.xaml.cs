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
using Wordlee.DataBase;
using Wordlee.Services;
using Wordlee.ViewModels;

namespace Wordlee.Views.Pages
{
    public partial class GamePage : Page
    {
        public GamePage(WordViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => MyFrame.Exit();

        private void ButtonBack_Click(object sender, RoutedEventArgs e) => MyFrame.frame.GoBack();

        private void GamePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Box.Focus();
        }
    }
}
