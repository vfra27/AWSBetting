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

namespace AWSBetting
{
    public class AddBetFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view= inflater.Inflate(Resource.Layout.AddBet, null);
            EditText teamName = view.FindViewById<EditText>(Resource.Id.teamName);
            EditText firstBet = view.FindViewById<EditText>(Resource.Id.firstBet);
            EditText betType = view.FindViewById<EditText>(Resource.Id.betType);
            Button saveBet = view.FindViewById<Button>(Resource.Id.addBetBtn);
            firstBet.FixDigits();


            saveBet.Click += delegate
             {
                 if (teamName.Text != string.Empty && firstBet.Text != string.Empty
                 && betType.Text != string.Empty)
                 {

                     Team t = new Team();
                     t.Name = teamName.Text;
                     t.Bet = betType.Text;
                     t.Id = Guid.NewGuid();
                     t.Status = false;
                     t.Win = 0;
                     t.BetProvider = ApplicationState.ActiveProvider;


                     if (AWSDataAccess.InsertTeam(t) != Guid.Empty)
                     {
                         BetDetails betDetails = new BetDetails();
                         betDetails.Id = Guid.NewGuid();
                         betDetails.Quantity = Convert.ToDecimal(firstBet.Text);
                         betDetails.Team_Id = t.Id;
                         if (AWSDataAccess.InsertBetDetails(betDetails) != Guid.Empty)
                         {
                             Toast.MakeText(Activity, "Bet added", ToastLength.Long).Show();
                             FragmentTransaction ft = FragmentManager.BeginTransaction();
                             ft.Replace(Resource.Id.frameLayout1, new HomeFragment());
                             ft.Commit();
                         }
                         else
                         {
                             Toast.MakeText(Activity, "Error in bet saving", ToastLength.Long).Show();

                         }

                     }
                     else
                     {
                         Toast.MakeText(Activity, "Error in bet saving", ToastLength.Long).Show();
                     }

                 }

             };

            

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}