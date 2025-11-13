using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using App1.Helpers;
using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace App1.LoginAndRegistration
{
    [Activity(Label = "Activity1")]
    public class ForgetPassword_FirstStage : AppCompatActivity
    {
        Button reset;
        EditText email_editText;
        FirebaseAuth mAuth;
        FirebaseFirestore database;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ResetingPasswordEmail);
            InitViews();
        }

        public void InitViews()
        {
            reset = FindViewById<Button>(Resource.Id.sendVerification);
            email_editText = FindViewById<EditText>(Resource.Id.email_forReseting);
            mAuth = AppDataHelper.GetFirebaseAuth();
            reset.Click += Reset_Click;
            database = AppDataHelper.GetFirestore();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            string email = email_editText.Text;

            if (string.IsNullOrEmpty(email))
            {
                Toast.MakeText(this, "Email's field is empty", ToastLength.Short).Show();
                return;
            }
            else if (!email.Contains("@"))
            {
                Toast.MakeText(this, "Please enter a valid email", ToastLength.Short).Show();
                return; 
            }

            mAuth.SendPasswordResetEmailAsync(email);
            Toast.MakeText(this, "Email Confirmation was sended", ToastLength.Short).Show();
            Thread.Sleep(4000);
            StartActivity(typeof(Login));
        }
    }
}