using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ChurchApplication
{
    class Videos
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }
}