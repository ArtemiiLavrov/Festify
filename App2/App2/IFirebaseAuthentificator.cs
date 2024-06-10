
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App2.Droid
{
    public interface IFirebaseAuthentificator
    {
        Task<String> SignWithEmailAndPasswordAsync(string email, string password);
        Task<String> CreateUserWithEmailAndPasswordAsync(string email, string password);
        Task SendPasswordResetEmailAsync(string email);
        Task SendEmailVerificationAsync();
        Task<bool> IsCurrentUserEmailVerified();
    }
}