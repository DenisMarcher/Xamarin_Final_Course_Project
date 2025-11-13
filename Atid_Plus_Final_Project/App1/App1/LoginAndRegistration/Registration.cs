
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using App1.EventListeners;
using App1.Helpers;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using System;
using System.Threading;

namespace App1
{
    [Activity(Label = "Activity1")]
    public class Registration : Activity
    {
        Button register;
        string user, email, password,confirmedPassword,school;
        EditText email_edit, password_edit, userName_edit,confirmPassword,schoolName_edit;
        ISharedPreferences sp;
        FirebaseAuth mAuth;
        TextView clickToLogin;
        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        FirebaseFirestore database;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Registration);
            InitViews();
            SetClickLogics();
        }

        public void InitViews()
        {
            userName_edit = FindViewById<EditText>(Resource.Id.UserName);
            email_edit = FindViewById<EditText>(Resource.Id.email_editText);
            //school = FindViewById<EditText>(Resource.Id.yourSchool);
            database = AppDataHelper.GetFirestore();
            password_edit = FindViewById<EditText>(Resource.Id.password_editText);
            register = FindViewById<Button>(Resource.Id.Register);
            clickToLogin = FindViewById<TextView>(Resource.Id.returtToLogin);
            confirmPassword = FindViewById<EditText>(Resource.Id.confirmPas);
            mAuth = AppDataHelper.GetFirebaseAuth();
            schoolName_edit = FindViewById<EditText>(Resource.Id.schoolType);


        }
        public void SetClickLogics()
        {
            register.Click += RegistrationButton_ClickLogic;
            clickToLogin.Click += (sender, e) =>
            {
                BackToLogin();
            };
        }
        private void BackToLogin()
        {
            Intent intent = new Intent();
            intent.PutExtra("mail",email);
            intent.PutExtra("pass", password);
            SetResult(Result.Ok);
            Finish();
        }
        private void RegistrationButton_ClickLogic (object sender, EventArgs e)
        {
            user = userName_edit.Text;
            email = email_edit.Text;
            password = password_edit.Text;
            confirmedPassword = confirmPassword.Text;
            school = schoolName_edit.Text;

            //Validations

            if(user.Length < 4|| string.IsNullOrEmpty(user))
            {
                Toast.MakeText(this,"Please enter a valid name",ToastLength.Short).Show();
                return;
            }
            else if (!email.Contains("@"))
            {
                Toast.MakeText(this, "Please enter a valid email", ToastLength.Short).Show();
                return;
            }
            else if (password.Length<5||string.IsNullOrEmpty(password))
            {
                Toast.MakeText(this, "Please enter a valid password", ToastLength.Short).Show();
                return;
            }
            else if (password != confirmedPassword)
            {
                Toast.MakeText(this, "Password doesn't match", ToastLength.Short).Show();
                return;
            }
            else if(string.IsNullOrEmpty(school)) 
            {
                Toast.MakeText(this, "Incorrect School Name", ToastLength.Short).Show();
                return;
            }

            //Sending all information to database after validation
            mAuth.CreateUserWithEmailAndPassword(email, password).AddOnSuccessListener(this,taskCompletionListeners)
                .AddOnFailureListener(this,taskCompletionListeners);

            taskCompletionListeners.Succes += (succes, args) =>
            {

                HashMap userMap = new HashMap();
                userMap.Put("email", email);
                userMap.Put("username", user);
                userMap.Put("password", password);
                userMap.Put("schooltype", school);
                DocumentReference userReference = database.Collection("users").Document(user);
                Intent.PutExtra("userName", userReference.Id);
                userReference.Set(userMap);
                Toast.MakeText(this, "Registration was Successfull, confirm email", ToastLength.Long).Show();
            };

            taskCompletionListeners.Failure += (failure, args) =>
            {
                Toast.MakeText(this, "Registration Failed" + args.Cause, ToastLength.Short).Show();
            };

            Thread.Sleep(5000);
            BackToLogin();
        }
    }
}