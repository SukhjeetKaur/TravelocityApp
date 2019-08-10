using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using TRAVELOCITY.Adapter;
using TRAVELOCITY.DB;
using TRAVELOCITY.Helper;
using TRAVELOCITY.Models;
using TRAVELOCITY.ViewModels;

namespace TRAVELOCITY
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class LocationActivity : AppCompatActivity
    {
        private LocationDataBase locationDb;
        public ListView listView;
        private SearchView searchView1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // check if user is login
            if (App.username == null)
            {
                Intent ints = new Intent(this, typeof(LoginActivity));
                StartActivity(ints);
            }

            SetContentView(Resource.Layout.locationPage);

            // on logout button click
            FindViewById<Button>(Resource.Id.btnLocationLogout)
            .Click += (s, arg) =>
            {
                App.username = null;
                Intent ints = new Intent(this, typeof(LoginActivity));
                StartActivity(ints);
            };

            // on checkout button click
            FindViewById<Button>(Resource.Id.btnLoactionCheckout)
            .Click += (s, arg) =>
            {
                Intent ints = new Intent(this, typeof(CheckoutActivity));
                StartActivity(ints);
            };

            // Create your application here
            locationDb = new LocationDataBase(SharedHelper.DbPath);

            //var checkout = FindViewById<>(Resource.Id.checkout);
            listView = FindViewById<ListView>(Resource.Id.listView1);
            searchView1 = FindViewById<SearchView>(Resource.Id.searchView1);

            LocationAdapter lp = new LocationAdapter(this, locationDb.GetLocationsAsync().Result);
            listView.Adapter = lp;
            listView.ItemClick += OnListClicked;
            searchView1.QueryTextChange += OnSerachChange;

        }

        private void OnSerachChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            var dataset = locationDb.GetLocationsAsync().Result;

            if (string.IsNullOrWhiteSpace(e.NewText))
            {
                LocationAdapter myAdapter = new LocationAdapter(this, dataset);
                listView.Adapter = myAdapter;
            }
            else
            {
                LocationAdapter myAdapter = new LocationAdapter(this, dataset.Where(us => us.Name.ToLower().StartsWith(e.NewText.ToLower())).ToList());
                listView.Adapter = myAdapter;
            }
        }

        private void OnListClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            //var activity2 = new Intent(this, typeof(Activity2));
            //activity2.PutExtra("MyData", "Data from Activity1");
            //StartActivity(activity2);

            if (e != null)
            {
                App.selectedLocation.Postion = e.Position;

                App.selectedLocation.LocationID = Convert.ToInt32(e.Id);
                var loc = locationDb.GetLocationAsync(App.selectedLocation.LocationID).Result;
                App.selectedLocation.Name = loc.Name;
                App.selectedLocation.Price = loc.Price;
            }

            Intent ints = new Intent(this, typeof(ChartActivity));
            StartActivity(ints);
        }

    }
}