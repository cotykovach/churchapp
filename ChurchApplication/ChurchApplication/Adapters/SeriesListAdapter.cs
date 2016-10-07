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
    class SeriesListAdapter : BaseAdapter<Series>
{
    private Context mContext;
    private int mLayout;
    private List<Series> mSeries;
    public String seriesTitle;

        public SeriesListAdapter(Context context, int layout, List<Series> contacts)
    {
        mContext = context;
        mLayout = layout;
        mSeries = contacts;
    }

    public override Series this[int position]
    {
        get { return mSeries[position]; }
    }

    public override int Count
    {
        get { return mSeries.Count; }
    }

    public override long GetItemId(int position)
    {
        return position;
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {

            seriesTitle = null;
            View row = convertView;

            

            if (row == null)
        {
            row = LayoutInflater.From(mContext).Inflate(mLayout, parent, false);
        }

            row.FindViewById<ImageButton>(Resource.Id.seriesButton).Tag = mSeries[position].ID.ToString();
            row.FindViewById<ImageButton>(Resource.Id.seriesButton).Tag += "/"+mSeries[position].Title;

            row.FindViewById<ImageButton>(Resource.Id.seriesButton).Click -= new EventHandler(this.SeriesBtn_Click);
            seriesTitle = mSeries[position].Title;
            row.FindViewById<ImageButton>(Resource.Id.seriesButton).Click += new EventHandler(this.SeriesBtn_Click);





            if (mSeries[position].ImageBase64 != null)
            {

                Picasso.With(mContext).Load(mSeries[position].ImageBase64).Into(row.FindViewById<ImageButton>(Resource.Id.seriesButton));

            }
            else
            {
                row.FindViewById<ImageButton>(Resource.Id.seriesButton).SetImageResource(Resource.Drawable.defaultdonate);
            }

        return row;
    }

        private Bitmap GetImageBitmapFromUrl(string v)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(v);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        private void SeriesBtn_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            
            var SeriesSermonScrollActivity = new Intent(this.mContext, typeof(Worship_Series_Sermon_Scroll_Activity));
            string data = btn.Tag.ToString();
            int index = data.IndexOf("/");
            string id = (index > 0 ? data.Substring(0, index) : "");
            string title = data.Substring(data.LastIndexOf('/') + 1);
            SeriesSermonScrollActivity.PutExtra("SeriesDataID", id);
            SeriesSermonScrollActivity.PutExtra("SeriesTitle", title);
            this.mContext.StartActivity(SeriesSermonScrollActivity);
            
        }


}
}