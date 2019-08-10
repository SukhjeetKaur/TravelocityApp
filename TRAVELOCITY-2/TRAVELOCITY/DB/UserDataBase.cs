using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using TRAVELOCITY.Models;

namespace TRAVELOCITY.DB
{
    class UserDataBase
    {
        readonly SQLiteAsyncConnection _database;

        public UserDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            var table = _database.Table<User>();
            foreach (var s in table.ToListAsync().Result)
            {
                Console.WriteLine(s.Username + " " + s.Password);
            }
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> GetUserAsync(string username)
        {
            return _database.Table<User>()
                            .Where(i => i.Username == username)
                            .FirstOrDefaultAsync();
        }

        public bool Login(string username, string password)
        {
            var us = _database.Table<User>()
                           .Where(i => i.Username == username && i.Password == password)
                           .ToListAsync();
            return us.Result.Count > 0;
        }
        public Task<int> SaveUserAsync(User user)
        {

            return _database.InsertAsync(user);

        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }
    }
}