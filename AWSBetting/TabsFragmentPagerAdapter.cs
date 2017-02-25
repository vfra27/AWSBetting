using System;
using Android.Support.V4.App;
using Java.Lang;
using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;

namespace AWSBetting
{
    public class TabsFragmentPagerAdapter : FragmentStatePagerAdapter
    {

        //Fragment[] fragments private readonly Fragment[] fragments;
        //private readonly ICharSequence[] titles;
        //private List<Fragment> fragments;
        private FragmentManager fm;
        //List<Fragment> fragments,
        //    ICharSequence[] titles
        private Context context;
        private readonly int NUM_ITEMS = 5;
        private Fragment homeChildFragment;
        private Fragment activeBetChildFragment;
        private bool isUpdate = true;

        public TabsFragmentPagerAdapter(FragmentManager fm, Context context): base(fm)
        {
            this.fm = fm;
            this.context = context;
            //this.fragments = fragments;
            //this.titles = titles;
        }

        public TabsFragmentPagerAdapter(FragmentManager fm):base(fm)
        {

        }

        public void UpdateViews()
        {
            isUpdate = false;
            NotifyDataSetChanged();
        }

        public void AddFragment(Fragment fragment, int type)
        {
            //if (fm.FindFragmentByTag("AddBet")!= null)
            //{

            //}
            //this.fragments.Add(fragment);
            switch (type)
            {
                case 0:
                    homeChildFragment = fragment;
                    break;
                case 1:
                    activeBetChildFragment = fragment;
                    break;                   
                default:
                    break;
            }            
            NotifyDataSetChanged();
        }

        public void RemoveFragment()
        {
            activeBetChildFragment = null;
            homeChildFragment= null;
            NotifyDataSetChanged();
        }

        public void Update()
        {
            NotifyDataSetChanged();
        }

        public override int Count
        {
            get
            {
                return NUM_ITEMS;
            }
        }

        public override int GetItemPosition(Java.Lang.Object @object)
        {
            //if (childFragment.GetType() == typeof(AddBetFragment))
            //{
            //    FragmentTransaction ft = fm.BeginTransaction();
            //    ft.Replace(Resource.Id.home, fragment, "AddBet");
            //    ft.Commit();
            //    childFragment = fragment;                
            //}                        
            return PositionNone;
            
        }

        //private bool isChildFragment = false;
        public bool IsChildFragment {


            get {


                return (homeChildFragment == null && activeBetChildFragment == null ? false : true);
                //if (homeChildFragment == null && activeBetChildFragment == null)
                //{
                //    return false;
                //}
                //else
                //    return true;                
            }
        }

        public override Fragment GetItem(int position)
        {
            //AddBetFragment addBetFragment= fm.FindFragmentByTag("AddBet") as AddBetFragment;
            //if (addBetFragment != null)
            //{
            //    return addBetFragment;
            //}          
            switch (position)
            {
                case 0:
                    if (homeChildFragment != null)
                    {
                        return homeChildFragment;

                    }
                    return new OddsFragment();
                    //return HomeFragment.NewInstance(context);                    
                case 1:
                    if (activeBetChildFragment != null)
                    {
                        return activeBetChildFragment;
                    }
                    return new ActiveBetFragment();
                case 2:                    
                    return new ClosedBetFragment();
                case 3:
                    return new RankingsFragment();
                case 4:
                    return new CalendarFragment();                                                                       
                default:
                    return null;
            }


            //return fragments[position];
        }
            

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            //Resources.GetString(Resource.String.Home_Tab_Label),
            //    Resources.GetString(Resource.String.ActiveBet_Tab_Label),
            //    Resources.GetString(Resource.String.ClosedBet_Tab_Label),
            //    Resources.GetString(Resource.String.Rankings_Tab_Label),
            //    Resources.GetString(Resource.String.Calendars_Tab_Label)
            

            switch (position)
            {
                case 0:
                    //context.Resources.GetString(Resource.String.Home_Tab_Label);
                    return new Java.Lang.String(context.Resources.GetString(Resource.String.Home_Tab_Label));
                case 1:
                    return new Java.Lang.String(context.Resources.GetString(Resource.String.ActiveBet_Tab_Label));
                case 2:
                    return new Java.Lang.String(context.Resources.GetString(Resource.String.ClosedBet_Tab_Label));
                case 3:
                    return new Java.Lang.String(context.Resources.GetString(Resource.String.Rankings_Tab_Label));
                case 4:
                    return new Java.Lang.String(context.Resources.GetString(Resource.String.Calendars_Tab_Label));
                default:
                    return null;
            }
        }
    }
}