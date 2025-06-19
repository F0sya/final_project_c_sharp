using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Final_project_wpf.Core;
using Final_project_wpf.Core.Interfaces;

namespace Final_project_wpf.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand TranslatorViewCommand { get; set; }
        public RelayCommand FlashcardsViewCommand { get; set; }
        public TranslatorViewModel TranslatorVM { get; set; }
        public FlashcardsViewModel FlashcardsVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }




        public MainViewModel()
        {
            TranslatorVM = new TranslatorViewModel();
            FlashcardsVM = new FlashcardsViewModel();

            CurrentView = TranslatorVM;

            // Initialize commands
            TranslatorViewCommand = new RelayCommand(o =>
            {
                CurrentView = TranslatorVM;
                
            });

            FlashcardsViewCommand = new RelayCommand(o =>
            {
                CurrentView = FlashcardsVM;
                
            });
        }
    }
}