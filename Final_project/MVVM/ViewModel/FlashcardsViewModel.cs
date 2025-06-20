using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Final_project_wpf.Core;
using Final_project_wpf.Infrastructure.Models;
using Final_project_wpf.MVVM.Model;

namespace Final_project_wpf.MVVM.ViewModel
{
    public class FlashcardsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Flashcard> _flashcards;
        private Flashcard _currentCard;
        private int _currentIndex;
        private readonly SupabaseService _supabaseService;
        public ICommand GenerateExampleWithAICommand { get; }

        private readonly SimpleTextGenerator _simpleTextGenerator;
        private readonly TranslatorService _translatorService;

        public FlashcardsViewModel()
        {
            string supabaseUrl = "https://jeszlpmipprqfwmlreao.supabase.co";
            string supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Implc3pscG1pcHBycWZ3bWxyZWFvIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDgxMjkwMTQsImV4cCI6MjA2MzcwNTAxNH0.mTwaMpfbOheO-YkxbIw7pCi3QdN8lD_2-39IRoYjVEQ";
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _simpleTextGenerator = new SimpleTextGenerator("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");
            _supabaseService = new SupabaseService(supabaseUrl, supabaseKey, options);
            _translatorService = new TranslatorService();
            Flashcards = new ObservableCollection<Flashcard>();
            FlipCardCommand = new RelayCommand(o => FlipCard());
            NextCardCommand = new RelayCommand(o => NextCard());
            PreviousCardCommand = new RelayCommand(o => PreviousCard());
            AddCardCommand = new RelayCommand(o => AddCard());
            GenerateExampleWithAICommand = new AsyncRelayCommand(async o => await GenerateExampleSentenceAsync());


            LoadFlashcardsAsync();
        }
        private async Task GenerateExampleSentenceAsync()
        {
            if (CurrentCard != null && !string.IsNullOrWhiteSpace(CurrentCard.FrontText))
            {
                try
                {
                    string example = await _simpleTextGenerator.GenerateText(CurrentCard.FrontText);
                    if (example == "0")
                    {
                        MessageBox.Show("AI is too busy right now. Try later", "AI Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        string updateWord = CurrentCard.FrontText;
                        CurrentCard.FrontText = example;
                        CurrentCard.BackText = _translatorService.TranslateText(CurrentCard.FrontText, "uk"); // Translate the example
                        CurrentCard.IsFlipped = false; // Reset flip state to show new example
                        await _supabaseService.UpdateTranslationAsync(updateWord, CurrentCard.FrontText, CurrentCard.BackText);
                        OnPropertyChanged(nameof(CurrentCard));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error generating example: {ex.Message}", "AI Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private async void LoadFlashcardsAsync()
        {
            await _supabaseService.InitializeAsync();
            var translations = await _supabaseService.GetAllTranslationsAsync();
            Flashcards.Clear();
            foreach (var t in translations)
            {
                Flashcards.Add(new Flashcard
                {
                    FrontText = t.Word,
                    BackText = t.Translation,
                    IsFlipped = false
                });
            }
            if (Flashcards.Count > 0)
            {
                CurrentCard = Flashcards[0];
                _currentIndex = 0;
            }
        }
        public ObservableCollection<Flashcard> Flashcards
        {
            get => _flashcards;
            set
            {
                _flashcards = value;
                OnPropertyChanged();
            }
        }

        public Flashcard CurrentCard
        {
            get => _currentCard;
            set
            {
                _currentCard = value;
                OnPropertyChanged();
            }
        }

        public ICommand FlipCardCommand { get; }
        public ICommand NextCardCommand { get; }
        public ICommand PreviousCardCommand { get; }
        public ICommand AddCardCommand { get; }

        private void FlipCard()
        {
            if (CurrentCard != null)
            {
                CurrentCard.IsFlipped = !CurrentCard.IsFlipped;
                OnPropertyChanged(nameof(CurrentCard));
            }
        }

        private void NextCard()
        {
            if (Flashcards.Count > 0)
            {
                _currentIndex = (_currentIndex + 1) % Flashcards.Count;
                CurrentCard = Flashcards[_currentIndex];
            }
        }

        private void PreviousCard()
        {
            if (Flashcards.Count > 0)
            {
                _currentIndex = (_currentIndex - 1 + Flashcards.Count) % Flashcards.Count;
                CurrentCard = Flashcards[_currentIndex];
            }
        }

        private void AddCard()
        {
            var newCard = new Flashcard
            {
                FrontText = "",
                BackText = "",
                IsFlipped = false
            };
            Flashcards.Add(newCard);

            if (CurrentCard == null)
            {
                CurrentCard = newCard;
                _currentIndex = Flashcards.Count - 1;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
