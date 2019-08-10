using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using TRAVELOCITY.Adapter;
using TRAVELOCITY.DB;
using TRAVELOCITY.Helper;
using TRAVELOCITY.Models;
using TRAVELOCITY.ViewModels;

namespace TRAVELOCITY
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class CheckoutActivity : AppCompatActivity
    {
        public ListView listViewCheckout;
        private TextView txtTotalPrice;
        private TravelingLocationDataBase trDb;
        private LocationDataBase locDb;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.checkout);
            // initialize databases
            trDb = new TravelingLocationDataBase(SharedHelper.DbPath);
            locDb = new LocationDataBase(SharedHelper.DbPath);

            //  get resource variables
            listViewCheckout = FindViewById<ListView>(Resource.Id.listViewCheckout);
            txtTotalPrice = FindViewById<TextView>(Resource.Id.txtTotalPrice);

            // add value to list adapter
            var travelLocation = trDb.GetTravelLocationByUserIdAsync(App.username).Result;

            if (travelLocation.Count <= 0)
            {
                new MessageHelper().alertFunction("Error", "Please select the locations", this);
                Intent ints = new Intent(this, typeof(LocationActivity));
                StartActivity(ints);
            }

            List<Location> selectedLoc = new List<Location>();
            for (int trvLoc = 0; trvLoc < travelLocation.Count; trvLoc++)
            {
                selectedLoc.Add(locDb.GetLocationAsync(travelLocation[trvLoc].LocationId).Result);
            }
            LocationAdapter lp = new LocationAdapter(this, selectedLoc);
            listViewCheckout.Adapter = lp;
            listViewCheckout.ItemClick += OnListClicked;

            // calculate total price
            txtTotalPrice.Text = selectedLoc.Select(sa => sa.Price).Sum().ToString();
        }

        private void OnListClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}