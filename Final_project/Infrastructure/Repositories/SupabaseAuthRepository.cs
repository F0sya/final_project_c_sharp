using Supabase;
using Supabase.Gotrue;
using Final_project_wpf.Core.Interfaces;

namespace Final_project_wpf.Infrastructure.Repositories
{
    public class SupabaseAuthRepository : ISupabaseAuthRepository
    {
        private readonly Supabase.Client _supabaseClient;

        public SupabaseAuthRepository(string supabaseUrl, string supabaseKey)
        {
            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(
                supabaseUrl,
                supabaseKey,
                options
            );
        }

        public async Task<User?> GetCurrentUser()
        {
            try
            {
                var session = _supabaseClient.Auth.CurrentSession;
                if (session?.AccessToken != null)
                {
                    return await _supabaseClient.Auth.GetUser(session.AccessToken);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }


        public async Task<User?> Login(string email, string password)
        {
            try
            {
                var session = await _supabaseClient.Auth.SignIn(email, password);
                return session.User;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User?> Register(string email, string password)
        {
            try
            {
                var session = await _supabaseClient.Auth.SignUp(email, password);
                return session.User;
            }
            catch
            {
                return null;
            }
        }

        public async Task Logout()
        {
            await _supabaseClient.Auth.SignOut();
        }
    }
}
