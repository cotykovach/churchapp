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
    [Activity(Label = "Contact: Name", Icon = "@drawable/icon")]
    public class More_Contacts_Detail_Activity : Activity
    {

        private TextView mContactName, mRole, mEmail, mPhone;
        private Contacts mContact;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pager_more_contacts_detail);

            string contactID = Intent.GetStringExtra("ContactDataID") ?? "Data not available";
            
            mContactName = FindViewById<TextView>(Resource.Id.contactName);
            mPhone = FindViewById<TextView>(Resource.Id.contactPhone);
            mEmail = FindViewById<TextView>(Resource.Id.contactEmail);
            mRole = FindViewById<TextView>(Resource.Id.contactRole);

            PostRequest("http://www.cotykovach.com/GetContactDetails.php", contactID);

        }

        async void PostRequest(string URL, string contactID)
        {
            var formContent = new FormUrlEncodedContent(new[]
    {
                new KeyValuePair<string, string>("ContactID", contactID),
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(URL, formContent);

            string json = await response.Content.ReadAsStringAsync();
            mContact = JsonConvert.DeserializeObject<Contacts>(json);

            mContactName.Text = mContact.Name;
            mPhone.Text = mContact.Phone;
            mEmail.Text = mContact.Email;
            mRole.Text = mContact.Role;
            this.Title = "Contact: "+mContact.Name;

            if (mContact.Image != null)
            {
                Picasso.With(this).Load(mContact.Image).Into(FindViewById<ImageView>(Resource.Id.contactPhoto));
            }

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }

}