using Android.App;
using Android.Content;
using Android.Views;
using Android.OS;

namespace ChurchApplication
{
    [Activity(Label = "More: Links", Icon = "@drawable/icon")]
    public class More_Links_Scroll_Activity : Activity
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_more_links_scroll);

        }

        [Java.Interop.Export("button_WebsiteLink")]
        public void button_WebsiteLink(View v)
        {
            var uri = Android.Net.Uri.Parse("http://www.firstpresmuncie.org/");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        [Java.Interop.Export("button_FacebookLink")]
        public void button_FacebookLink(View v)
        {
            var uri = Android.Net.Uri.Parse("https://www.facebook.com/FPCMuncie/");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        [Java.Interop.Export("button_TwitterLink")]
        public void button_TwitterLink(View v)
        {
            var uri = Android.Net.Uri.Parse("https://twitter.com/firstpresmuncie");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}