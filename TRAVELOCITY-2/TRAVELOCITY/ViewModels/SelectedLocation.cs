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

namespace TRAVELOCITY.ViewModels
{
    class SelectedLocation:Location
    {
        public int Postion { get; set; }
    }
}