using Android.App;
using Android.Views;
using Android.OS;
using Android.Webkit;

namespace ChurchApplication
{
    [Activity(Label = "About Us: Our Church", Icon = "@drawable/icon")]
    public class More_AboutUs_OurChurch : Activity
    {
        private WebView mMobileBible;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_mobile_webview);


            mMobileBible = FindViewById<WebView>(Resource.Id.mobileWebView);
            WebSettings settings = mMobileBible.Settings;
            settings.DefaultTextEncodingName = ("ISO-8859-1");
            mMobileBible.LoadUrl("http://www.cotykovach.com/AboutUs-OurChurch.html");


        }



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}