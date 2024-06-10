using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App2.Droid;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthentificator))]
namespace App2.Droid
{
    public class FirebaseAuthentificator : IFirebaseAuthentificator
    {
        public async Task<string> CreateUserWithEmailAndPasswordAsync(string email, string password)
        {
            var authResult = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            var getTokenResult = await authResult.User.GetIdTokenAsync(false);
            return getTokenResult.Token;
        }

        public async Task<string> SignWithEmailAndPasswordAsync(string email, string password)
        {
            var authResult = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
            var getTokenResult = await authResult.User.GetIdTokenAsync(false);
            return getTokenResult.Token;
        }
        public async Task SendPasswordResetEmailAsync(string email)
        {
            await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
        }
        public async Task SendEmailVerificationAsync()
        {
            await FirebaseAuth.Instance.CurrentUser.SendEmailVerificationAsync(ActionCodeSettings.NewBuilder().Build());
        }
        public async Task<bool> IsCurrentUserEmailVerified()
        {
            await FirebaseAuth.Instance.CurrentUser.ReloadAsync();
            return FirebaseAuth.Instance.CurrentUser.IsEmailVerified;
        }
    }
}