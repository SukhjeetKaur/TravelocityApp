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
    class User
    {
        /// <summary>
        /// 
        /// </summary>
        [PrimaryKey]
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }


      
    }
}