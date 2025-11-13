using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1
{
    internal class StudentAdapter : BaseAdapter
    {

        readonly Context context;
        readonly List<Student> students;
        readonly bool filter;

        public StudentAdapter(Context context, List<Student> students, bool filter)
        {
            this.context = context;
            this.students = students;
            this.filter = filter;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layoutInflater;
            if (filter)
                layoutInflater = ((FilterListActivity)context).LayoutInflater;
            else
                layoutInflater = ((AllStudentsListActivity)context).LayoutInflater;

            View view = layoutInflater.Inflate(Resource.Layout.StudentsListRow, parent, false);
            TextView tvName = view.FindViewById<TextView>(Resource.Id.tvName);
            TextView mthLevel = view.FindViewById<TextView>(Resource.Id.mthLevel);
            TextView englishLevel = view.FindViewById<TextView>(Resource.Id.englLevel);
            TextView tvSchool = view.FindViewById<TextView>(Resource.Id.tvSchool);
            Student student = students[position];

            if (student != null)
            {
                tvName.Text = student.Name;
                mthLevel.Text = student.MathLevel.ToString();
                englishLevel.Text = student.EnglishLevel.ToString();
                tvSchool.Text = student.School;
            }
            return view;


        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return 0;
            }
        }

    }

    internal class StudentAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}