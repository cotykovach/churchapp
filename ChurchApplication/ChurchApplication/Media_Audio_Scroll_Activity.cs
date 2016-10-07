using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace ChurchApplication
{
    [Activity(Label = "Media: Audio", Icon = "@drawable/icon")]
    public class Media_Audio_Scroll_Activity : Activity
    {

        private BaseAdapter<Audio> mAdapter;
        private WebClient mClient;
        private Uri mUrl;
        private List<Audio> mAudio;
        private ListView mListView;
        private ProgressBar mProgressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_default_button_scroll);

            mAudio = null;
            mAdapter = null;
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            mListView = FindViewById<ListView>(Resource.Id.listView);
            mClient = new WebClient();
            mUrl = new Uri("http://www.cotykovach.com/GetAudio.php");

            mClient.DownloadDataAsync(mUrl);
            mClient.DownloadDataCompleted += mClient_DownloadDataCompleted;



        }

        void mClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                
                string json = Encoding.UTF8.GetString(e.Result);
                mAudio = JsonConvert.DeserializeObject<List<Audio>>(json);

                mAdapter = new AudioListAdapter(this, Resource.Layout.pager_simple_button, mAudio);
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