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
    public class WordViewModel : ViewModelBase
    {
        private string _selectedWord;
        public string SelectedWord
        {
            get => _selectedWord;
            set
            {
                _selectedWord = value;
                OnPropertyChanged();
            }
        }

        private string _word;
        public string Word
        {
            get => _word;
            set
            {
                _word = value;
                OnPropertyChanged();
            }
        }



        private List<Word> _listWords;
        public List<Word> ListWords
        {
            get => _listWords;
            set
            {
                _listWords = value;
                OnPropertyChanged();
            }
        }

        private List<int> _idWords;
        public List<int> IdWords
        {
            get => _idWords;
            set
            {
                _idWords = value;
                OnPropertyChanged();
            }
        }

        private int _selectedIdWord;
        public int SelectedIdWord
        {
            get => _selectedIdWord;
            set
            {
                _selectedIdWord = value;
                SelectedWord = ListWords.FirstOrDefault(x => x.Id == value).WordName;
                OnPropertyChanged();
            }
        }

        public WordViewModel()
        {
            ListWords = DbClass.entities.Words.ToList();
            IdWords = new(ListWords.Capacity);
            foreach (var word in _listWords)
            {
                IdWords.Add(word.Id);
            }
        }

        private RelayCommand _startCommand;
        public RelayCommand StartCommand =>_startCommand ??= new RelayCommand(obj => MyFrame.Navigate(new GamePage(this)));

        private RelayCommand _enterCommand;
        public RelayCommand EnterCommand
        {
            get
            {
                return _enterCommand ??= new RelayCommand(obj =>
                {
                    Word = string.Empty;
                });
            }
        }
    }
}
