using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace ChurchApplication
{
    [Activity(Label = "Worship: Sermon Archive", Icon = "@drawable/icon")]
    public class Worship_Series_Scroll_Activity : Activity
    {
        

        private BaseAdapter<Series> mAdapter;
        private WebClient mClient;
        private Uri mUrl;
        private List<Series> mSeries;
        private ListView mListView;
        private ProgressBar mProgressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_default_button_scroll);

            mSeries = null;
            mAdapter = null;
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            mListView = FindViewById<ListView>(Resource.Id.listView);
            mClient = new WebClient();
            mUrl = new Uri("http://www.cotykovach.com/GetSeries.php");

            mClient.DownloadDataAsync(mUrl);
            mClient.DownloadDataCompleted += mClient_DownloadDataCompleted;

            
        }

        void mClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                string json = Encoding.UTF8.GetString(e.Result);
                mSeries = JsonConvert.DeserializeObject<List<Series>>(json);

                mAdapter = new SeriesListAdapter(this, Resource.Layout.pager_button, mSeries);
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