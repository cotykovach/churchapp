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

namespace ChurchApplication
{
    class EventListAdapter : BaseAdapter<Event>
{
        private Context mContext;
        private int mLayout;
        private List<Event> mEvent;

        public EventListAdapter(Context context, int layout, List<Event> events)
    {
        mContext = context;
        mLayout = layout;
        mEvent = events;
    }


        public override Event this[int position]
    {
        get { return mEvent[position]; }
    }

    public override int Count
    {
        get { return mEvent.Count; }
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

        row.FindViewById<TextView>(Resource.Id.myImageViewText).Text = mEvent[position].Title+"\n"+mEvent[position].Date;
        row.FindViewById<ImageButton>(Resource.Id.seriesButton).Tag = mEvent[position].ID.ToString();
        row.FindViewById<ImageButton>(Resource.Id.seriesButton).Click -= new EventHandler(this.SeriesBtn_Click);
        row.FindViewById<ImageButton>(Resource.Id.seriesButton).Click += new EventHandler(this.SeriesBtn_Click);

            
            

        return row;
    }

        private void SeriesBtn_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
                
            var EventDetailActivity = new Intent(this.mContext, typeof(Event_Detail_Activity));
            EventDetailActivity.PutExtra("EventDataID", btn.Tag.ToString());
            this.mContext.StartActivity(EventDetailActivity);
            
            Console.WriteLine("Clicked Button! Tag Value:"+ btn.Tag.ToString());
            

        }

}
}