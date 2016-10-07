
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

namespace ChurchApplication
{
    class AudioListAdapter : BaseAdapter<Audio>
{
    private Context mContext;
    private int mLayout;
    private List<Audio> mAudio;
    private Action<ImageView> mActionPicSelected;
    public String videoTitle;

        public AudioListAdapter(Context context, int layout, List<Audio> audio)
    {
        mContext = context;
        mLayout = layout;
        mAudio = audio;
        mActionPicSelected = null;
    }

    public override Audio this[int position]
    {
        get { return mAudio[position]; }
    }

    public override int Count
    {
        get { return mAudio.Count; }
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

            row.FindViewById<TextView>(Resource.Id.simpleTitle).Text = mAudio[position].Title;
            row.FindViewById(Resource.Id.simpleButton).Tag = mAudio[position].ID.ToString();
            
            row.FindViewById(Resource.Id.simpleButton).Click -= new EventHandler(this.AudioBtn_Click);
            row.FindViewById(Resource.Id.simpleButton).Click += new EventHandler(this.AudioBtn_Click);



            


        return row;
    }


        private void AudioBtn_Click(object sender, EventArgs e)
        {
            LinearLayout btn = (LinearLayout)sender;
            
            var AudioDetailActivity = new Intent(this.mContext, typeof(Media_Audio_Detail_Activity));
            AudioDetailActivity.PutExtra("AudioDataID", btn.Tag.ToString());
            this.mContext.StartActivity(AudioDetailActivity); 
        }
}
}