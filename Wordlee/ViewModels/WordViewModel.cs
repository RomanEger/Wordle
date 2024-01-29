using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordlee.DataBase;
using Wordlee.Models;

namespace Wordlee.ViewModels
{
    class WordViewModel : ViewModelBase
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
    }
}
