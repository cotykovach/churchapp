using Android.App;
using Android.Views;
using Android.OS;
using Android.Webkit;

namespace ChurchApplication
{
    [Activity(Label = "About Us: History", Icon = "@drawable/icon")]
    public class More_AboutUs_History : Activity
    {
        private WebView mHistory;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_mobile_webview);


            mHistory = FindViewById<WebView>(Resource.Id.mobileWebView);
            WebSettings settings = mHistory.Settings;
            settings.DefaultTextEncodingName = ("ISO-8859-1");
            mHistory.LoadUrl("http://www.cotykovach.com/AboutUs-History.html");


        }



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}