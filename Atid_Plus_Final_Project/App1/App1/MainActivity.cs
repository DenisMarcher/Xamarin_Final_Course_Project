using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using System.Numerics;


namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, Android.Views.View.IOnClickListener
    {
        private Button plus;
        private Button minus;
        private Button onButton;
        private Button offButton;
        private TextView temperature;
        private Button settingsButton;
        public RemoteControl rc;

        public Intent intent;
        IMenuItem MenuItem;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            initViews();
            setListeners();
            updateInfo();


            
        }

        public void updateInfo()
        {
            rc.SetMinTemp(Intent.GetIntExtra("minValue",RemoteControl.DEFAULT_MIN));
            rc.SetMaxTemp(Intent.GetIntExtra("maxValue", RemoteControl.DEFAULT_MAX));
            int midTemp = (rc.getMinTemp() + rc.getMaxTemp()) / 2;
            rc.SetDisplay(midTemp);
            
            
        }
        public void setListeners()
        {

            plus.SetOnClickListener(this);
            minus.SetOnClickListener(this);
            onButton.SetOnClickListener(this);
            offButton.SetOnClickListener(this);
            settingsButton.SetOnClickListener(this);
        }
        public void initViews()
        {
 
            plus            = FindViewById<Button>(Resource.Id.plusButton);
            minus           = FindViewById<Button>(Resource.Id.minusButton);
            onButton        = FindViewById<Button>(Resource.Id.onButton);
            offButton       = FindViewById<Button>(Resource.Id.offButton);
            temperature     = FindViewById<TextView>(Resource.Id.number);
            settingsButton  = FindViewById<Button>(Resource.Id.SettingsButton);
            rc = new RemoteControl();

        }
        Android.Widget.SearchView searchView;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu1, menu);
            MenuItem = menu.FindItem(Resource.Id.OnAndOffButton);
            MenuItem.SetVisible(true);
            MenuItem.SetShowAsAction(ShowAsAction.Always);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.settingsItem)
            {
                intent = new Intent(this, typeof(SettingsCod));
                StartActivity(intent);
                Toast.MakeText(this, "you selected settings", ToastLength.Long).Show();
                return true;
            }
            else if (item.ItemId == Resource.Id.OnAndOffButton)
            {
                rc.Power = !rc.Power;
                if(rc.Power==true)
                    item.SetIcon(Resource.Drawable.btn_radio_on_to_off_mtrl_animation); 
                if (rc.Power==false)
                    item.SetIcon(Resource.Drawable.btn_radio_off_to_on_mtrl_animation); 
            }
            return base.OnOptionsItemSelected(item); 
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnClick(View v)
        {
            if (v == onButton)
            {
                rc.Power = true;
                plus.Enabled = true;
                minus.Enabled = true;
            
            }

            else if (v == offButton)
            {
                rc.Power = false;
                temperature.Text = "   ";
                plus.Enabled = false;
                minus.Enabled = false;
            }
            if(v == settingsButton)
            {
                intent = new Intent(this,typeof(SettingsCod));
                StartActivity(intent);
            }
                
                
            else if (v == minus)
                 rc.decreaseTemperature();
            else if (v == plus)
                    rc.increaseTemperature();

            if (rc.Power == false) return;
            temperature.Text = rc.Display;
            
        }

        public class RemoteControl
        {
            public bool Power = false;
            private int display;
            private int MIN_TEMP = 17;
            private int MAX_TEMP = 30;

            public static readonly int DEFAULT_MAX = 30;
            public static readonly int DEFAULT_MIN = 17;





            public int getMinTemp() { return MIN_TEMP; }
            public int getMaxTemp() { return MAX_TEMP; }

            public void SetMaxTemp(int value) { MAX_TEMP = value; }
            public void SetMinTemp(int value) { MIN_TEMP = value; }



            public void increaseTemperature()
            {
                if (Power == true && display < MAX_TEMP)
                    display++;
            }

            public void decreaseTemperature()
            {
                if (Power == true && display > MIN_TEMP)
                    display--;
            }


            public string Display
            {
                get { return "" + display; }
            }
            public void SetDisplay(int display) { this.display = display; }
            

            

        }

    }

}