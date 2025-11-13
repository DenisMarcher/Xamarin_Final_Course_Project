using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using App1.Helpers;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using static Android.Widget.AdapterView;

namespace App1.Admin
{
    [Activity(Label = "AllStudentsActivity")]
    public class AllStudentsListActivity : AppCompatActivity, View.IOnClickListener, IEventListener, ListView.IOnItemLongClickListener, IOnItemClickListener
    {
        AppDataHelper datahelper;
        Button btnAddLesson, btnFilter;
        ListView lvStudents;
        List<Student> lstStudents;
        StudentAdapter studentsAdapter;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Students_List);
            
        }

        public void InitObject()
        {
            datahelper = new AppDataHelper();
            datahelper.AddCollectionSnapshotListener(this, "users");
        }

        public void InitViews()
        {
            btnAddLesson = FindViewById<Button>(Resource.Id.btnAddLesson);
            btnFilter = FindViewById<Button>(Resource.Id.btnFilter);
            lvStudents = FindViewById<ListView>(Resource.Id.lvStudents);
            btnAddLesson.SetOnClickListener(this);
            btnFilter.SetOnClickListener(this);
            lvStudents.OnItemClickListener = this;
            lvStudents.OnItemLongClickListener = this;
        }


        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {

            GetStudents((QuerySnapshot)obj);
        }

        private void GetStudents(QuerySnapshot snapshot)
        {
            lstStudents = new List<Student>();

            foreach(DocumentSnapshot item in snapshot.Documents)
            {
                Student student = new Student
                {
                    School = item.Get("schoolType").ToString(),
                    MathLevel = int.Parse(item.Get("math").ToString()),
                    EnglishLevel = int.Parse(item.Get("english").ToString()),
                };
                lstStudents.Add(student);
            }

            studentsAdapter = new StudentAdapter(this, lstStudents, false);
            lvStudents.Adapter=studentsAdapter;

        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            throw new NotImplementedException();
        }

        public bool OnItemLongClick(AdapterView parent, View view, int position, long id)
        {
            throw new NotImplementedException();
        }

        public void OnClick(View v)
        {
        
        
        }
    }
}