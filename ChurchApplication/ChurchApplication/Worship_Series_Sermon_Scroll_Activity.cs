using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace ChurchApplication
{
    [Activity(Label = "Series", Icon = "@drawable/icon")]
    public class Worship_Series_Sermon_Scroll_Activity : Activity
    {
        private BaseAdapter<Sermon> mAdapter;
        private List<Sermon> mSermons;
        private ListView mListView;
        private ProgressBar mProgressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pager_default_button_scroll);

            string seriesID = Intent.GetStringExtra("SeriesDataID") ?? "Data not available";
            string seriesTitle = Intent.GetStringExtra("SeriesTitle") ?? "Data not available";
            this.Title = ("Series: "+ seriesTitle);


            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            mListView = FindViewById<ListView>(Resource.Id.listView);
            
            PostRequest("http://www.cotykovach.com/GetSermons.php", seriesID);
        }


        async void PostRequest(string URL, string SeriesID)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("SeriesID", SeriesID),
            });


            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(URL, formContent);

            string json = await response.Content.ReadAsStringAsync();
            mSermons = JsonConvert.DeserializeObject<List<Sermon>>(json);

            mAdapter = new SermonListAdapter(this, Resource.Layout.pager_button, mSermons);
            mListView.Adapter = mAdapter;
            mProgressBar.Visibility = ViewStates.Gone;

        }


            void mClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            { 
                string json = Encoding.UTF8.GetString(e.Result);
                mSermons = JsonConvert.DeserializeObject<List<Sermon>>(json);

                mAdapter = new SermonListAdapter(this, Resource.Layout.pager_button, mSermons);
                mListView.Adapter = mAdapter;
                mProgressBar.Visibility = ViewStates.Gone;
            });
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}