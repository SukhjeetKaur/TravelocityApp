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
    class LocationDataBase
    {
        readonly SQLiteAsyncConnection _database;

        public LocationDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Location>().Wait();
            //_database.DeleteAllAsync<Location>().Wait();
            seedingData();
        }

        public void seedingData()
        {
            var citiesNames = new string[]{"Barrie","Belleville","Brampton","Brantford","Brockville","Elliot Lake","Etobicoke",
                "Fort Erie","Fort Frances","Gananoque","Guelph","Hamilton","Iroquois Falls","Kapuskasing","Niagara Falls",
                "Niagara-on-the-Lake","North Bay","North York","Oakville","Saint Catharines","Saint Thomas","Sarnia-Clearwater","Thorold",
                "Thunder Bay","Timmins","Toronto","Trenton","Waterloo","Welland"};

            if (_database.Table<Location>().CountAsync().Result == 0)
            {
                // only insert the data if it doesn't already exist
                var newLocation = new Location();
                for (int i = 0; i < citiesNames.Length; i++)
                {
                    newLocation.Name = citiesNames[i];
                    newLocation.Price = new Random().Next(30, 200); ;
                    SaveLocationAsync(newLocation);
                    newLocation = new Location();
                }

            }
            Console.WriteLine("Reading data");
            var table = _database.Table<Location>();
            foreach (var s in table.ToListAsync().Result)
            {
                Console.WriteLine(s.LocationID + " " + s.Name);
            }
        }
        public Task<List<Location>> GetLocationsAsync()
        {
            return _database.Table<Location>().ToListAsync();
        }

        public Task<Location> GetLocationAsync(int locationId)
        {
            return _database.Table<Location>()
                            .Where(i => i.LocationID == locationId)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveLocationAsync(Location location)
        {
            if (location.LocationID != 0)
            {
                return _database.UpdateAsync(location);
            }
            else
            {
                return _database.InsertAsync(location);
            }
        }

        public Task<int> DeleteLocationAsync(Location note)
        {
            return _database.DeleteAsync(note);
        }
    }
}