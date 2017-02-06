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
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using System.Threading;

namespace AWSBetting
{
    [Activity(Label = "BettingPerding", MainLauncher = true, Icon = "@drawable/profit", 
        WindowSoftInputMode =SoftInput.AdjustPan)]
    public class MainActivity : AppCompatActivity
    {
        int count = 1;

        static readonly string Tag = "ActionBarTabsSupport";
        //Fragment[] _fragments;

        TabLayout tabLayout;
                      

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

            #region OLD CODE
            //List<Team> teams= AWSDataAccess.SelectActiveBetTeam();

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };





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
            #endregion

            #region ActionBar Mode
            //ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            //SetContentView(Resource.Layout.Main);

            //_fragments = new Fragment[]
            //{
            //    new HomeFragment(),
            //    new ActiveBetFragment(),
            //    new ClosedBetFragment(),
            //    new RankingsFragment(),
            //    new CalendarFragment()
            //};

            //AddTabToActionBar(Resource.String.Home_Tab_Label);
            //AddTabToActionBar(Resource.String.ActiveBet_Tab_Label);
            //AddTabToActionBar(Resource.String.ClosedBet_Tab_Label);
            //AddTabToActionBar(Resource.String.Rankings_Tab_Label);
            //AddTabToActionBar(Resource.String.Calendars_Tab_Label);
            #endregion

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById
                <Android.Support.V7.Widget.Toolbar>(Resource.Id.mainToolbar);

            
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            //SupportActionBar.SetHomeButtonEnabled(true);

            tabLayout = FindViewById<TabLayout>(Resource.Id.tab_layout);


            tabLayout.SetTabTextColors(Android.Graphics.Color.Aqua,
                Android.Graphics.Color.AntiqueWhite);

            var viewPager = FindViewById<ViewPager>(Resource.Id.pager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, this);
            //viewPager.AddOnPageChangeListener(new ProgressDialogListener(this) {
            //});
            tabLayout.SetupWithViewPager(viewPager);
            //FnInitTabLayout();
            

        }

        void FnInitTabLayout()
        {

            #region oldcode
            //var fragments = new List<Android.Support.V4.App.Fragment>
            //{
            //    new HomeFragment(),
            //    new ActiveBetFragment(),
            //    new ClosedBetFragment(),
            //    new RankingsFragment(),
            //    new CalendarFragment(),                
            //};


            //var titles = CharSequence.ArrayFromStringArray(new[]
            //{
            //    Resources.GetString(Resource.String.Home_Tab_Label),
            //    Resources.GetString(Resource.String.ActiveBet_Tab_Label),
            //    Resources.GetString(Resource.String.ClosedBet_Tab_Label),
            //    Resources.GetString(Resource.String.Rankings_Tab_Label),
            //    Resources.GetString(Resource.String.Calendars_Tab_Label)
            //});
            #endregion

            //viewPager.Adapter = new SlidePagerAdapter(SupportFragmentManager);
            //tabLayout.SetupWithViewPager(viewPager);
        }

        


        #region ACTION BAR MODE
        //private ActionBar.Tab tab;

        //void AddTabToActionBar(int labelResourceId)
        //{
        //     this.tab = ActionBar.NewTab()
        //        .SetText(labelResourceId);           

        //    tab.TabSelected += TabOnTabSelected;
        //    ActionBar.AddTab(tab);

        //}

        //void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        //{
        //    this.tab = sender as ActionBar.Tab;
        //    Fragment frag = _fragments[tab.Position];
        //    tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);


        //}
        #endregion

        public override void OnBackPressed()
        {

            #region ACTION BAR MODE
            //AddBetFragment addBetFragment = FragmentManager.FindFragmentByTag("AddBet") as AddBetFragment;
            //if (addBetFragment != null && addBetFragment.IsVisible)
            //{
            //    FragmentTransaction ft = FragmentManager.BeginTransaction();
            //    ft.Replace(Resource.Id.frameLayout1, new HomeFragment());
            //    ft.Commit();
            //    return;
            //}

            //ModifyBetFragment modBetFragment = FragmentManager.FindFragmentByTag("ModifyBet") as ModifyBetFragment;
            //if (modBetFragment != null && modBetFragment.IsVisible)
            //{
            //    FragmentTransaction ft = FragmentManager.BeginTransaction();
            //    ft.Replace(Resource.Id.frameLayout1, new HomeFragment());
            //    ft.Commit();
            //    return;
            //}

            //CalculateBetFragment fragment = FragmentManager.FindFragmentByTag("CALCULATE") as CalculateBetFragment;
            //if (fragment != null && fragment.IsVisible)
            //{
            //    FragmentTransaction ft = FragmentManager.BeginTransaction();
            //    ft.Replace(Resource.Id.frameLayout1, new ActiveBetFragment());
            //    ft.Commit();
            //    return;
            //}

            //Finish();
            //Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            #endregion


            TabsFragmentPagerAdapter viewPager = FindViewById<ViewPager>(Resource.Id.pager).Adapter 
                as TabsFragmentPagerAdapter;

            if (viewPager.IsChildFragment)
            {
                viewPager.RemoveFragment();
                return;
            }
            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }


        public class ProgressDialogListener : ViewPager.SimpleOnPageChangeListener
        {
            private MainActivity mainActivity;

            public ProgressDialogListener(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }

            

            public override void OnPageSelected(int position)
            {
                //if (position == 0)
                //{

                //    var progressDialog = ProgressDialog.Show(mainActivity, "", "Creating plot", true);
                //    progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);

                //    progressDialog.Show();
                //    Thread.Sleep(2000);
                //    progressDialog.Dismiss();
                //}
            }
        }

    }

    

    
}

