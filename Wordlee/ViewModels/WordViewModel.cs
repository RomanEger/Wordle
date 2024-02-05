using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
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

        
        private static IEnumerable<Word> _listWords;
        public IEnumerable<Word> ListWords
        {
            get => _listWords;
            set
            {
                _listWords = value;
                OnPropertyChanged();
            }
        }

        private static List<int> _idWords;
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
                SelectedWord = ListWords.FirstOrDefault(x => x.Id == value)?.WordName;
                OnPropertyChanged();
            }
        }

        public WordViewModel()
        {
            Start();
            IsReadOnly = false;
        }

        private async void Start()
        {
            if(_listWords == null || !_listWords.Any())
                await GetWords();
            if(CurrentUser.UserId == null) return;
            await InitResolveds();
        }

        private async Task GetWords()
        {
            ListWords = DbClass.entities.Words.AsEnumerable();
            IdWords = new(ListWords.Count());
            foreach (var word in _listWords)
            {
                IdWords.Add(word.Id);
            }
        }

        private async Task InitResolveds()
        {
            try
            {
                Resolveds = new ObservableCollection<int>(await DbClass.entities.Resolveds.
                    Where(x => x.UserId == CurrentUser.UserId.Value).
                    Select(x => x.WordId).ToListAsync());
            }
            catch
            {
                MessageBox.Show("Проверьте подключение к интернету");
            }
        }

        public WordViewModel(List<Word> listWords, int selectedIdWord)
        {
            ListWords = listWords;
            IdWords = new(ListWords.Count());
            foreach (var word in _listWords)
                IdWords.Add(word.Id);

            SelectedIdWord = selectedIdWord;

            if (CurrentUser.UserId == null) return;
            InitResolveds();
            IsReadOnly = false;
            
        }

        private RelayCommand _startCommand;
        public RelayCommand StartCommand =>_startCommand ??= new RelayCommand(obj => MyFrame.Navigate(new GamePage(this)));

        private RelayCommand _enterCommand;
        public RelayCommand EnterCommand
        {
            get
            {
                return _enterCommand ??= new RelayCommand(async obj =>
                {
                    if (SelectedWord == Word)
                    {
                        SetLetters();
                        SetBColors();
                        MessageBox.Show("Победа!");
                        if (CurrentUser.UserId != null)
                        {
                            Resolved resolved = new()
                            {
                                UserId = CurrentUser.UserId.Value,
                                WordId = SelectedIdWord
                            };
                            if(Resolveds.Contains(SelectedIdWord))
                                return;
                            try
                            {
                                await DbClass.entities.Resolveds.AddAsync(resolved);
                                await DbClass.entities.SaveChangesAsync();
                                Resolveds.Add(resolved.WordId);
                            }
                            catch
                            {
                                MessageBox.Show("Проверьте подключение к интернету");
                            }
                        }

                        IsReadOnly = true;
                        return;
                    }

                    if (ListWords.All(x => !string.Equals(x.WordName, Word, StringComparison.CurrentCultureIgnoreCase))) return;

                    SetLetters();
                    SetBColors();
                    if (_counter > 4)
                    {
                        IsReadOnly = true;
                        MessageBox.Show("Загаданное слово - "+SelectedWord);
                    }
                });
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

        private void SetLetters()
        {
            switch (_counter)
            {
                case 0:
                {
                    L00 = Word[0].ToString();
                    L01 = Word[1].ToString();
                    L02 = Word[2].ToString();
                    L03 = Word[3].ToString();
                    L04 = Word[4].ToString();
                    break;
                }
                case 1:
                {
                    L10 = Word[0].ToString();
                    L11 = Word[1].ToString();
                    L12 = Word[2].ToString();
                    L13 = Word[3].ToString();
                    L14 = Word[4].ToString();
                    break;
                }
                case 2:
                {
                    L20 = Word[0].ToString();
                    L21 = Word[1].ToString();
                    L22 = Word[2].ToString();
                    L23 = Word[3].ToString();
                    L24 = Word[4].ToString();
                    break;
                }
                case 3:
                {
                    L30 = Word[0].ToString();
                    L31 = Word[1].ToString();
                    L32 = Word[2].ToString();
                    L33 = Word[3].ToString();
                    L34 = Word[4].ToString();
                    break;
                }
                case 4:
                {
                    L40 = Word[0].ToString();
                    L41 = Word[1].ToString();
                    L42 = Word[2].ToString();
                    L43 = Word[3].ToString();
                    L44 = Word[4].ToString();
                    break;
                }
            }
        }

        private Brush brush;
        
        private void SetBColors()
        {
            var dictY = new Dictionary<int, char>();
            var dictG = new Dictionary<int, char>();
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
                    dictG.Add(item.Key, item.Value);
                    dict.Remove(item.Key);
                    nDict.Remove(item.Key);
                }
            }

            foreach (var item in dict)
            {
                if (nDict.ContainsValue(item.Value))
                {
                    dictY.Add(item.Key, item.Value);
                    dict.Remove(item.Key);
                    nDict.Remove(nDict.First(x => x.Value == item.Value).Key);
                }
            }

            foreach (var item in dictG)
            {
                brush = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                var s = $"BColor{_counter}{item.Key}";
                typeof(WordViewModel).GetProperty(s)?.SetValue(this, brush);
            }

            foreach (var item in dictY)
            {
                brush = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                var s = $"BColor{_counter}{item.Key}";
                typeof(WordViewModel).GetProperty(s)?.SetValue(this, brush);
            }

            Word = string.Empty;

            _counter++;
        }

        public ObservableCollection<int> Resolveds { get; 
            set; }

    }
}
