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

namespace AWSBetting
{
    public class ActiveBetFragment : Fragment
    {
        List<Team> teams;

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
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            //View view = new View(Activity);
            var progressDialog = ProgressDialog.Show(Activity, "", "Loading bet", true);
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);

            progressDialog.Show();
           
            View view = inflater.Inflate(Resource.Layout.ActiveBet, null);
            

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



            new Thread(new ThreadStart(delegate
            {


                this.teams = AWSDataAccess.GetBetTeam(0);
                Thread.Sleep(1000);
                Activity.RunOnUiThread(() => {
                    ListView betList = view.FindViewById<ListView>(Resource.Id.ActiveBetList);


                    ViewGroup header = (ViewGroup)inflater.Inflate(Resource.Layout.ActiveBetHeader, betList, false);
                    betList.AddHeaderView(header, null, false);
                    betList.Adapter = new TeamListAdapter(Activity, this.teams);
                    betList.ItemClick += ActiveBetFragment_ItemClick;
                    
                });
                //Activity.RunOnUiThread(() => { });
                //Activity.RunOnUiThread(() => { });
                //betList.AddHeaderView(header, null, false);
                //betList.Adapter = new TeamListAdapter(Activity, this.teams);
                //betList.ItemClick += ActiveBetFragment_ItemClick;

                Thread.Sleep(1000);
                progressDialog.Dismiss();
            })).Start();
            //this.teams = AWSDataAccess.GetBetTeam(0);

            //ViewGroup header = (ViewGroup)inflater.inflate(R.layout.header, myListView, false);
            //myListView.addHeaderView(header, null, false);

            //betList.AddHeaderView(header, null, false);
            //betList.Adapter = new TeamListAdapter(Activity, this.teams);
            //betList.ItemClick += ActiveBetFragment_ItemClick;

            //progressDialog.Dismiss();

            return view;
            //AWSDataAccess.SelectActiveBetTeam()
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        



        private void ActiveBetFragment_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var calculateBetFragment = new CalculateBetFragment() { Arguments = new Bundle() };
            calculateBetFragment.Arguments.PutString("PID", teams[e.Position-1].Id.ToString());
            FragmentTransaction ft = this.Activity.FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.frameLayout1, calculateBetFragment,"CALCULATE");
            ft.Commit();
        }
    }

    

}