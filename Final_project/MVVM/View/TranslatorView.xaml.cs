using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Final_project_wpf.Core;

namespace Final_project_wpf.MVVM.View
{
    /// <summary>
    /// Interaction logic for TranslatorView.xaml
    /// </summary>
    public partial class TranslatorView : UserControl
    {
        public TranslatorView()
        {
            InitializeComponent();
        }

        private void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredText = InputTextBox.Text;
            try
            {
                TranslatorService translatorService = new TranslatorService();
                string translatedText = translatorService.TranslateText(enteredText, "en");
                OutputTextBox.Text = translatedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while translating: {ex.Message}");
            }
        }


    }
}
