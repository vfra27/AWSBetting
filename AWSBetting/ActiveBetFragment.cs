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
using Android.Net;
using System.Threading;
using System.Threading.Tasks;
using Android.Support.V4.App;
using Android.Support.V4.View;

namespace AWSBetting
{
    public class ActiveBetFragment : Android.Support.V4.App.Fragment
    {
        List<Team> teams;
        ListView betList;

        public ActiveBetFragment():base()
        {
            
        }

        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            
            View view = inflater.Inflate(Resource.Layout.ActiveBet, null);

            //FetchingList task = new FetchingList(Activity);
            //task.Execute("");

            Task.Run(() => SetupData());
            //this.teams = AWSDataAccess.GetBetTeam(0);

            //foreach (Team item in this.teams)
            //{
            //    List<BetDetails> betDetails = AWSDataAccess.GetBetDetailsByTeamId(item.Id);
            //    item.LastBet = AWSDataAccess.DoFormat(betDetails[betDetails.Count - 1].Quantity);
            //}

            betList = view.FindViewById<ListView>(Resource.Id.ActiveBetList);
            

            ViewGroup header = (ViewGroup)inflater.Inflate(Resource.Layout.ActiveBetHeader, betList, false);
            betList.AddHeaderView(header, null, false);
            //betList.Adapter = new TeamListAdapter(Activity, this.teams);
            betList.ItemClick += ActiveBetFragment_ItemClick;



            #region with progress dialog
            //var progressDialog = ProgressDialog.Show(Activity, "", "Loading bet", true);
            //progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            //progressDialog.Show();
            //new Thread(new ThreadStart(delegate
            //{


            //    this.teams = AWSDataAccess.GetBetTeam(0);
            //    Thread.Sleep(1000);
            //    Activity.RunOnUiThread(() => {
            //        ListView betList = view.FindViewById<ListView>(Resource.Id.ActiveBetList);
            //        //ListView betList = view.FindViewById<ListView>(Resource.Id.ActiveBetList);

            //        ViewGroup header = (ViewGroup)inflater.Inflate(Resource.Layout.ActiveBetHeader, betList, false);
            //        betList.AddHeaderView(header, null, false);
            //        betList.Adapter = new TeamListAdapter(Activity, this.teams);
            //        betList.ItemClick += ActiveBetFragment_ItemClick;

            //    });


            //    Thread.Sleep(1000);
            //    progressDialog.Dismiss();
            //})).Start();
            #endregion




            #region Task List method
            //Task<List<Team>> task1 = Task.Factory.StartNew(() => { return AWSDataAccess.GetBetTeam(0); }
            //);


            //Task task2 = task1.ContinueWith((antecedent) =>
            //{
            //    try
            //    {
            //        //progressDialog.Dismiss();

            //        betList.AddHeaderView(header, null, false);
            //        this.teams = task1.Result;
            //        betList.Adapter = new TeamListAdapter(Activity, teams);
            //        betList.ItemClick += ActiveBetFragment_ItemClick;

            //        progressDialog.Dismiss();
            //    }
            //    catch (AggregateException aex)
            //    {
            //        //Toast.MakeText(this, aex.InnerException.Message, ToastLength.Short).Show();
            //    }
            //}, TaskScheduler.FromCurrentSynchronizationContext()
            //);
            #endregion


            #region old code
            //this.teams = AWSDataAccess.GetBetTeam(0);

            //ViewGroup header = (ViewGroup)inflater.inflate(R.layout.header, myListView, false);
            //myListView.addHeaderView(header, null, false);

            //betList.AddHeaderView(header, null, false);
            //betList.Adapter = new TeamListAdapter(Activity, this.teams);
            //betList.ItemClick += ActiveBetFragment_ItemClick;

            //progressDialog.Dismiss();
            #endregion
            return view;
            //AWSDataAccess.SelectActiveBetTeam()
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        async Task SetupData()
        {
            await Task.Run(async () =>
            {
                 this.teams = AWSDataAccess.GetBetTeam(0);
                await Task.Delay(0);
                foreach (Team item in this.teams)
                {                    
                    List<BetDetails> betDetails = AWSDataAccess.GetBetDetailsByTeamId(item.Id);
                    item.LastBet = AWSDataAccess.DoFormat(betDetails[betDetails.Count - 1].Quantity);
                }
                Activity.RunOnUiThread(() =>
                {
                    betList.Adapter = new TeamListAdapter(Activity, this.teams);
                });

            });
        }

        public override bool UserVisibleHint
        {
            get
            {
                return base.UserVisibleHint;
            }

            set
            {
                //ViewPager pager = Activity.FindViewById<ViewPager>(Resource.Id.pager);
                if (value==true)
                {
                    
                }
                base.UserVisibleHint = value;
            }
        }


        private void ActiveBetFragment_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(Activity, typeof(SecondaryActivity));
            intent.PutExtra("fragmentNumber", 3);
            intent.PutExtra("PID", teams[e.Position - 1].Id.ToString());
            intent.PutExtra("Name", teams[e.Position - 1].Name);
            StartActivityForResult(intent,2);

            #region viewpager mode
            //var calculateBetFragment = new CalculateBetFragment() { Arguments = new Bundle() };
            //calculateBetFragment.Arguments.PutString("PID", teams[e.Position-1].Id.ToString());
            //calculateBetFragment.Arguments.PutString("Name", teams[e.Position - 1].Name);
            ////Android.Support.V4.App.FragmentTransaction ft = this.Activity.SupportFragmentManager.BeginTransaction();
            ////ft.Replace(Resource.Id.root_frame, calculateBetFragment,"CALCULATE");
            ////ft.Commit();
            //TabsFragmentPagerAdapter adapter = Activity.FindViewById<ViewPager>
            //            (Resource.Id.pager).Adapter as TabsFragmentPagerAdapter;
            //adapter.AddFragment(calculateBetFragment,1);
            #endregion
        }
    }

     class FetchingList : AsyncTask<string, int, string>
    {

        List<Team> teams = new List<Team>();

        Activity ctx;

        public FetchingList(Context ctx)
        {
            this.ctx = ctx as Activity;
        }
        protected override void OnPostExecute(string result)
        {
            if (result == "OK")
            {
                
                ListView listView = ctx.FindViewById<ListView>(Resource.Id.ActiveBetList);
                listView.Adapter = new TeamListAdapter(ctx, this.teams);
            }
        }

        protected override string RunInBackground(params string[] @params)
        {

            teams = AWSDataAccess.GetBetTeam(0);
            foreach (Team item in teams)
            {
                List<BetDetails> betDetails = AWSDataAccess.GetBetDetailsByTeamId(item.Id);
                item.LastBet = AWSDataAccess.DoFormat(betDetails[betDetails.Count - 1].Quantity);
            }

            return "OK";
        }
        
    }


}