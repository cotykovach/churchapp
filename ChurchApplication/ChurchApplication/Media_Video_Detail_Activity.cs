using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using Newtonsoft.Json;
using Square.Picasso;

namespace ChurchApplication
{
    [Activity(Label = "", Icon = "@drawable/icon")]
    public class Media_Video_Detail_Activity : Activity
    {


        private TextView mVideoTitle, mVideoAuthor, mVideoDate, mVideoDesc;
        private string mVideoLink;
        private Videos mVideo;
        private Button mVideoPlay, mVideoShare;
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.pager_media_video_detail);
            string videoID = Intent.GetStringExtra("VideoDataID") ?? "Data not available";


            mVideoDate = FindViewById<TextView>(Resource.Id.videoDate);
            mVideoAuthor = FindViewById<TextView>(Resource.Id.videoAuthor);
            mVideoDesc = FindViewById<TextView>(Resource.Id.videoDesc);
            mVideoPlay = FindViewById<Button>(Resource.Id.playVideo);
            mVideoShare = FindViewById<Button>(Resource.Id.shareVideo);

            PostRequest("http://www.cotykovach.com/GetVideoDetail.php", videoID);

        }

        async void PostRequest(string URL, string videoID)
        {
            var formContent = new FormUrlEncodedContent(new[]
    {
                new KeyValuePair<string, string>("VideoID", videoID),
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(URL, formContent);

            string json = await response.Content.ReadAsStringAsync();
            mVideo = JsonConvert.DeserializeObject<Videos>(json);

            
            mVideoDate.Text = mVideo.Date.ToString();
            mVideoAuthor.Text = mVideo.Author;
            mVideoDesc.Text = mVideo.Description;
            mVideoLink = mVideo.Link;

            mVideoPlay.Click += new EventHandler(playVideo);
            mVideoShare.Click += new EventHandler(shareVideo);

            this.Title = mVideo.Title;

            if (mVideo.Image != null)
            {
                Picasso.With(this).Load(mVideo.Image).Into(FindViewById<ImageView>(Resource.Id.videoTitle));
            }

        }

        private void shareVideo(object sender, EventArgs e)
        {
            Intent share = new Intent(Android.Content.Intent.ActionSend);
            share.SetType("text/plain");
            share.PutExtra(Intent.ExtraSubject, "Title Of The Post");
            share.PutExtra(Intent.ExtraText, mVideo.Link);

            StartActivity(Intent.CreateChooser(share, "Share link!"));
        }

        private void playVideo(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse(mVideoLink);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}