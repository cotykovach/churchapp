using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChurchApplication
{
    [Activity(Label = "1st Presbyterian Church", Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        private BaseAdapter<Event> mAdapter;
        private WebClient mClient;
        private Uri mUrl;
        private List<Event> mEvent;
        private Sermon mDailySermon;
        private ListView mListView;
        private ProgressBar mProgressBar;
        private Button mDailySermonButton;

        protected override void OnCreate(Bundle bundle)
        {
           

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SlidingTabFragment fragment = new SlidingTabFragment();
            transaction.Replace(Resource.Id.sample_content_fragment, fragment);
            transaction.Commit();

            mDailySermonButton = FindViewById<Button>(Resource.Id.sermon);

            mUrl = new Uri("http://www.cotykovach.com/GetDailySermon.php");

            mClient = new WebClient();
            mClient.DownloadDataAsync(mUrl);
            mClient.DownloadDataCompleted += mClient_DownloadDailySermon;

        }

        private void mClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {

            RunOnUiThread(() =>
            {

                string json = Encoding.UTF8.GetString(e.Result);
                mEvent = JsonConvert.DeserializeObject<List<Event>>(json);
                mAdapter = new EventListAdapter(this, Resource.Layout.pager_button, mEvent);
                mListView.Adapter = mAdapter;
                mProgressBar.Visibility = ViewStates.Gone;
            });
        }

        private void mClient_DownloadDailySermon(object sender, DownloadDataCompletedEventArgs e)
        {

            RunOnUiThread(() =>
            {

                string json = Encoding.UTF8.GetString(e.Result);
                mDailySermon = JsonConvert.DeserializeObject<Sermon>(json);
                mDailySermonButton = FindViewById<Button>(Resource.Id.sermon);
                mDailySermonButton.Tag = mDailySermon.ID.ToString();

            });
        }

        internal void DownloadEventData()
        {
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            mListView = FindViewById<ListView>(Resource.Id.listView);
            mClient = new WebClient();
            mUrl = new Uri("http://www.cotykovach.com/GetEvents.php");
            
            mClient.DownloadDataAsync(mUrl);
            mClient.DownloadDataCompleted += mClient_DownloadDataCompleted;
        }


        [Java.Interop.Export("button_Sermon")]
        public void button_Sermon(View v)
        {
           
            var SermonDetailActivity = new Intent(this, typeof(Worship_Sermon_Detail_Activity));
            SermonDetailActivity.PutExtra("SermonDataID", mDailySermonButton.Tag.ToString());
            this.StartActivity(SermonDetailActivity);
        }


        [Java.Interop.Export("button_MobileBible")]
        public void button_MobileBible(View v)
        {
            StartActivity(typeof(Worship_Mobile_Bible_Activity));
        }


        [Java.Interop.Export("button_SermonArchive")]
        public void button_SermonArchive(View v)
        {
            StartActivity(typeof(Worship_Series_Scroll_Activity));
        }

        [Java.Interop.Export("button_Video")]
        public void button_Video(View v)
        { 
            StartActivity(typeof(Media_Video_Scroll_Activity));
        }

        [Java.Interop.Export("button_Audio")]
        public void button_Audio(View v)
        {
            StartActivity(typeof(Media_Audio_Scroll_Activity));
        }

        [Java.Interop.Export("button_Gallery")]
        public void button_Gallery(View v)
        {
            StartActivity(typeof(Media_Gallery_Scroll_Activity));
        }

        [Java.Interop.Export("button_AboutUs")]
        public void button_AboutUs(View v)
        {
            StartActivity(typeof(More_AboutUs_Scroll_Activity));
        }

        [Java.Interop.Export("button_Contacts")]
        public void button_Contacts(View v)
        {
            StartActivity(typeof(More_Contacts_Scroll_Activity));
        }

        [Java.Interop.Export("button_Links")]
        public void button_Links(View v)
        {
            StartActivity(typeof(More_Links_Scroll_Activity));
        }

        [Java.Interop.Export("button_Donate")]
        public void button_Donate(View v)
        {
            StartActivity(typeof(Donate_Activity));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}

