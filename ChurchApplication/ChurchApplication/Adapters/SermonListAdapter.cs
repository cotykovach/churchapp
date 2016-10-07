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
using Square.Picasso;

namespace ChurchApplication
{
    class SermonListAdapter : BaseAdapter<Sermon>
{
    private Context mContext;
    private int mLayout;
    private List<Sermon> mSermons;

    public SermonListAdapter(Context context, int layout, List<Sermon> sermons)
    {
        mContext = context;
        mLayout = layout;
        mSermons = sermons;
    }

    public override Sermon this[int position]
    {
        get { return mSermons[position]; }
    }

    public override int Count
    {
        get { return mSermons.Count; }
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

        
        row.FindViewById<ImageButton>(Resource.Id.seriesButton).Tag = mSermons[position].ID.ToString();
        row.FindViewById<ImageButton>(Resource.Id.seriesButton).Click -= new EventHandler(this.SermonBtn_Click);
        row.FindViewById<ImageButton>(Resource.Id.seriesButton).Click += new EventHandler(this.SermonBtn_Click);

            if (mSermons[position].Image != null)
            {
                Picasso.With(mContext).Load(mSermons[position].Image).Into(row.FindViewById<ImageButton>(Resource.Id.seriesButton));
            }

            return row;
    }

        private void SermonBtn_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            
            
            var SermonDetailActivity = new Intent(this.mContext, typeof(Worship_Sermon_Detail_Activity));
            SermonDetailActivity.PutExtra("SermonDataID", btn.Tag.ToString());
            this.mContext.StartActivity(SermonDetailActivity);
            
            

        }

}
}