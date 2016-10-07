using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using Newtonsoft.Json;
using Square.Picasso;
using Android.Webkit;


namespace ChurchApplication
{
    [Activity(Label = "Worship: Sermon Title", Icon = "@drawable/icon")]
    public class Worship_Sermon_Detail_Activity : Activity
    {

        private Sermon mSermon;
        private WebView mSermonContent;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_worship_sermon_detail);

            string sermonID = Intent.GetStringExtra("SermonDataID") ?? "Data not available";

            mSermonContent = FindViewById<WebView>(Resource.Id.sermonWeb);
            WebSettings settings = mSermonContent.Settings;
            settings.DefaultTextEncodingName = ("ISO-8859-1");

            PostRequest("http://www.cotykovach.com/GetSermonDetail.php", sermonID);

        }


        async void PostRequest(string URL, string sermonID)
        {
            var formContent = new FormUrlEncodedContent(new[]
    {
                new KeyValuePair<string, string>("SermonID", sermonID),
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(URL, formContent);

            string json = await response.Content.ReadAsStringAsync();
            mSermon = JsonConvert.DeserializeObject<Sermon>(json);

            
            mSermonContent.LoadUrl(mSermon.Text);
            this.Title = mSermon.Title;

            if (mSermon.Image != null)
            {

                Picasso.With(this).Load(mSermon.Image).Into(FindViewById<ImageView>(Resource.Id.sermonTitle));
            }


    }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}