using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TRAVELOCITY.Models;

namespace TRAVELOCITY.Adapter
{
    class LocationAdapter : BaseAdapter<Location>
    {

        Context context;
        private List<Location> locations;

        public LocationAdapter(Context context, List<Location> locations)
        {
            this.context = context;
            this.locations = locations;
        }

        public override int Count
        {
            get
            {
                return locations.Count;
            }
        }

        public override Location this[int position]
        {

            get { return locations[position]; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return locations[position].LocationID; 
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            Location myObj = locations[position];
            //LocationAdapterViewHolder holder = null;

            if (view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.CellLayout, null, false);
            }

            //if (holder == null)
            //{
            //    holder = new LocationAdapterViewHolder();
            //    var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
            //    //replace with your item and your holder items
            //    //comment back in
            //    //view = inflater.Inflate(Resource.Layout.item, parent, false);
            //    //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
            //    view.Tag = holder;
            //}

          TextView tx=  view.FindViewById<TextView>(Resource.Id.nameID);
            tx.Text = myObj.Name;
                tx.Tag = myObj.LocationID;
            TextView tPrice= view.FindViewById<TextView>(Resource.Id.priceId);
            tPrice.Text = myObj.Price.ToString();
            tPrice.Tag = myObj.LocationID;
            //fill in your items
            //holder.Title.Text = "new text here";

            return view;
        }

     

    }

    class LocationAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}