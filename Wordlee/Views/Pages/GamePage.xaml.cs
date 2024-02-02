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
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        int row = 0;
        WordViewModel _vm;

        public GamePage(WordViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            _vm = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => MyFrame.Exit();

        private void ButtonBack_Click(object sender, RoutedEventArgs e) => MyFrame.frame.GoBack();

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            switch (row)
            {
                case 0:
                    {
                        tb00.Text = tb.Text[0].ToString();
                        tb01.Text = tb.Text[1].ToString();
                        tb02.Text = tb.Text[2].ToString();
                        tb03.Text = tb.Text[3].ToString();
                        tb04.Text = tb.Text[4].ToString();
                        break;
                    }
                case 1:
                    {
                        tb10.Text = tb.Text[0].ToString();
                        tb11.Text = tb.Text[1].ToString();
                        tb12.Text = tb.Text[2].ToString();
                        tb13.Text = tb.Text[3].ToString();
                        tb14.Text = tb.Text[4].ToString();
                        break;
                    }
                case 2:
                    {
                        tb20.Text = tb.Text[0].ToString();
                        tb21.Text = tb.Text[1].ToString();
                        tb22.Text = tb.Text[2].ToString();
                        tb23.Text = tb.Text[3].ToString();
                        tb24.Text = tb.Text[4].ToString();
                        break;
                    }
                case 3:
                    {
                        tb30.Text = tb.Text[0].ToString();
                        tb31.Text = tb.Text[1].ToString();
                        tb32.Text = tb.Text[2].ToString();
                        tb33.Text = tb.Text[3].ToString();
                        tb34.Text = tb.Text[4].ToString();
                        break;
                    }
                case 4:
                    {
                        tb40.Text = tb.Text[0].ToString();
                        tb41.Text = tb.Text[1].ToString();
                        tb42.Text = tb.Text[2].ToString();
                        tb43.Text = tb.Text[3].ToString();
                        tb44.Text = tb.Text[4].ToString();
                        break;
                    }
            }
        }

        private int Contains(char c, int index)
        {
            if (!_vm.SelectedWord.Contains(c))
                return 0;
            else
            {
                if (_vm.SelectedWord.)
                    return 2;
                return 1;
            }
        }
    }
}
