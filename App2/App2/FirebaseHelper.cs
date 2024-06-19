using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public class FirebaseHelper
    {
        FirebaseClient firebaseClient;

        public FirebaseHelper(string firebaseUrl)
        {
            firebaseClient = new FirebaseClient(firebaseUrl);
        }

        public async Task AddEvent(Event newEvent, string userId)
        {
            var result = await firebaseClient.Child("events").Child(userId).PostAsync(newEvent);
        }

        public async Task<List<Event>> GetEvents(string userId)
        {
            return (await firebaseClient.Child("Events").Child(userId).OnceAsync<Event>()).Select(item => new Event
            {
                Name = item.Object.Name,
                Type = item.Object.Type,
                Description = item.Object.Description,
                Address = item.Object.Address,
                Day = item.Object.Day,
                Month = item.Object.Month,
                Year = item.Object.Year,
                Duration = item.Object.Duration,
                Price = item.Object.Price,
                MinimalAge = item.Object.MinimalAge,
            }).ToList();
        }
        public async Task SaveUserUid(string userId)
        {
            await firebaseClient.Child("users").Child(userId).Child("uid").PutAsync(userId);
        }

        public async Task<string> GetUserUid(string userId)
        {
            var user = await firebaseClient.Child("users").Child(userId).Child("uid").OnceSingleAsync<string>();
            return user;
        }
    }
}
