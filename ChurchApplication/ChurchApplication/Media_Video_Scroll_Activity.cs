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
    [Activity(Label = "Media: Videos", Icon = "@drawable/icon")]
    public class Media_Video_Scroll_Activity : Activity
    {


        private BaseAdapter<Videos> mAdapter;
        private WebClient mClient;
        private Uri mUrl;
        private List<Videos> mVideos;
        private ListView mListView;
        private ProgressBar mProgressBar;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            //SetContentView(Resource.Layout.pager_media_detail);


            SetContentView(Resource.Layout.pager_default_button_scroll);

            mVideos = null;
            mAdapter = null;
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            mListView = FindViewById<ListView>(Resource.Id.listView);
            mClient = new WebClient();
            mUrl = new Uri("http://www.cotykovach.com/GetVideos.php");

            //Call php file
            mClient.DownloadDataAsync(mUrl);
            mClient.DownloadDataCompleted += mClient_DownloadDataCompleted;



        }


        void mClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                string json = Encoding.UTF8.GetString(e.Result);
                mVideos = JsonConvert.DeserializeObject<List<Videos>>(json);

                mAdapter = new VideoListAdapter(this, Resource.Layout.pager_thumbnail_button, mVideos);
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