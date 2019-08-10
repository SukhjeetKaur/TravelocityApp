using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using TRAVELOCITY.DB;
using TRAVELOCITY.Helper;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace TRAVELOCITY
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class LoginActivity : AppCompatActivity
    {
        private Button btBack;
        private Button btSignIn;
        private EditText userName;
        private EditText password;
        private UserDataBase userDB;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            userDB = new UserDataBase(SharedHelper.DbPath);
            SetContentView(Resource.Layout.loginScreen);// Create your application here
                                                        // Registering Components
            btBack = FindViewById<Button>(Resource.Id.btnLoginBack);
            btSignIn = FindViewById<Button>(Resource.Id.btnLoginSignIn);
            userName = FindViewById<EditText>(Resource.Id.editLoginUsername);
            password = FindViewById<EditText>(Resource.Id.editLoginPassword);

            btSignIn.Click += OnSignInClick;

            btBack.Click += delegate
            {
                Intent homeScreen = new Intent(this, typeof(MainActivity));
                StartActivity(homeScreen);
            };
        }
        private void OnSignInClick(object sender, EventArgs e)
        {


            if ((userName.Text.Trim().Equals("") || userName.Text.Length < 0) ||
                (password.Text.Trim().Equals("") || password.Text.Length < 0))
            {
                new MessageHelper().alertFunction("Error", "Please Fill All Fields", this);
            }
           if(userDB.Login(userName.Text, password.Text))
            {
                var activity2 = new Intent(this, typeof(LocationActivity));
                //activity2.PutExtra("mail", myEmail);
                //activity2.PutExtra("name", myName);
                //activity2.PutExtra("age", myAge);
                App.username = userName.Text;
                StartActivity(activity2);
            }
        }
        }
}