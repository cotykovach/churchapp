using Android.App;
using Android.Views;
using Android.OS;


namespace ChurchApplication
{
    [Activity(Label = "More: About Us", Icon = "@drawable/icon")]
    public class More_AboutUs_Scroll_Activity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_more_aboutus_scroll);
        }

        [Java.Interop.Export("button_OurChurch")]
        public void button_OurChurch(View v)
        {
            StartActivity(typeof(More_AboutUs_OurChurch));
        }

        [Java.Interop.Export("button_OurMission")]
        public void button_OurMission(View v)
        {
            StartActivity(typeof(More_AboutUs_Mission));
        }

        [Java.Interop.Export("button_OurHistory")]
        public void button_OurHistory(View v)
        {
            StartActivity(typeof(More_AboutUs_History));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}