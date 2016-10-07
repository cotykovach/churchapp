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
using Android.Media;

namespace ChurchApplication


{
    [Activity(Label = "", Icon = "@drawable/icon")]
    public class Media_Audio_Detail_Activity : Activity
    {

        private TextView mAudioTitle, mAudioAuthor, mAudioDate, mAudioLength, mAudioDesc;
        private string mAudioLink;
        private Audio mAudio;
        private Button mAudioPlay;
        private MediaPlayer player = new MediaPlayer();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_media_audio_detail);

            string audioID = Intent.GetStringExtra("AudioDataID") ?? "Data not available";
            

            mAudioTitle = FindViewById<TextView>(Resource.Id.audioTitle);
            mAudioDate = FindViewById<TextView>(Resource.Id.audioPublishDate);
            mAudioAuthor = FindViewById<TextView>(Resource.Id.audioAuthor);
            mAudioLength = FindViewById<TextView>(Resource.Id.audioLength);
            mAudioDesc = FindViewById<TextView>(Resource.Id.audioDesc);
            mAudioPlay = FindViewById<Button>(Resource.Id.playAudio);

            
            PostRequest("http://www.cotykovach.com/GetAudioDetail.php", audioID);

            

        }


        async void PostRequest(string URL, string audioID)
        {
            var formContent = new FormUrlEncodedContent(new[]
    {
                new KeyValuePair<string, string>("AudioID", audioID),
            });
            var dialog = ProgressDialog.Show(this, "Stream", "Streaming Audio");

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(URL, formContent);

            string json = await response.Content.ReadAsStringAsync();
            mAudio = JsonConvert.DeserializeObject<Audio>(json);


            mAudioDate.Text = mAudio.Date.ToString();
            mAudioAuthor.Text = mAudio.Author;
            mAudioDesc.Text = mAudio.Description;
            mAudioLink = mAudio.Link;
            mAudioPlay.Click += new EventHandler(playAudio);

            this.Title = mAudio.Title;

            player.SetAudioStreamType(Stream.Music);
            player.SetDataSource(mAudioLink);
            player.Prepare();
            setProgressText();


            if (mAudio.Image != null)
            {
                Picasso.With(this).Load(mAudio.Image).Into(FindViewById<ImageView>(Resource.Id.audioTitle));
            }

            dialog.Dismiss();

        }

        protected void setProgressText()
        {

            int HOUR = 60 * 60 * 1000;
            int MINUTE = 60 * 1000;
            int SECOND = 1000;

            int durationInMillis = player.Duration;
            int curVolume = player.CurrentPosition;

            int durationHour = durationInMillis / HOUR;
            int durationMint = (durationInMillis % HOUR) / MINUTE;
            int durationSec = (durationInMillis % MINUTE) / SECOND;

            int currentHour = curVolume / HOUR;
            int currentMint = (curVolume % HOUR) / MINUTE;
            int currentSec = (curVolume % MINUTE) / SECOND;

            if (durationHour > 0)
            {
                Console.WriteLine(" 1 = " + Java.Lang.String.Format("%02d:%02d:%02d/%02d:%02d:%02d",
                        currentHour, currentMint, currentSec, durationHour, durationMint, durationSec));
                mAudioLength.Text = durationHour.ToString() + ":" + durationMint.ToString() + ":" + durationSec.ToString();
            }
            else
            {
                Console.WriteLine(" 1 = " + Java.Lang.String.Format("%02d:%02d/%02d:%02d",
                        currentMint, currentSec, durationMint, durationSec));
                if (durationSec.ToString().Length == 1)
                {
                    mAudioLength.Text = durationMint.ToString() + ":" + durationSec.ToString()+"0";
                }
                else { 
                mAudioLength.Text = durationMint.ToString()+":"+durationSec.ToString();
                }
            }
        }


        private void playAudio(object sender, EventArgs e)
        {
           
           if (player.IsPlaying == false) {
            mAudioPlay.SetBackgroundResource(Resource.Drawable.pausebutton);
            player.Start();
            }
           else
            {
                mAudioPlay.SetBackgroundResource(Resource.Drawable.listenbutton);
                player.Pause();
            }
        }



        public override void OnBackPressed()
        {
            player.Stop();
            player = null;
            base.OnBackPressed();
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}