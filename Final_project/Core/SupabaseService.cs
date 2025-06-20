using Supabase;
using System.Threading.Tasks;
using Final_project_wpf.Infrastructure.Models;

namespace Final_project_wpf.Core
{
    public class SupabaseService
    {
        private readonly Client _client;

        public SupabaseService(string url, string key, SupabaseOptions options)
        {
            _client = new Client(url, key, options);
        }

        public async Task InitializeAsync()
        {
            await _client.InitializeAsync();
        }

        public async Task InsertTranslationAsync(TranslationTable data)
        {
            await _client.From<TranslationTable>().Insert(data);
        }
        public async Task UpdateTranslationAsync(string word,string sentence, string translatedSentence)
        {
            await _client.From<TranslationTable>().Where(x => x.Word == word).Set(x => x.Sentence, sentence).Update();
            await _client.From<TranslationTable>().Where(x => x.Word == word).Set(x => x.TranslatedSentence, translatedSentence).Update();
        }
        public async Task<TranslationTable?> GetTranslationByWordAsync(string word)
        {
            var response = await _client
                .From<TranslationTable>()
                .Where(t => t.Word == word)
                .Get();
            return response.Models.FirstOrDefault();
        }
        public async Task<List<TranslationTable>> GetAllTranslationsAsync()
        {
            var response = await _client.From<TranslationTable>().Get();
            return response.Models;
        }
    }
}