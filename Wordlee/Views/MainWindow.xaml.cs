using System.Windows;
using Wordlee.DataBase;
using Wordlee.Models;
using Wordlee.Services;
using Wordlee.ViewModels;
using Wordlee.Views.Pages;

namespace Wordlee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.CanMinimize;
            DbClass.entities = new RepositoryContext();
            MyFrame.frame = MainFrame;
            WordViewModel viewModel = new WordViewModel();
            viewModel.StartCommand.Execute(0);
        }
    }
}
