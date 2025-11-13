using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1.Helpers
{
    class FetchDataSuccesListeners : Java.Lang.Object, IEventListener
    {

        public EventHandler<FetchDataSuccessArgs> OnFetchSuccess;

        public class FetchDataSuccessArgs: EventArgs
        {
            public FetchDataSuccessArgs (string[] userNames)
            {
                this.userNames = userNames;
            }

            public string[] userNames { get; private set; }
        }

        public void OnEvent(Java.Lang.Object result, FirebaseFirestoreException error)
        {
            var snapshot = (QuerySnapshot)result;
            Console.WriteLine("OnEvent");
            if (!snapshot.IsEmpty)
            {
                var documents = snapshot.Documents;
                List<String> userNames = new List<String>();
                foreach(DocumentSnapshot item in documents)
                {
                    if(item.Id == "voS6icHgp7P3CsWBOYxWOWWVHIL2")
                    {
                        if (item.Get("schooltype").ToString() == "t")
                        {
                            userNames.Add(item.Get("username").ToString());
                        }
                    }
                }

                OnFetchSuccess?.Invoke(this, new FetchDataSuccessArgs(userNames.ToArray()));
            }
        }

        public void Create(string path)
        {
            AppDataHelper.GetFirestore().Collection(path).AddSnapshotListener(this);
        }
    }
}