using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using App1.Admin;
using App1.EventListeners;
using App1.Helpers;
using App1.LoginAndRegistration;
using Firebase.Auth;
using System;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Login : AppCompatActivity, View.IOnClickListener
    {
        TextView signIn, forgotPass, adminBut; 
        Button login;
        EditText email,password;
        ISharedPreferences sp;
        Intent intent;
        FirebaseAuth mAuth;
        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.LoginIn);
            InitViews();
        }
        public void OnSuccess(Java.Lang.Object result)
        {
            Toast.MakeText(this, "Succes", ToastLength.Short).Show();
           // StartActivity(typeof(MainActivity));
        }
        public void OnFailure(Java.Lang.Exception e)
        {
            Toast.MakeText(this, "Failed: " + e.Message, ToastLength.Short).Show();
        }
        
        public void InitViews (){
            login = FindViewById<Button>(Resource.Id.Login);
            login.Click += LoginClick;
            email = FindViewById<EditText>(Resource.Id.email_editText);
            password = FindViewById<EditText> (Resource.Id.password_editText);
            email.SetOnClickListener(this);
            password.SetOnClickListener(this);
            signIn = FindViewById<TextView>(Resource.Id.SignIn);
            signIn.SetOnClickListener(this);
            forgotPass = FindViewById<TextView>(Resource.Id.forgotPassword);
            forgotPass.SetOnClickListener(this);
            sp = this.GetSharedPreferences("details", FileCreationMode.Private);
            intent = new Intent(this,typeof(Registration));
            mAuth = AppDataHelper.GetFirebaseAuth();
            adminBut = FindViewById<TextView>(Resource.Id.forAdmin);
            adminBut.SetOnClickListener(this);
        }

        public void LoginClick(object sender, EventArgs e)
        {
            string emailText = email.Text;
            string passwordText = password.Text;

            if (!emailText.Contains("@"))
            {
                Toast.MakeText(this, "Please enter a valid email", ToastLength.Short).Show();
                return;
            }

            else if (passwordText.Length < 5)
            {
                Toast.MakeText(this, "Please enter a valid password", ToastLength.Short).Show();
                return;
            }
            
            mAuth.SignInWithEmailAndPassword(emailText, passwordText).AddOnSuccessListener(taskCompletionListeners)
                .AddOnFailureListener(taskCompletionListeners);

            taskCompletionListeners.Succes += (succes, args) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("userId", mAuth.CurrentUser.Uid);

                //поставить проверку как-то связав с файрбейс чтобы узнать, есть ли права админа или нет
                StartActivity(intent);

                Toast.MakeText(this, "Login Was successful", ToastLength.Short).Show();
            };

            taskCompletionListeners.Failure += (failure, args) =>
            {
                Toast.MakeText(this, "Login failed: " + args.Cause, ToastLength.Short).Show();
            };
        }
        public void OnClick(View v)
        {
            if(v == signIn)
                StartActivityForResult(intent,0);

            if (v == forgotPass)
                StartActivity(typeof(ForgetPassword_FirstStage));
            if (v == adminBut)
                StartActivity(typeof(AllStudentsListActivity));
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if(requestCode == 0)
            {
                if(resultCode == Result.Ok)
                {
                    Intent intent = new Intent();
                    email.Text = intent.GetStringExtra("mail");
                    password.Text = intent.GetStringExtra("pass");
                }
            }
        }

        
    }
    
}