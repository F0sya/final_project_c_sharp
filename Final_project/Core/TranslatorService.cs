using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;

namespace Final_project_wpf.Core
{
    public class TranslatorService
    {
        private readonly TranslationClient _client;

        public TranslatorService()
        {
            _client = TranslationClient.CreateFromApiKey("AIzaSyAVLZJdbYjVieSqk8hQK3m7P76oLHRDNOk");
        }

        public string TranslateText(string text, string targetLanguage)
        {
            var response = _client.TranslateText(text, targetLanguage);
            return response.TranslatedText;
        }
    }
}