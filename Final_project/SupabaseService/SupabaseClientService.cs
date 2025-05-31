using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase;
using Final_project_wpf.SupabaseService.Models;
using System.Windows;

namespace Final_project_wpf.SupabaseService
{
    public class SupabaseClientService
    {
        private readonly Client _client;

        public SupabaseClientService()
        {
            var url = "https://jeszlpmipprqfwmlreao.supabase.co";
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Implc3pscG1pcHBycWZ3bWxyZWFvIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDgxMjkwMTQsImV4cCI6MjA2MzcwNTAxNH0.mTwaMpfbOheO-YkxbIw7pCi3QdN8lD_2-39IRoYjVEQ";
            _client = new Client(url, key);
        }

        public async Task InitializeAsync()
        {
            await _client.InitializeAsync();
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {

            try
            {
                var user = new UserList
                {
                    Username = username,
                    Password = password,
                };

                var response = await _client.From<UserList>().Insert(new[] { user });

                // If the insert was successful, at least one model should be returned
                if (response.Models != null && response.Models.Count > 0)
                {
                    return true;
                }
                else
                {

                    MessageBox.Show("Registration failed: No rows inserted.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Optionally, show a message or log for debugging
                MessageBox.Show($"Exception: {ex.Message}");
                return false;
            }
        }
    }
}
