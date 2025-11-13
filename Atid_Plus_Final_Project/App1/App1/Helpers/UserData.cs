using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1.Helpers
{
    class UserData
    {
        private string eMail { set; get; }
        private string password { set; get; }
        private string school { set; get; }

        private string username { set; get; }

        private string grade { set; get; }

        private string mathEducationLevel { set; get; }

        public UserData(string eMail, string password, string school, string username)
        {
            this.eMail = eMail;
            this.password = password;
            this.school = school;
            this.username = username;
        }
    }
}