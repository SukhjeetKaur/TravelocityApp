using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using TRAVELOCITY.Models;

namespace TRAVELOCITY.DB
{
    class TravelingLocationDataBase
    {
        readonly SQLiteAsyncConnection _database;

        public TravelingLocationDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TravelLocation>().Wait();
            //_database.DeleteAllAsync<TravelLocation>().Wait();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<TravelLocation>> GetTravelingLocationsAsync()
        {
            return _database.Table<TravelLocation>().ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public Task<List<TravelLocation>> GetTravelLocationByLocationIdAsync(int locationId)
        {
            return _database.Table<TravelLocation>()
                            .Where(i => i.LocationId == locationId)
                            .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<List<TravelLocation>> GetTravelLocationByUserIdAsync(string username)
        {
            return _database.Table<TravelLocation>()
                            .Where(i => i.Username == username)
                            .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="travlLocation"></param>
        /// <returns></returns>
        public Task<int> SaveTravelingLocationAsync(TravelLocation travlLocation)
        {
            if (travlLocation.TravelLocationId != 0)
            {
                return _database.UpdateAsync(travlLocation);
            }
            else
            {
                return _database.InsertAsync(travlLocation);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public Task<int> DeleteLocationAsync(TravelLocation TravelLoc)
        {
            return _database.DeleteAsync(TravelLoc);
        }
    }
}