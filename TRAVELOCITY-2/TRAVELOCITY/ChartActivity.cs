using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using TRAVELOCITY.DB;
using TRAVELOCITY.Helper;
using TRAVELOCITY.Models;
using TRAVELOCITY.ViewModels;

namespace TRAVELOCITY
{
    [Activity(Label = "ChartActivity")]
    public class ChartActivity : Activity
    {
        TravelingLocationDataBase traveLocation;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            traveLocation = new TravelingLocationDataBase(SharedHelper.DbPath);
            if (App.username == null)
            {
                Intent ints = new Intent(this, typeof(LoginActivity));
                StartActivity(ints);
            }
            SetContentView(Resource.Layout.cart);

            Button backCheckout = FindViewById<Button>(Resource.Id.btnCheckoutBack);
            backCheckout.Click += ONBackCheckout_Click;

            Button addCheckout = FindViewById<Button>(Resource.Id.btnCheckoutAdd);
            addCheckout.Click += OnAddCheckout_Click;
            // Give name in text box
            FindViewById<TextView>(Resource.Id.txtSelectedCity).Text = App.selectedLocation.Name;
            // Create your application here
        }


        private void OnAddCheckout_Click(object sender, EventArgs e)
        {
            if (App.selectedLocation == null)
            {
                SetContentView(Resource.Layout.locationPage);
            }

            TravelLocation travelLoc = new TravelLocation()
            {
                LocationId = App.selectedLocation.LocationID,
                Username = App.username
            };

            traveLocation.SaveTravelingLocationAsync(travelLoc);
            Intent location = new Intent(this, typeof(LocationActivity));
            StartActivity(location);

        }

        private void ONBackCheckout_Click(object sender, EventArgs e)
        {
            Intent location = new Intent(this, typeof(LocationActivity));
            StartActivity(location);
        }
    }
}