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

namespace TRAVELOCITY.Helper
{
    class MessageHelper
    {
        Context myContex;
        AlertDialog.Builder alert;

        public void alertFunction(string title, string message, Context myContex)
        {
            this.myContex = myContex;
            alert = new AlertDialog.Builder(myContex);

            alert.SetTitle(title);
            alert.SetMessage(message);

            if (title == "Error")
            {
                alert.SetPositiveButton("OK", alertOKButton);
            }
            else if (title == "Registration Successfull")
            {
                alert.SetPositiveButton("OK", alertOKButtonRegister);
            }
            else if (title == "Duplicate Value")
            {
                alert.SetPositiveButton("OK", alertOKButtonDuplicate);
            }

            alert.SetNegativeButton("Cancel", alertOKButton);
            Dialog myDialog = alert.Create();
            myDialog.Show();
        }

        private void alertOKButtonRegister(object sender, DialogClickEventArgs e)
        {
            Intent newScreen = new Intent(this.myContex, typeof(LoginActivity));
            myContex.StartActivity(newScreen);
        }

        public void alertOKButton(object sender, DialogClickEventArgs e)
        {

        }
        private void alertOKButtonDuplicate(object sender, DialogClickEventArgs e)
        {

        }
    }
}