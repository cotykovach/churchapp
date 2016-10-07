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
    [Activity(Label = "Events: Event Title", Icon = "@drawable/icon")]
    public class Event_Detail_Activity : Activity
    {

        private TextView mEventTitle, mEventInfo, mEventDate, mEventDetails, mEventDirections;
        private Event mEvent;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_event_detail);

            string eventID = Intent.GetStringExtra("EventDataID") ?? "Data not available";


            mEventTitle = FindViewById<TextView>(Resource.Id.eventTitle);
            mEventDate = FindViewById<TextView>(Resource.Id.eventDate);
            mEventInfo = FindViewById<TextView>(Resource.Id.eventMoreInfo);
            mEventDetails = FindViewById<TextView>(Resource.Id.eventDetails);
            mEventDirections = FindViewById<TextView>(Resource.Id.eventDirections);


            PostRequest("http://www.cotykovach.com/GetEventDetail.php", eventID);

        }

        async void PostRequest(string URL, string eventID)
        {
            var formContent = new FormUrlEncodedContent(new[]
    {
                new KeyValuePair<string, string>("EventID", eventID),
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(URL, formContent);

            string json = await response.Content.ReadAsStringAsync();
            mEvent = JsonConvert.DeserializeObject<Event>(json);

            mEventTitle.Text = mEvent.Title;
            mEventDate.Text = mEvent.Date.ToString();
            mEventInfo.Text = mEvent.Contact;
            mEventDetails.Text = mEvent.Description;
            mEventDirections.Text = mEvent.Directions;

            this.Title = mEvent.Title;

            if (mEvent.Image != null)
            {
                Picasso.With(this).Load(mEvent.Image).Into(FindViewById<ImageView>(Resource.Id.eventTitle));
            }

        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}