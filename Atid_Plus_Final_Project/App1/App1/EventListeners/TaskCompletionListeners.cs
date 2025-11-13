using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1.EventListeners
{
    public class TaskCompletionListeners : Java.Lang.Object, IOnSuccessListener, IOnFailureListener
    {
        public event EventHandler<TaskSuccessEventArgs> Succes;
        public event EventHandler<TaskCompletionFailureEventArgs> Failure;


        //Succes
        public class TaskCompletionFailureEventArgs : EventArgs
        {
            public string Cause { get; set; }
        }
        //Failure
        public class TaskSuccessEventArgs : EventArgs
        {
            public Java.Lang.Object Result { get; set; }
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            Failure?.Invoke(this, new TaskCompletionFailureEventArgs { Cause = e.Message });
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            Succes?.Invoke(this, new TaskSuccessEventArgs { Result = result });
        }
    }

}