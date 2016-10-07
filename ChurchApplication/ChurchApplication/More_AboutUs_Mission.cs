using Android.App;
using Android.Views;
using Android.OS;
using Android.Webkit;

namespace ChurchApplication
{
    [Activity(Label = "About Us: Mission & Focus Area", Icon = "@drawable/icon")]
    public class More_AboutUs_Mission : Activity
    {
        private WebView mMission;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_mobile_webview);


            mMission = FindViewById<WebView>(Resource.Id.mobileWebView);
            WebSettings settings = mMission.Settings;
            settings.DefaultTextEncodingName = ("ISO-8859-1");
            mMission.LoadUrl("http://www.cotykovach.com/AboutUs-Mission.html");


        }



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}