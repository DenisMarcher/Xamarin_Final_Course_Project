using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1
{
    public class MyDialog: AppCompatActivity, View.IOnClickListener
    {
        private readonly Button ExitButton;
        private readonly Context context;
        Dialog dialog;  

        public MyDialog(Context context)
        {
            this.context = context;
            this.dialog = new Dialog(context);
            dialog.SetContentView(Resource.Layout.XMLFile1);
            ExitButton = dialog.FindViewById<Button>(Resource.Id.btn_exit);
            ExitButton.SetOnClickListener(this);

        }

        public void Show()
        {
            dialog.Show();
        }

        public void OnClick(View v)
        {
            Toast.MakeText(context, "Returning to the settings...", ToastLength.Long).Show();
            dialog.Dismiss();
        }



    }
}