using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Firestore.Model;
using Firebase.Firestore;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Lang;

namespace App1.MainActivities
{
    [Activity(Label = "Activity1")]
    public class User : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        EditText math, english;
        FirebaseFirestore database;
        Button editButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.User);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }
        public void InitViews()
        {


        }

        public void ChangeMathEducationLevel()
        {
            int mathPoints = Integer.ParseInt(math.Text);

            if (mathPoints < 6 && mathPoints > 2)
            {

                string username = Intent.GetStringExtra("userName");
                DocumentReference doc = database.Collection("users").Document("voS6icHgp7P3CsWBOYxWOWWVHIL2");

                doc.Update("mathLevel", mathPoints);
            } else Toast.MakeText(this, "Math Education points must be in range from 3 to 5 include", ToastLength.Long).Show();
        }

        public void ChangeEnglishEducationLevel()
        {
            int englishPoints = Integer.ParseInt(english.Text);

            if (englishPoints < 6 && englishPoints > 2)
            {

                string username = Intent.GetStringExtra("userName");
                DocumentReference doc = database.Collection("users").Document("voS6icHgp7P3CsWBOYxWOWWVHIL2");

                doc.Update("englishLevel", englishPoints);
            }
            else Toast.MakeText(this, "English Education points must be in range from 3 to 5 include", ToastLength.Long).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            
            switch (item.ItemId)
            {

                case Resource.Id.navigation_home:
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    return true;

                //case Resource.Id.profle_icon:
                  //  var intent2 = new Intent(this, typeof(User));
                  //  StartActivity(intent2);
                  //  return true;
            }
            return false;
        }
    }
}