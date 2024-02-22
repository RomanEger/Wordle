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
        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??= new RelayCommand(async obj =>
                {
                    try
                    {
                        var q = await DbClass.entities.Users.FirstOrDefaultAsync(x => x.Login == ThisUser.Login && x.Password == ThisUser.Password) ?? null;
                        if (q != null)
                        {
                            CurrentUser.UserId = q.Id;
                            MyFrame.Navigate(new MenuPage());
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
                        MessageBox.Show("Проверьте подключение к интернету");
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
                    var vm = new WordViewModel();
                    vm.StartCommand.Execute(null);
                    ThisUser = new User();
                });
            }
        }

        private RelayCommand _registrationCommand;
        public RelayCommand RegistrationCommand
        {
            get
            {
                return _registrationCommand ??= new RelayCommand(obj => 
                {
                    MyFrame.Navigate(new RegistrationPage());
                });
            }
        }

        private RelayCommand _createAccountCommand;
        public RelayCommand CreateAccountCommand
        {
            get
            {
                return _createAccountCommand ??= new RelayCommand(async obj =>
                {
                    try
                    {
                        if (await DbClass.entities.Users.AnyAsync(x => x.Login == ThisUser.Login))
                        {
                            Error = $"Этот логин ({ThisUser.Login}) уже занят, попробуйте еще раз.";
                            ThisUser.Login = null;
                            ThisUser.Password = null;
                            SecondPassword = null;
                        }
                        else
                        {
                            await DbClass.entities.Users.AddAsync(ThisUser);
                            await DbClass.entities.SaveChangesAsync();
                            MessageBox.Show("Аккаунт успешно создан!");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Проверьте подключение к интернету");
                    }
                });
            }
        }

        public UserViewModel()
        {
            ThisUser = new User();
        }
    }
}
