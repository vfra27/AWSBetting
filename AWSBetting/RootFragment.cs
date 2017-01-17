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

namespace AWSBetting
{
    public class RootFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view= inflater.Inflate(Resource.Layout.Root, container, false);

            Android.Support.V4.App.FragmentTransaction ft = Activity.SupportFragmentManager
                .BeginTransaction();

            ft.Replace(Resource.Id.root_frame, new HomeFragment());
            ft.Commit();
            return view;
        }
    }
}