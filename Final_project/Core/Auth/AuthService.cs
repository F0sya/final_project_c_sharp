using Final_project_wpf.Core.Interfaces;
using Supabase.Gotrue;
using System;
using System.Threading.Tasks;

namespace Final_project_wpf.Core.Auth
{
    public class AuthService
    {
        private readonly ISupabaseAuthRepository _authRepository;
        private Session? _currentSession;

        public AuthService(ISupabaseAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public User? CurrentUser { get; private set; }
        public event Action? AuthStateChanged;

        public async Task<bool> Initialize()
        {
            try
            {
                CurrentUser = await _authRepository.GetCurrentUser();
                return CurrentUser != null;
            }
            catch
            {
                return false;
            }
        }

        public async Task<User?> Login(string email, string password)
        {
            CurrentUser = await _authRepository.Login(email, password);
            AuthStateChanged?.Invoke();
            return CurrentUser;
        }

        public async Task<User?> Register(string email, string password)
        {
            CurrentUser = await _authRepository.Register(email, password);
            AuthStateChanged?.Invoke();
            return CurrentUser;
        }

        public async Task Logout()
        {
            await _authRepository.Logout();
            CurrentUser = null;
            AuthStateChanged?.Invoke();
        }

        public bool IsLoggedIn => CurrentUser != null;
    }
}
