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
    using Firebase;
    using Firebase.Firestore;
    using Firebase.Auth;
    using Firebase.Database;
    using Java.Nio.FileNio;
    using Java.Util;
    using Android.Gms.Tasks;

    namespace App1.Helpers
    {
        public class AppDataHelper
        {
            private readonly FirebaseFirestore firestore;


            public static FirebaseAuth GetFirebaseAuth()
            {
                var app = FirebaseApp.InitializeApp(Application.Context);
                FirebaseAuth mAuth;

                if(app == null)
                {
                    var options = new FirebaseOptions.Builder()
                        .SetProjectId("atidplus-66ac5")
                        .SetApplicationId("atidplus-66ac5")
                        .SetApiKey("AIzaSyBrWkxWCRQ-NtC62xmIKP2R_TWMY9a7ITc")
                        .SetDatabaseUrl("atidplus-66ac5.firebaseapp.com")
                        .SetStorageBucket("atidplus-66ac5.appspot.com")
                        .Build();

                    app = FirebaseApp.InitializeApp(Application.Context,options);
                    mAuth = FirebaseAuth.Instance;
                }
                else
                {
                    mAuth = FirebaseAuth.Instance;
                }
                return mAuth;
            }

            public static FirebaseFirestore GetFirestore()
            {
                var app = FirebaseApp.InitializeApp(Application.Context);
                FirebaseFirestore database;

                if (app == null)
                {
                    var options = new FirebaseOptions.Builder()
                        .SetProjectId("atidplus-66ac5")
                        .SetApplicationId("atidplus-66ac5")
                        .SetApiKey("AIzaSyBrWkxWCRQ-NtC62xmIKP2R_TWMY9a7ITc")
                        .SetDatabaseUrl("atidplus-66ac5.firebaseapp.com")
                        .SetStorageBucket("atidplus-66ac5.appspot.com")
                        .Build();

                    app = FirebaseApp.InitializeApp(Application.Context, options);
                    database = FirebaseFirestore.Instance;
                }
                else
                {
                    database = FirebaseFirestore.Instance;
                }
                return database;
            }

            public void AddCollectionSnapshotListener(Activity activity, string cName)
            {
                firestore.Collection(cName).AddSnapshotListener((Firebase.Firestore.IEventListener)activity);
            }
            public Task SetFsDocument(string cName, string id, HashMap hmFields, out string newId)
            {
                DocumentReference dr;
                if (id == string.Empty)
                    dr = firestore.Collection(cName).Document();
                else
                    dr = firestore.Collection(cName).Document(id);
                newId = dr.Id;

                return dr.Set(hmFields);
            }

            public Task GetEqualCollection(string fName, string fValue)
            {
                return firestore.Collection("users").WhereEqualTo(fName, fValue).Get();
            }
            public Task GetEqualSubject(string fName, int fValue)
            {
                return firestore.Collection("users").WhereEqualTo(fName, fValue).Get();
            }

            //короче нужно листенеры поставить, когда я буду менять значение в файрбейсе "haveLesson = true" листенер
            //увидет это и уже на главной странице я увижу урок, потом буду делать уведомления об уроке
            // на будующее чекать приложения янива, у него это реализованно
            //почекать инфу про листенер и как он работает


            //Войти в учиетльский аккаунт, отправить у какой группы урок,у учинеков есть поле в базе данных "haveMathLesson" и яхидот сколько учат
            // валидирую его подходит ли он или нет, меняю на true, у ученика появляется уведомление и на главной странице надпись



        }
    }