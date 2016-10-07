
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Net;
using Square.Picasso;

namespace ChurchApplication
{
    class ContactsListAdapter : BaseAdapter<Contacts>
{
    private Context mContext;
    private int mLayout;
    private List<Contacts> mContacts;
    public String videoTitle;

        public ContactsListAdapter(Context context, int layout, List<Contacts> contacts)
    {
        mContext = context;
        mLayout = layout;
        mContacts = contacts;
    }

    public override Contacts this[int position]
    {
        get { return mContacts[position]; }
    }

    public override int Count
    {
        get { return mContacts.Count; }
    }

    public override long GetItemId(int position)
    {
        return position;
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
        View row = convertView;
        
        

            if (row == null)
        {
            row = LayoutInflater.From(mContext).Inflate(mLayout, parent, false);
        }

            row.FindViewById<TextView>(Resource.Id.thumbnailTitle).Text = mContacts[position].Name;
            row.FindViewById(Resource.Id.thumbnailButton).Tag = mContacts[position].ID.ToString();
            
            row.FindViewById(Resource.Id.thumbnailButton).Click -= new EventHandler(this.ContactBtn_Click);
            row.FindViewById(Resource.Id.thumbnailButton).Click += new EventHandler(this.ContactBtn_Click);



            if (mContacts[position].Image != null)
            {
                Picasso.With(mContext).Load(mContacts[position].Image).Into(row.FindViewById<ImageView>(Resource.Id.mainThumbail));
            }
            else
            {
                Picasso.With(mContext).Load(Resource.Drawable.ic_action_person).Into(row.FindViewById<ImageView>(Resource.Id.mainThumbail));
            }



        return row;
    }


        private void ContactBtn_Click(object sender, EventArgs e)
        {
            LinearLayout btn = (LinearLayout)sender;
            
            var ContactDetailActivity = new Intent(this.mContext, typeof(More_Contacts_Detail_Activity));
            ContactDetailActivity.PutExtra("ContactDataID", btn.Tag.ToString());
            this.mContext.StartActivity(ContactDetailActivity);

        }


}
}