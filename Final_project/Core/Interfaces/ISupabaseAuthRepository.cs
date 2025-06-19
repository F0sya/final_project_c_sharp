using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase.Gotrue;

namespace Final_project_wpf.Core.Interfaces
{
    public interface ISupabaseAuthRepository
    {
        Task<User?> GetCurrentUser();
        Task<User?> Login(string email, string password);
        Task<User?> Register(string email, string password);
        Task Logout();
    }
}
