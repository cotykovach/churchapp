
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
    class GalleryListAdapter : BaseAdapter<Gallery>
{
    private Context mContext;
    private int mLayout;
    private List<Gallery> mGallery;

        public GalleryListAdapter(Context context, int layout, List<Gallery> gallery)
    {
        mContext = context;
        mLayout = layout;
        mGallery = gallery;
    }

    public override Gallery this[int position]
    {
        get { return mGallery[position]; }
    }

    public override int Count
    {
        get { return mGallery.Count; }
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

            row.FindViewById<TextView>(Resource.Id.simpleTitle).Text = mGallery[position].Title;
            row.FindViewById(Resource.Id.simpleButton).Tag = mGallery[position].ID.ToString();
            
            row.FindViewById(Resource.Id.simpleButton).Click -= new EventHandler(this.GalleryBtn_Click);
            row.FindViewById(Resource.Id.simpleButton).Click += new EventHandler(this.GalleryBtn_Click);



            


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

        private void GalleryBtn_Click(object sender, EventArgs e)
        {
            LinearLayout btn = (LinearLayout)sender;
            
            var GalleryDetailActivity = new Intent(this.mContext, typeof(Media_Gallery_Detail_Activity));
            GalleryDetailActivity.PutExtra("GalleryDataID", btn.Tag.ToString());
            this.mContext.StartActivity(GalleryDetailActivity);
        }

   
}
}