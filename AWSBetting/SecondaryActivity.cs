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
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.V4.App;
using Android.Support.Design.Widget;

namespace AWSBetting
{
    [Activity(Label = "Betting")]
    public class SecondaryActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Secondary);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.secondaryToolbar);
            SetSupportActionBar(toolbar);

            if (Intent!=null)
            {
                int fragmentNumber = Intent.GetIntExtra("fragmentNumber", 0);
                Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
                switch (fragmentNumber)
                {                    
                    case 1:                        
                        ft.Replace(Resource.Id.frame, new AddBetFragment());
                        ft.Commit();                        
                        break;
                    case 2:
                        ft.Replace(Resource.Id.frame, new ModifyBetFragment());
                        ft.Commit();
                        break;
                    case 3:
                        var calculateBetFragment = new CalculateBetFragment() { Arguments = new Bundle() };
                        calculateBetFragment.Arguments.PutString("PID", Intent.GetStringExtra("PID"));
                        calculateBetFragment.Arguments.PutString("Name", Intent.GetStringExtra("Name"));
                        this.Title += " on " + Intent.GetStringExtra("Name");
                        ft.Replace(Resource.Id.frame, calculateBetFragment);
                        ft.Commit();
                        break;
                    default:
                        break;
                }
            }

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {                
                Finish();
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            Finish();
        }

    }

   
}