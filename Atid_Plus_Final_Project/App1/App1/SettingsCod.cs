using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Icu.Text.AlphabeticIndex;
using static App1.MainActivity;

namespace App1
{
    [Activity(Label = "SettingsCod")]
    public class SettingsCod : Activity, Android.Views.View.IOnClickListener
    {
        private Button apply;
        private Button cancel;
        private EditText MaximalEditText;
        private EditText MinimalEditText;
        public RemoteControl remote;
        MyDialog dialog;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SettingsPanel);

            apply = FindViewById<Button>(Resource.Id.ApplyButton);
            cancel = FindViewById<Button>(Resource.Id.CancelButton);
            MaximalEditText = FindViewById<EditText>(Resource.Id.MaxEditText);
            MinimalEditText = FindViewById<EditText>(Resource.Id.MinEditText);
            apply.SetOnClickListener(this);
            cancel.SetOnClickListener(this);
            MinimalEditText.SetOnClickListener(this);
            MaximalEditText.SetOnClickListener(this);
            RegisterForContextMenu(MinimalEditText);
            RegisterForContextMenu(MaximalEditText);
            dialog = new MyDialog(this);




        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.first)
            {
                MinimalEditText.Text = "16";
                return true;
            }

            else if (item.ItemId == Resource.Id.second)
            {
                MinimalEditText.Text = "15";
                return true;
            }
            else if (item.ItemId == Resource.Id.third)
            {
                MinimalEditText.Text = "14";
                return true;
            }
            if (item.ItemId == Resource.Id.thirtyOne)
            {
                MaximalEditText.Text = "31";
                return true;
            }

            else if (item.ItemId == Resource.Id.thirtyTwo)
            {
                MaximalEditText.Text = "32";
                return true;
            }
            else if (item.ItemId == Resource.Id.thirtyThree)
            {
                MaximalEditText.Text = "33";
                return true;
            }
            return false;

        }

        public override void OnCreateContextMenu(IContextMenu menu,View v,IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);
            if(v== MinimalEditText)
                MenuInflater.Inflate(Resource.Menu.contextMenuMinimal, menu);
            if (v == MaximalEditText)
                MenuInflater.Inflate(Resource.Menu.contextMenuMaximal, menu);
        }
        public void OnClick(View v)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            int minimalValue;
            int maximalValue;
            if (v== cancel)
            {
                StartActivity(intent);
            }
            if (v == apply && int.TryParse(MinimalEditText.Text, out minimalValue) == true && int.TryParse(MaximalEditText.Text, out maximalValue) == true)
            {
                intent.PutExtra("minValue", minimalValue);
                intent.PutExtra("maxValue", maximalValue);
                StartActivity(intent);
            }else dialog.Show();
        }
    }
}