using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using TRAVELOCITY.DB;
using System.IO;
using System;
using TRAVELOCITY.Helper;

namespace TRAVELOCITY
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btn_sign_in;
        private Button btn_sign_up;
        LocationDataBase locDB;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            Console.WriteLine(SharedHelper.DbPath);

            locDB = new LocationDataBase(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "travel.db3"));
          
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            btn_sign_in = FindViewById<Button>(Resource.Id.button_sign_in);
            btn_sign_up = FindViewById<Button>(Resource.Id.button_sign_up);

            btn_sign_in.Click += delegate
            {
                Intent signInScreen = new Intent(this, typeof(LoginActivity));
                StartActivity(signInScreen);
            };

            btn_sign_up.Click += delegate
            {
                Intent signUpScreen = new Intent(this, typeof(RegistrationActivity));
                StartActivity(signUpScreen);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}