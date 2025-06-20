using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HuggingFace;

namespace Final_project_wpf.Core
{
    public class SimpleTextGenerator
    {
        private readonly HuggingFaceClient _client;

        public SimpleTextGenerator(string apiKey)
        {
            _client = new HuggingFaceClient(apiKey);
        }

        public async Task<string> GenerateText(string word)
        {
            try
            {
                var response = await _client.GenerateTextAsync(
                    "meta-llama/Llama-3.1-8B-Instruct",  // Use a simpler model for testing
                    request: new GenerateTextRequest
                    {
                        Inputs = $"Generate exactly one 5-8 word sentence containing '{word}'. Return ONLY the sentence.",
                        Parameters = new GenerateTextRequestParameters
                        {
                            MaxNewTokens = 20,
                            Temperature = 0.7,
                            NumReturnSequences = 1,
                            TopP = 0.9
                        }
                    });
                

                var rawText = response.First().GeneratedText;
                return CleanResponse(rawText,word);
            }
            catch
            {
                return "0";
            }
        }

        private string CleanResponse(string text, string word)
        {
            // Remove quotation marks and AI prefixes
            // Take only the first sentence before any period
            var firstSentence = text.Replace($"Generate exactly one 5-8 word sentence containing '{word}'. Return ONLY the sentence.","").Split('.')[0].Trim();

            // Remove any remaining quotes or brackets
            return firstSentence
                .Replace("\"", "")
                .Replace("'", "")
                .Replace("(", "")
                .Replace(")", "");
        }
    }
}