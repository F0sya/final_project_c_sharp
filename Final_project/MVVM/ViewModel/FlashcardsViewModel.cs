using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Final_project_wpf.Core;
using Final_project_wpf.MVVM.Model;

namespace Final_project_wpf.MVVM.ViewModel
{
    public class FlashcardsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Flashcard> _flashcards;
        private Flashcard _currentCard;
        private int _currentIndex;

        public FlashcardsViewModel()
        {
            Flashcards = new ObservableCollection<Flashcard>();
            FlipCardCommand = new RelayCommand(o => FlipCard());
            NextCardCommand = new RelayCommand(o => NextCard());
            PreviousCardCommand = new RelayCommand(o => PreviousCard());
            AddCardCommand = new RelayCommand(o => AddCard());

            // Sample data - remove in production
            AddCard();
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
                FrontText = "New Word",
                BackText = "Translation",
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
