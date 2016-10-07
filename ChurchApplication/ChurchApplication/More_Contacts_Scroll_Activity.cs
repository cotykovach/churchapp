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
    [Activity(Label = "More: Contacts", Icon = "@drawable/icon")]
    public class More_Contacts_Scroll_Activity : Activity
    {

        private BaseAdapter<Contacts> mAdapter;
        private WebClient mClient;
        private Uri mUrl;
        private List<Contacts> mContacts;
        private ListView mListView;
        private ProgressBar mProgressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_default_button_scroll);

            mContacts = null;
            mAdapter = null;
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            mListView = FindViewById<ListView>(Resource.Id.listView);
            mClient = new WebClient();
            mUrl = new Uri("http://www.cotykovach.com/GetContacts.php");

            mClient.DownloadDataAsync(mUrl);
            mClient.DownloadDataCompleted += mClient_DownloadDataCompleted;


        }


        void mClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                string json = Encoding.UTF8.GetString(e.Result);
                mContacts = JsonConvert.DeserializeObject<List<Contacts>>(json);

                mAdapter = new ContactsListAdapter(this, Resource.Layout.pager_thumbnail_button, mContacts);
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