using Android.App;
using Android.Views;
using Android.OS;
using Android.Webkit;

namespace ChurchApplication
{
    [Activity(Label = "Give: Donation Page", Icon = "@drawable/icon")]
    public class Donate_Activity : Activity
    {
        private WebView mDonationPage;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_mobile_webview);


            mDonationPage = FindViewById<WebView>(Resource.Id.mobileWebView);
            WebSettings settings = mDonationPage.Settings;
            settings.DefaultTextEncodingName = ("ISO-8859-1");
            mDonationPage.LoadUrl("https://www.eservicepayments.com/cgi-bin/Vanco_ver3.vps?appver3=Dc8dzPGn4-LCajFevTkh9GB0uGl46Vf0raJipQQABnwd3I9cRbbubXo-6dFAYatvtJ-1qUX4qdqek5S3h87XtGsE5sFcP5W_q6OpXQTy0tHClegOnsxzA4Nh7RO2V8w_k0PpduXvnt8gXUeZjQYbnzLGL0wWJJYh97kJLnGTUlE=&ver=3");


        }



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}