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

namespace App1
{
    public class Student
    {

        public string Name { get; set; }
        public string School { get; set; }
        public int MathLevel { get; set; }
        public int EnglishLevel { get; set; }

        public Student() {
            this.Name = string.Empty;
            this.School = string.Empty;
            this.EnglishLevel = 0;
            this.MathLevel = 0;
        }

        public Student (string id, string name, string school, int mathLevel, int englishLevel)
        {
            this.Name = name;
            this.School = school;
            this.MathLevel = mathLevel;
            this.EnglishLevel = englishLevel;
        }
    }
}