
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
    class VideoListAdapter : BaseAdapter<Videos>
    {
        private Context mContext;
        private int mLayout;
        private List<Videos> mVideos;
        public String videoTitle;

        public VideoListAdapter(Context context, int layout, List<Videos> videos)
        {
            mContext = context;
            mLayout = layout;
            mVideos = videos;
        }

        public override Videos this[int position]
        {
            get { return mVideos[position]; }
        }

        public override int Count
        {
            get { return mVideos.Count; }
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

            row.FindViewById<TextView>(Resource.Id.thumbnailTitle).Text = mVideos[position].Title;
            row.FindViewById(Resource.Id.thumbnailButton).Tag = mVideos[position].ID.ToString();

            row.FindViewById(Resource.Id.thumbnailButton).Click -= new EventHandler(this.VideoBtn_Click);
            row.FindViewById(Resource.Id.thumbnailButton).Click += new EventHandler(this.VideoBtn_Click);


            if (mVideos[position].Image != null)
            {
                Picasso.With(mContext).Load(mVideos[position].Image).Into(row.FindViewById<ImageView>(Resource.Id.mainThumbail));
            }
            else
            {
                Picasso.With(mContext).Load(Resource.Drawable.defaultvideoimage).Into(row.FindViewById<ImageView>(Resource.Id.mainThumbail));
            }


            return row;
        }

        private void VideoBtn_Click(object sender, EventArgs e)
        {
            LinearLayout btn = (LinearLayout)sender;

            var VideoDetailActivity = new Intent(this.mContext, typeof(Media_Video_Detail_Activity));
            VideoDetailActivity.PutExtra("VideoDataID", btn.Tag.ToString());
            this.mContext.StartActivity(VideoDetailActivity);

        }

    }
}