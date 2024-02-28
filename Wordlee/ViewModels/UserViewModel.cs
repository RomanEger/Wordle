using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Wordlee.Commands;
using Wordlee.DataBase;
using Wordlee.Models;
using Wordlee.Services;
using Wordlee.Views.Pages;

namespace Wordlee.ViewModels
{
    class UserViewModel : ViewModelBase
    {

        private string _secondPassword;
        public string SecondPassword
        {
            get => _secondPassword;
            set
            {
                _secondPassword = value;
                OnPropertyChanged();
            }
        }

        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _guestCommand;
        public RelayCommand GuestCommand
        {
            get
            {
                return _guestCommand ??= new RelayCommand(obj =>
                {
                    var vm = new WordViewModel();
                    vm.StartCommand.Execute(null);
                    MyFrame.Navigate(new GamePage(vm));
                });
            }
        }
    }
}
