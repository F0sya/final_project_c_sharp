using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Final_project_wpf.Core;

namespace Final_project_wpf.MVVM.ViewModel
{
    class TranslatorViewModel : INotifyPropertyChanged
    {
        private string _inputText;
        private string _outputText;
        private readonly TranslatorService _translatorService;

        public ICommand TranslateCommand { get; }

        public TranslatorViewModel()
        {
            _translatorService = new TranslatorService();
            TranslateCommand = new RelayCommand(o => Translate());
        }

        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }

        public string OutputText
        {
            get { return _outputText; }
            set
            {
                _outputText = value;
                OnPropertyChanged();
            }
        }

        private void Translate()
        {
            if (string.IsNullOrWhiteSpace(InputText))
                return;

            try
            {
                OutputText = _translatorService.TranslateText(InputText,"uk");
            }
            catch (Exception ex)
            {
                OutputText = $"Translation error: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}