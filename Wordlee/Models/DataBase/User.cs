using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordlee.ViewModels;

namespace Wordlee.DataBase
{
    class User : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _login;
        public string Login
        { 
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            } 
        }
        private string _password;
        public string Password 
        { 
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public List<Resolved> Resolveds { get; set; } = new List<Resolved>();
    }
}
