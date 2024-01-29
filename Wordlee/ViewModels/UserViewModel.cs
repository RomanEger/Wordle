using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordlee.Commands;
using Wordlee.DataBase;
using Wordlee.Models;
using Wordlee.Services;
using Wordlee.Views.Pages;

namespace Wordlee.ViewModels
{
    class UserViewModel : ViewModelBase
    {
        private User _thisUser;
        public User ThisUser
        {
            get => _thisUser;
            set
            {
                _thisUser = value;
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
        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        User? q = DbClass.entities.Users.FirstOrDefault(x => x.Login == ThisUser.Login && x.Password == ThisUser.Password) ?? null;
                        if (q != null)
                        {
                            MyFrame.Navigate(new MenuPage(q.Id));
                            ThisUser = new User();
                        }
                        else
                        {
                            Error = "Неправильный логин или пароль";
                            ThisUser.Password = null;
                        }
                    }
                    catch
                    {
                        //
                    }
                });
            }
        }

        private RelayCommand _guestCommand;
        public RelayCommand GuestCommand
        {
            get
            {
                return _guestCommand ??= new RelayCommand(obj =>
                {
                    MyFrame.Navigate(new MenuPage());
                    ThisUser = new User();
                });
            }
        }

        private RelayCommand _registrationCommand;
        public RelayCommand RegistrationCommand
        {
            get
            {
                return _registrationCommand ??= new RelayCommand(obj => { });
            }
        }

        public UserViewModel()
        {
            ThisUser = new User();
        }
    }
}
