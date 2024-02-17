using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using Wordlee.Commands;
using Wordlee.DataBase;
using Wordlee.Models;
using Wordlee.Services;
using Wordlee.Views.Pages;

namespace Wordlee.ViewModels
{
    public partial class WordViewModel : ViewModelBase
    {
        private Brush _brush;

        private int _counter;

        private string _selectedWord;
        public string SelectedWord
        {
            get => _selectedWord;
            set
            {
                _selectedWord = value?.ToUpper();
                OnPropertyChanged();
            }
        }

        private string _word;
        public string Word
        {
            get => _word;
            set
            {
                _word = value?.ToUpper();
                OnPropertyChanged();
            }
        }

        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                OnPropertyChanged();
            }
        }

        public WordViewModel()
        {
            IsReadOnly = false;
        }

        public RelayCommand StartCommand =>
            new (_ =>
            {
                try
                {
                    var obj = DbClass.entities.Words.FromSqlRaw("SELECT TOP 1 * FROM Word ORDER BY NEWID()").First();
                    SelectedWord = obj.WordName;
                    MyFrame.Navigate(new GamePage(this));
                }
                catch
                {
                    MessageBox.Show("Проверьте подключение к интернету");
                }
            });

        public RelayCommand EnterCommand =>
            new (_ =>
            {

                if (SelectedWord == Word)
                {
                    SetLetters();
                    SetBColors();
                    MessageBox.Show("Победа!");
                    IsReadOnly = true;
                    return;
                }

                if(!DbClass.entities.Words.Any(x => x.WordName == Word)) return;
                SetLetters();
                SetBColors();
                if (_counter > 4)
                {
                    IsReadOnly = true;
                    MessageBox.Show("Загаданное слово - "+SelectedWord);
                }
            });

        private void SetLetters()
        {
            for (int i = 0; i < 5; i++)
            {
                var s = $"L{_counter}{i}";
                typeof(WordViewModel).GetProperty(s)?.SetValue(this, Word[i].ToString());
            }
        }

        private void SetBColors()
        {
            var dictYellow = new Dictionary<int, char>();
            var dictGreen = new Dictionary<int, char>();
            var nDict = new Dictionary<int, char>();
            var dict = new Dictionary<int, char>();

            for (int i = 0; i < SelectedWord.Length; i++)
            {
                nDict.Add(i, SelectedWord[i]);
                dict.Add(i, Word[i]);
            }

            foreach (var item in dict)
            {
                if (item.Value == nDict[item.Key])
                {
                    dictGreen.Add(item.Key, item.Value);
                    dict.Remove(item.Key);
                    nDict.Remove(item.Key);
                }
            }

            foreach (var item in dict)
            {
                if (nDict.ContainsValue(item.Value))
                {
                    dictYellow.Add(item.Key, item.Value);
                    dict.Remove(item.Key);
                    nDict.Remove(nDict.First(x => x.Value == item.Value).Key);
                }
            }

            foreach (var item in dictGreen)
            {
                _brush = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                var s = $"BColor{_counter}{item.Key}";
                typeof(WordViewModel).GetProperty(s)?.SetValue(this, _brush);
            }

            foreach (var item in dictYellow)
            {
                _brush = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                var s = $"BColor{_counter}{item.Key}";
                typeof(WordViewModel).GetProperty(s)?.SetValue(this, _brush);
            }

            foreach (var item in dict)
            {
                _brush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
                var s = $"BColor{_counter}{item.Key}";
                typeof(WordViewModel).GetProperty(s)?.SetValue(this, _brush);
            }

            Word = string.Empty;

            _counter++;
        }
    }
}
