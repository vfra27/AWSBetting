using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;

namespace AWSBetting
{
    public class ProviderChoiceFragment : Android.Support.V4.App.DialogFragment
    {
        public static ProviderChoiceFragment NewInstance(Bundle bundle)
        {
            ProviderChoiceFragment fragment = new ProviderChoiceFragment();
            fragment.Arguments = bundle;
            return fragment;
        }

        public override void OnDestroy()
        {


            base.OnDestroy();

            //ViewPager pager = Activity.FindViewById<ViewPager>(Resource.Id.pager);
            //pager.
            //((TabsFragmentPagerAdapter)pager.Adapter).;
            //Android.Support.V4.App.FragmentTransaction ft = Activity.SupportFragmentManager.BeginTransaction();
            //ft.Replace(Resource.Id.frameLayout1, new HomeFragment());
            //ft.Commit();
            //if (FragmentManager.FindFragmentById<HomeFragment>(Resource.Id.home) !=null)
            //{


            //} 

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            Dialog.SetTitle("Betting Platform");
            View view =inflater.Inflate(Resource.Layout.ProviderChoiceDialog, container, false);

            Spinner betProviders = view.FindViewById<Spinner>(Resource.Id.betProviders);

            betProviders.Visibility = ViewStates.Visible;
            var providers = new List<BetProvider>() { BetProvider.PaddyPower, BetProvider.Bet365 };
            betProviders.Adapter = new ArrayAdapter<BetProvider>(Activity,
                Android.Resource.Layout.SimpleDropDownItem1Line, providers);


            betProviders.SetSelection(providers.IndexOf(ApplicationState.ActiveProvider));

            betProviders.ItemSelected += delegate
            {
                ApplicationState.ActiveProvider = providers[betProviders.SelectedItemPosition];

            };


            return view;
        }
        
    }
}