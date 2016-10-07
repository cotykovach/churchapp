using Android.App;
using Android.Views;
using Android.OS;

namespace ChurchApplication
{
    [Activity(Label = "Media: Gallery 1 Title", Icon = "@drawable/icon")]
    public class Media_Gallery_Detail_Activity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}