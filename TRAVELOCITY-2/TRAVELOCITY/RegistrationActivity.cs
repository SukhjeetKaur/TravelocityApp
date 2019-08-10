using System;
using System.Collections.Generic;
using System.IO;
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
using TRAVELOCITY.Models;

namespace TRAVELOCITY
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class RegistrationActivity : AppCompatActivity
    {
        private UserDataBase userDB;
        private Button btBack;
        private Button btSignUp;
        private EditText email;
        private EditText username;
        private EditText password;
        private EditText name;
        private EditText age;
        private EditText confirmPassword;
        private EditText phoneNumber;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            userDB = new UserDataBase(SharedHelper.DbPath);
            SetContentView(Resource.Layout.registration);
            // Create your application here

            btBack = FindViewById<Button>(Resource.Id.btnBack);
            btSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            email = FindViewById<EditText>(Resource.Id.editEmail);
            username = FindViewById<EditText>(Resource.Id.editUsername);
            password = FindViewById<EditText>(Resource.Id.editPassword);
            name = FindViewById<EditText>(Resource.Id.editName);
            age = FindViewById<EditText>(Resource.Id.editAge);
            confirmPassword = FindViewById<EditText>(Resource.Id.editConPassword);
            phoneNumber = FindViewById<EditText>(Resource.Id.editPNumber);

            btSignUp.Click += OnSignUpClick;
            btBack.Click += delegate
            {
                Intent homeScreen = new Intent(this, typeof(MainActivity));
                StartActivity(homeScreen);
            };
        }

        private void OnSignUpClick(object sender, EventArgs e)
        {

            if (password.Text != confirmPassword.Text)
            {
                new MessageHelper().alertFunction("Error", "Password should be the same", this);
                return;
            }

            Func<string, bool> checkInput = value => value.Trim().Equals("") || value.Length < 0;

            if (checkInput(username.Text) || checkInput(password.Text) || checkInput(email.Text) || checkInput(phoneNumber.Text) || checkInput(name.Text) || checkInput(age.Text))
            {
                new MessageHelper().alertFunction("Error", "Please Complete all the fields", this);
                return;

            }

            if (userDB.GetUserAsync(username.Text).Result != null)
            {
                new MessageHelper().alertFunction("Error", "User name already exit", this);
                return;
            }

            if (userDB.GetUserAsync(email.Text).Result != null)
            {
                new MessageHelper().alertFunction("Error", "Email Id already exit", this);
                return;

            }
            var userA = new User
            {
                Username = username.Text,
                Password = password.Text,
                Name = name.Text,
                Age = Convert.ToInt32(age.Text),
                Email = email.Text,
                MobileNumber = phoneNumber.Text
            };

            if (!userDB.SaveUserAsync(userA).IsCompleted)
            {
                new MessageHelper().alertFunction("Registration Successfull", "User created Successfully", this);

                Intent loginScreeen = new Intent(this, typeof(LoginActivity));
                StartActivity(loginScreeen);
            }
            else
            {
                new MessageHelper().alertFunction("Error", "Something wrong happened", this);
            }
        }



    }
}