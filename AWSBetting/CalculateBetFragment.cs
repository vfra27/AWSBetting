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
using Android.Views.InputMethods;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;

namespace AWSBetting
{
    public class CalculateBetFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.CalculateBet, null);

            
            //((AppCompatActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //((AppCompatActivity)Activity).SupportActionBar.SetHomeButtonEnabled(true);            


            List<BetDetails> betDetails = AWSDataAccess.GetBetDetailsByTeamId(TeamId);
            string history = string.Empty;

            foreach (var item in betDetails)
            {
                int temp = (int)item.Quantity;
                history += temp + ",";

            }

            history = history.Substring(0,history.LastIndexOf(','));

            view.FindViewById<EditText>(Resource.Id.History).Text = history;

            view.FindViewById<EditText>(Resource.Id.Odds).FixDigits();
            Button calculateBtn = view.FindViewById<Button>(Resource.Id.Calculate);
            EditText historyTxt = view.FindViewById<EditText>(Resource.Id.History);
            EditText odds = view.FindViewById<EditText>(Resource.Id.Odds);
            EditText nextbet = view.FindViewById<EditText>(Resource.Id.NextBet);
            EditText profit = view.FindViewById<EditText>(Resource.Id.Profit);
            EditText teamName = view.FindViewById<EditText>(Resource.Id.TeamName);
            teamName.Text = TeamName;  
            Button saveBetBtn = view.FindViewById<Button>(Resource.Id.SaveBet);


            saveBetBtn.Click += delegate
            {
                if (nextbet.Text != String.Empty)
                {

                    if (!string.Equals(TeamName,teamName.Text,StringComparison.OrdinalIgnoreCase))
                    {
                        Team t = new Team()
                        {
                            Id = TeamId,
                            Name = teamName.Text                                                       
                        };
                        if (AWSDataAccess.UpdateTeamName(t)== Guid.Empty)
                        {
                            Toast.MakeText(Activity, "Error in team updating", ToastLength.Long).Show();
                            return;
                        }
                        
                    }

                    BetDetails newBet = new BetDetails()
                    {
                        Id = Guid.NewGuid(),
                        Quantity = Decimal.Parse(nextbet.Text),
                        Team_Id = TeamId
                    };

                    if (AWSDataAccess.InsertBetDetails(newBet) != Guid.Empty)
                    {
                        Toast.MakeText(Activity, "Bet saved", ToastLength.Long).Show();
                        //Android.Support.V4.App.FragmentTransaction ft = Activity.SupportFragmentManager
                        //.BeginTransaction();
                        //ft.Replace(Resource.Id.root_frame, new ActiveBetFragment());
                        //ft.Commit();
                        TabsFragmentPagerAdapter adapter = Activity.FindViewById<ViewPager>
                        (Resource.Id.pager).Adapter as TabsFragmentPagerAdapter;
                        adapter.RemoveFragment();
                    }
                    else
                    {
                        Toast.MakeText(Activity, "Error in bet saving", ToastLength.Long).Show();
                    } 

                }
                else
                {
                    return;
                }

            };

            calculateBtn.Click += delegate
            {

                InputMethodManager inputManager = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);

                //inputManager.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
                if (Activity.CurrentFocus != null)
                {
                    inputManager.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
                }

                //&& offset.Text != string.Empty
                if (historyTxt.Text != string.Empty && odds.Text != string.Empty)
                {
                    BettingPerding result = new BettingPerding();

                    int offsetVal=0;
                    //if (offset.Text!= string.Empty)
                    //{
                    //    Int32.Parse(offset.Text);
                    //}

                    result = BetCalculator.Calculate(historyTxt.Text,
                        Double.Parse(odds.Text), 0, offsetVal);

                    //nextbet.SetText(ciccio.ToString(),TextView.BufferType.Normal);
                    profit.Text = "" + result.Profit;
                    nextbet.Text = "" + result.NextBet;
                }
                else
                {
                    odds.Error = "Requested";
                    //offset.Error = "Requested";
                }


                
            };

            nextbet.TextChanged += delegate
            {
                //&& offset.Text != string.Empty

                if (historyTxt.Text!=string.Empty && odds.Text!=string.Empty )
                {
                    BettingPerding result = new BettingPerding();
                    int offsetValue = 0;
                    //if (offset.Text!=string.Empty)
                    //{
                    //    offsetValue = Int32.Parse(offset.Text);
                    //}

                    double next = 0;
                    if (!String.IsNullOrEmpty(nextbet.Text))
                    {
                        next = Double.Parse(nextbet.Text);
                    }
                    result = BetCalculator.Calculate(historyTxt.Text,
                        Double.Parse(odds.Text), next, offsetValue);


                    profit.Text = "" + result.Profit;

                }

                


            };

            return view;
        }


        public string TeamName
        {
            get {
                return Arguments.GetString("Name");
            }
                
        }


        public Guid TeamId
        {
            get { return new Guid(Arguments.GetString("PID", "0")); }
                
        }

    }
}