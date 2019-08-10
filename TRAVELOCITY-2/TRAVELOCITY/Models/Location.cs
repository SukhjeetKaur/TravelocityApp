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
using SQLite;

 namespace TRAVELOCITY.Models
{
    public class Location
    {
        [PrimaryKey, AutoIncrement]
        public int LocationID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}