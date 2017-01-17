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
using System.Threading;
using Android.Support.V4.App;
using Android.Support.V4.View;

namespace AWSBetting
{
    public class ClosedBetFragment : Android.Support.V4.App.Fragment
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

            View view = inflater.Inflate(Resource.Layout.ClosedBet, null);

            List<Team> teams = AWSDataAccess.GetBetTeam(1);
            ListView closedBetList = view.FindViewById<ListView>(Resource.Id.ClosedBetList);
            ViewGroup header = (ViewGroup)inflater.Inflate(Resource.Layout.ClosedBetHeader, closedBetList, false);
            closedBetList.AddHeaderView(header, null, false);
            closedBetList.Adapter = new ClosedBetAdapter(Activity, teams);

            #region with progress dialog
            //var progressDialog = ProgressDialog.Show(Activity, "", "Loading bet", true);
            //progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);

            //progressDialog.Show();

            //new Thread(new ThreadStart(delegate
            //{
            //    Thread.Sleep(1000);
            //    List<Team> teams = AWSDataAccess.GetBetTeam(1);
            //    Activity.RunOnUiThread(() => {
            //        ListView closedBetList = view.FindViewById<ListView>(Resource.Id.ClosedBetList);
            //        ViewGroup header = (ViewGroup)inflater.Inflate(Resource.Layout.ClosedBetHeader, closedBetList, false);
            //        closedBetList.AddHeaderView(header, null, false);
            //        closedBetList.Adapter = new ClosedBetAdapter(Activity, teams);
            //    });
            //    //Activity.RunOnUiThread(() => { });                


            //    Thread.Sleep(10);
            //    progressDialog.Dismiss();
            //})).Start();

            #endregion




            //List<Team> teams = AWSDataAccess.GetBetTeam(1);


            //closedBetList.AddHeaderView(header, null, false);
            //closedBetList.Adapter = new ClosedBetAdapter(Activity, teams);

            //view.FindViewById(Resource.Id.textView1);
            return view;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}