using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using App1.Helpers;
using App1.MainActivities;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
using Google.Android.Material.BottomNavigation;
using Java.Util;
using System;
using IEventListener = Java.Util.IEventListener;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, Firebase.Firestore.IEventListener
    {
        TextView textMessage;
        FirebaseFirestore database;
        EditText editText;
        Button button;
        ISharedPreferences sp;
        CollectionReference cf;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            InitViews();

        }
        public void InitViews()
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            textMessage = FindViewById<TextView>(Resource.Id.message);
            database = AppDataHelper.GetFirestore();



            //AppDataHelper appDataHelper = new AppDataHelper();
            //appDataHelper.AddCollectionSnapshotListener(this, "users");
            var MyId = Intent.GetStringExtra("userId");
            database.Collection("users").Document(MyId).AddSnapshotListener(this);
        }

 
        public bool AddNewDocument(string str)
        {
            try {
                HashMap hashMap = new HashMap();
                hashMap.Put("value", str);
                DocumentReference document = database.Collection("TestColection").Document();
                document.Set(hashMap);

                Toast.MakeText(this, "true", ToastLength.Short);

            }catch (Exception ex)
            {
                return false;
            }
            return true;
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

                //case Resource.Id.navigation_home:
                    //var intent = new Intent(this, typeof(MainActivity));
                    //StartActivity(intent);
                    //return true;

                case Resource.Id.profle_icon:
                    var intent2 = new Intent(this, typeof(User));
                    StartActivity(intent2);
                    return true;
            }
            return false;
        }

        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {
            Console.WriteLine($"obj -> {obj}");
            DocumentSnapshot snapshot = (DocumentSnapshot)obj;
            var x = snapshot.Get("username");
            Console.WriteLine(x);
        }
    }
}

