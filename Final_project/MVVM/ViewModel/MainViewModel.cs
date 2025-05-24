using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_project_wpf.Core;

namespace Final_project_wpf.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public TranslatorViewModel TransVM { get; set; }

        private object _currentView; 
        public object CurrentView
        {
            get { return _currentView; }
            set { 
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            TransVM = new TranslatorViewModel();
            CurrentView = TransVM;
        }
    }
}
