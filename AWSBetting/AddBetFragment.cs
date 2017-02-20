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
using Android.Support.V7.Widget;
namespace AWSBetting
{
    public class AddBetFragment : Android.Support.V4.App.Fragment
    {
        

        public static AddBetFragment InstantiateItem()
        {
            AddBetFragment addFrag = new AddBetFragment();
            
            return addFrag;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view= inflater.Inflate(Resource.Layout.AddBet, null);

            SwitchCompat rechargeSwt = view.FindViewById<SwitchCompat>(Resource.Id.rechargeSwt);
            EditText teamName = view.FindViewById<EditText>(Resource.Id.teamName);
            //EditText recharge = view.FindViewById<EditText>(Resource.Id.recharge);
            EditText firstBet = view.FindViewById<EditText>(Resource.Id.firstBet);
            EditText betType = view.FindViewById<EditText>(Resource.Id.betType);
            Button saveBet = view.FindViewById<Button>(Resource.Id.addBetBtn);
            firstBet.FixDigits();

            rechargeSwt.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e)
             {
                 if (e.IsChecked)
                 {
                     //teamName.Visibility = ViewStates.Visible;
                     teamName.InputType = Android.Text.InputTypes.TextVariationNormal;
                     teamName.Hint = "Enter team name";
                     firstBet.Visibility = ViewStates.Visible;
                     betType.Visibility = ViewStates.Visible;
                     //saveBet.Visibility = ViewStates.Visible;
                     //recharge.Visibility = ViewStates.Invisible;
                 }
                 else
                 {
                                          
                     teamName.InputType = Android.Text.InputTypes.ClassNumber;
                     teamName.FixDigits();
                     teamName.Hint = "Enter recharge amount";
                     //recharge.Visibility = ViewStates.Visible;
                     //teamName.Visibility = ViewStates.Invisible;
                     firstBet.Visibility = ViewStates.Invisible;
                     betType.Visibility = ViewStates.Invisible;
                     //saveBet.Visibility = ViewStates.Invisible;
                 }
             };

            saveBet.Click += delegate
             {


                 if (rechargeSwt.Checked)
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
                         t.TotalCost = 0;
                         t.BetProvider = ApplicationState.ActiveProvider;


                         if (AWSDataAccess.InsertTeam(t) != Guid.Empty)
                         {
                             BetDetails betDetails = new BetDetails();
                             betDetails.Id = Guid.NewGuid();
                             betDetails.Quantity = Convert.ToDecimal(firstBet.Text);
                             betDetails.Team_Id = t.Id;
                             if (AWSDataAccess.InsertBetDetails(betDetails) != Guid.Empty)
                             {

                                 Intent intent = new Intent();
                                 intent.PutExtra("type", Resources.GetString(Resource.String.addBet));                                 
                                 Activity.SetResult(Result.Ok, intent);
                                 Activity.Finish();

                                 #region old code
                                 //Intent intent = new Intent(Activity, typeof(MainActivity));
                                 //intent.PutExtra("result", true);

                                 //Toast.MakeText(Activity, "Bet added", ToastLength.Long).Show();
                                 //TabsFragmentPagerAdapter adapter = Activity.FindViewById<ViewPager>
                                 //(Resource.Id.pager).Adapter as TabsFragmentPagerAdapter;
                                 //adapter.RemoveFragment();

                                 //Android.Support.V4.App.FragmentTransaction ft = FragmentManager.BeginTransaction();
                                 //ft.Replace(Resource.Id.frameLayout1, new HomeFragment());
                                 //ft.Commit();
                                 #endregion
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
                 }
                 else
                 {
                     if (teamName.Text != string.Empty)
                     {
                         Recharge r = new Recharge()
                         {
                             Id= Guid.NewGuid(),
                             Amount = Convert.ToDecimal(teamName.Text),
                             Date = DateTime.Now,
                             BetProvider = ApplicationState.ActiveProvider
                         };
                         if (AWSDataAccess.InsertRecharge(r) != Guid.Empty)
                         {
                             Toast.MakeText(Activity, "Recharge added", ToastLength.Long).Show();
                             TabsFragmentPagerAdapter adapter = Activity.FindViewById<ViewPager>
                             (Resource.Id.pager).Adapter as TabsFragmentPagerAdapter;                             
                             adapter.RemoveFragment();
                         }
                         else
                         {
                             Toast.MakeText(Activity, "Error in recharge saving", ToastLength.Long).Show();
                         }
                     }
                 }                 

             };
            

            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}