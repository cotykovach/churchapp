using System.Collections.Generic;


using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;


namespace ChurchApplication
{
    public class SlidingTabFragment : Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;
        public FrameLayout mFragement4Container;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Main, container, false);
            return inflater.Inflate(Resource.Layout.fragment_sample, container, false);
        }


        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SliderPagerAdapter();
            mSlidingTabScrollView.ViewPager = mViewPager;
        }


        public class SliderPagerAdapter : PagerAdapter
        {

            List<string> items = new List<string>();

            public SliderPagerAdapter() : base()
            {
                
                items.Add("Worship");
                items.Add("Events");
                items.Add("Media");
                items.Add("Give");
                items.Add("More");
                
            }

            public override int Count
            {
                get { return items.Count; }
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

          

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {

                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.default_pager_worship, container, false);
                container.AddView(view);
                int pos = position + 1;

                if (pos == 2)
                {
                        view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_default_button_scroll, container, false);
                        container.AddView(view);
                        ((MainActivity)container.Context).DownloadEventData();
                }


                if (pos == 3)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_media, container, false);
                    container.AddView(view);
                    
                }

                if (pos == 4)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_donation, container, false);
                    container.AddView(view);
                }


                if (pos == 5)
                {
                    view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_more, container, false);
                    container.AddView(view);
                }

                return view;
            }

            public string GetHeaderTitle(int position)
            {
                return items[position];
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }
        }


    }
}
 