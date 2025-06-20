using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Final_project_wpf.Core;
using Final_project_wpf.Infrastructure.Models;

namespace Final_project_wpf.MVVM.ViewModel
{
    class TranslatorViewModel : INotifyPropertyChanged
    {
        private string _inputText;
        private string _outputText;
        private readonly TranslatorService _translatorService;
        private readonly SupabaseService _supabaseService;

        public ICommand TranslateCommand { get; }

        public TranslatorViewModel()
        {
            _translatorService = new TranslatorService();
            TranslateCommand = new RelayCommand(o => Translate());

            string supabaseUrl = "https://jeszlpmipprqfwmlreao.supabase.co";
            string supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Implc3pscG1pcHBycWZ3bWxyZWFvIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDgxMjkwMTQsImV4cCI6MjA2MzcwNTAxNH0.mTwaMpfbOheO-YkxbIw7pCi3QdN8lD_2-39IRoYjVEQ";
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseService = new SupabaseService(supabaseUrl, supabaseKey, options);
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

        private async Task Translate()
        {
            if (string.IsNullOrWhiteSpace(InputText))
                return;


            try
            {
                await _supabaseService.InitializeAsync();
                var existing = await _supabaseService.GetTranslationByWordAsync(InputText);

                if (existing != null)
                {
                    OutputText = existing.Translation;
                }
                else
                {
                    OutputText = _translatorService.TranslateText(InputText, "uk");
                    var data = new TranslationTable
                    {
                        Word = InputText,
                        Translation = OutputText,
                        Sentence = "",
                        TranslatedSentence = ""
                    };
                    await _supabaseService.InsertTranslationAsync(data);
                }
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