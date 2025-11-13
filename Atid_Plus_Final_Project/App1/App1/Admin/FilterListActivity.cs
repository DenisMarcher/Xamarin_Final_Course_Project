using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.Helpers;
using Firebase.Firestore;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1
{
    [Activity(Label = "FilterListActivity")]
    public class FilterListActivity : Activity, View.IOnClickListener, IEventListener, IOnCompleteListener
    {
        RadioButton rbSchool, rbMath, rbEnglish;
        EditText etSchool, etMath, etEnglish;
        ListView lvStudents;
        List<Student> lstStudents;
        StudentAdapter studentsAdapter;
        Button btnOk;
        AppDataHelper database;
        int filterById;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.filter_layout);
            InitView();
            InitObjects();

        }

        private void InitObjects()
        {
            filterById = rbSchool.Id;
            database = new AppDataHelper();
            database.AddCollectionSnapshotListener(this, "users");
        }
        public void InitView()
        {
            rbSchool = FindViewById<RadioButton>(Resource.Id.rbSchool);
            rbMath = FindViewById<RadioButton>(Resource.Id.rbMath);
            rbEnglish = FindViewById<RadioButton>(Resource.Id.rbEnglish);
            etSchool = FindViewById<EditText>(Resource.Id.etSchool);
            etMath = FindViewById<EditText>(Resource.Id.etMath);
            etEnglish = FindViewById<EditText>(Resource.Id.etEnglish);
            lvStudents = FindViewById<ListView>(Resource.Id.lvStudents);
            btnOk = FindViewById<Button>(Resource.Id.btnOk);
            btnOk.SetOnClickListener(this);
            rbSchool.SetOnClickListener(this);
            rbMath.SetOnClickListener(this);
            rbEnglish.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v == btnOk)
                FilterStudents();
            else
            {
                RadioButton rb = FindViewById<RadioButton>(filterById);
                rb.Checked = false;
                filterById = v.Id;
            }
        }

        private void FilterStudents()
        {
            if (filterById == rbSchool.Id)
                FilterBySchool();
            else if (filterById == rbMath.Id)
                FilterByMath();
            else if (filterById == rbEnglish.Id)
                FilterByEnglish();
        }

        private void FilterByMath()
        {
            database.GetEqualSubject("math", int.Parse(etMath.Text)).AddOnCompleteListener(this);
        }

        private void FilterByEnglish()
        {
            database.GetEqualSubject("english",int.Parse(etEnglish.Text) ).AddOnCompleteListener(this);
        }

        private void FilterBySchool()
        {
            database.GetEqualCollection("schoolType",etSchool.Text).AddOnCompleteListener(this);
        }

        private void GetStudents(QuerySnapshot snapshot)
        {
            lstStudents = new List<Student>();
            foreach (DocumentSnapshot item in snapshot.Documents)
            {
                Student student = new Student()
                {
                    School = item.Get("schoolType").ToString(),
                    MathLevel = int.Parse(item.Get("math").ToString()),
                    EnglishLevel = int.Parse(item.Get("english").ToString()),

                };
                lstStudents.Add(student);
            }
            studentsAdapter = new StudentAdapter(this, lstStudents, true);
            lvStudents.Adapter = studentsAdapter;
        }
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
                GetStudents((QuerySnapshot)task.Result);
        }

        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {
            FilterStudents();
        }
    }
}