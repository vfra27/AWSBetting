using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using System.Collections.Generic;
using Android.Net;
using AWSBetting;

namespace AWSBetting
{
    [Activity(Label = "BettingPerding", MainLauncher = true, Icon = "@drawable/profit")]
    public class MainActivity : Activity
    {
        int count = 1;

        static readonly string Tag = "ActionBarTabsSupport";
        Fragment[] _fragments;
        
        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);
            #region old AmazonDynamoDb
            //AWSConfigs.AWSRegion = "us-east-1";
            //var loggingConfig = AWSConfigs.LoggingConfig;
            //loggingConfig.LogMetrics = true;
            //loggingConfig.LogResponses = ResponseLoggingOption.Always;
            //loggingConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
            //loggingConfig.LogTo = LoggingOptions.SystemDiagnostics;

            ////CognitoCachingCredentialsProvider
            //CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            //    "us-east-1:ec8dc78c-125d-48c7-bc83-1722ed1add43", // Identity Pool ID
            //    RegionEndpoint.USEast1);

            //var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
            //DynamoDBContext context = new DynamoDBContext(client);

            //Team retrievedTeam = context.LoadAsync<Team>(1);

            //Team juve = new Team()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Juventus"

            //};
            //context.SaveAsync(juve);
            #endregion

            #region Insert
            //Team t = new Team()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "PROVA2",
            //    Status = true,
            //    Bet = "X",
            //    Win = decimal.Parse("127,50")

            //};

            //Guid teamId = AWSDataAccess.InsertTeam(t);


            //decimal[] quantities = new decimal[]
            //{
            //    5,10
            //};

            //for (int i = 0; i < quantities.Length; i++)
            //{
            //    BetDetails detail = new BetDetails()
            //    {
            //        Id = Guid.NewGuid(),
            //        Quantity = quantities[i],
            //        Team_Id = teamId
            //    };

            //    AWSDataAccess.InsertBetDetails(detail);
            //}
            #endregion

            //List<Team> teams= AWSDataAccess.SelectActiveBetTeam();

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            SetContentView(Resource.Layout.Main);
            
            

            //List<Team> teams = new List<Team>();

            //ConnectivityManager connectivityManager = GetSystemService(ConnectivityService) as ConnectivityManager;
            //NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            //teams.Add(new Team()
            //{
            //    Name = "JUVENTUS",
            //    Bet = "WIN"
            //});

            //try
            //{
            //    teams = AWSDataAccess.GetBetTeam(0);
            //}
            //catch (Exception e)
            //{

            //    teams.Add(new Team()
            //    {
            //        Name = e.Message,
            //        Bet = e.Message
            //    });
            //}


            //if (activeConnection != null && activeConnection.IsConnected)
            //{
            //    

            //}
            //else
            //{
            //    teams.Add(new Team()
            //    {
            //        Name = "InternetError",
            //        Bet = "X"
            //    });
            //}

            _fragments = new Fragment[]
            {
                new HomeFragment(),
                new ActiveBetFragment(),
                new ClosedBetFragment(),
                new RankingsFragment(),
                new CalendarFragment()
            };

            AddTabToActionBar(Resource.String.Home_Tab_Label);
            AddTabToActionBar(Resource.String.ActiveBet_Tab_Label);
            AddTabToActionBar(Resource.String.ClosedBet_Tab_Label);
            AddTabToActionBar(Resource.String.Rankings_Tab_Label);
            AddTabToActionBar(Resource.String.Calendars_Tab_Label);

        }

        private ActionBar.Tab tab;

        void AddTabToActionBar(int labelResourceId)
        {
             this.tab = ActionBar.NewTab()
                .SetText(labelResourceId);           

            tab.TabSelected += TabOnTabSelected;
            ActionBar.AddTab(tab);
               
        }

        void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        {
            this.tab = sender as ActionBar.Tab;
            Fragment frag = _fragments[tab.Position];
            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);


        }

        public override void OnBackPressed()
        {
            //List<Team> teams = new List<Team>();
            //try
            //{
            //    teams = AWSDataAccess.GetBetTeam(0);
            //}
            //catch (Exception exp)
            //{

            //    teams.Add(new Team()
            //    {
            //        Name = exp.Message,
            //        Bet = exp.Message
            //    });
            //}
            AddBetFragment addBetFragment = FragmentManager.FindFragmentByTag("AddBet") as AddBetFragment;
            if (addBetFragment != null && addBetFragment.IsVisible)
            {
                FragmentTransaction ft = FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.frameLayout1, new HomeFragment());
                ft.Commit();
                return;
            }

            ModifyBetFragment modBetFragment = FragmentManager.FindFragmentByTag("ModifyBet") as ModifyBetFragment;
            if (modBetFragment != null && modBetFragment.IsVisible)
            {
                FragmentTransaction ft = FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.frameLayout1, new HomeFragment());
                ft.Commit();
                return;
            }

            CalculateBetFragment fragment = FragmentManager.FindFragmentByTag("CALCULATE") as CalculateBetFragment;
            if (fragment != null && fragment.IsVisible)
            {
                FragmentTransaction ft = FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.frameLayout1, new ActiveBetFragment());
                ft.Commit();
                return;
            }

            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
        

    }
}

