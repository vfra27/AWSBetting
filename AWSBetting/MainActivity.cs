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
using Android.Provider;
using Android.Accounts;

namespace AWSBetting
{
    [Activity(Label = "BettingPerding", MainLauncher = true, Icon = "@drawable/profit",
        WindowSoftInputMode = SoftInput.AdjustPan)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        //int count = 1;

        //static readonly string Tag = "ActionBarTabsSupport";
        private static readonly int TIME_INTERVAL = 2000;
        private long mBackPressed;
        //Fragment[] _fragments;

        TabLayout tabLayout;
        DrawerLayout drawer;
        NavigationView navigationView;

        protected override void OnRestart()
        {
            if (navigationView != null)
            {
                navigationView.Menu.FindItem(Resource.Id.nav_home).SetChecked(true);                
            }
            base.OnRestart();
        }

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
            drawer = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);

            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            //SupportActionBar.SetHomeButtonEnabled(true);

            Android.Support.V7.App.ActionBarDrawerToggle toggle = new Android.Support.V7.App.ActionBarDrawerToggle(this,
                drawer, toolbar, Resource.String.Navigation_drawer_open, Resource.String.Navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            tabLayout = FindViewById<TabLayout>(Resource.Id.tab_layout);


            tabLayout.SetTabTextColors(Android.Graphics.Color.Aqua,
                Android.Graphics.Color.AntiqueWhite);

            var viewPager = FindViewById<ViewPager>(Resource.Id.pager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, this);
            //viewPager.AddOnPageChangeListener(new ProgressDialogListener(this) {
            //});
            tabLayout.SetupWithViewPager(viewPager);
            //FnInitTabLayout();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            navigationView.Menu.FindItem(Resource.Id.nav_home).SetChecked(true);

            string model = Build.Model;
            if (model.StartsWith("frd", StringComparison.CurrentCultureIgnoreCase))
            {
                ApplicationState.ActiveProvider = BetProvider.Bet365; //MODIFY only first run
                navigationView.Menu.FindItem(Resource.Id.nav_preferences).SetVisible(true);
            }
            else
            {
                navigationView.Menu.FindItem(Resource.Id.nav_preferences).SetVisible(false);
            }
            navigationView.SetNavigationItemSelectedListener(this);
            

            //ManageToast();

            //Account[] accounts = AccountManager.Get(this).GetAccountsByType("com.google");
            AccountManager manager = GetSystemService(AccountService) as AccountManager;
            Account[] list = manager.GetAccounts();

            
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

            if (drawer != null && drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                TabsFragmentPagerAdapter viewPager = FindViewById<ViewPager>(Resource.Id.pager).Adapter
                as TabsFragmentPagerAdapter;

                if (viewPager.IsChildFragment)
                {
                    viewPager.RemoveFragment();
                    return;
                }



                if (mBackPressed + TIME_INTERVAL > DateTime.Now.Millisecond)
                {
                    Finish();
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                }
                else
                {
                    Toast.MakeText(this, "Tap back 2 time to exit", ToastLength.Short).Show();
                }
                mBackPressed = DateTime.Now.Millisecond;

            }


        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode== Result.Ok)
            {
                string msg = data.GetStringExtra("type");
                Toast.MakeText(this, msg, ToastLength.Long).Show();
                TabsFragmentPagerAdapter adapter = FindViewById<ViewPager>
                        (Resource.Id.pager).Adapter as TabsFragmentPagerAdapter;
                adapter.Update();
            }


        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            Android.Support.V4.App.FragmentTransaction ft;
            switch (menuItem.ItemId)
            {
                

                case Resource.Id.nav_create:
                    Intent intent = new Intent(this, typeof(SecondaryActivity));
                    intent.PutExtra("fragmentNumber", 1);
                    StartActivityForResult(intent,0);


                    #region viewpager mode
                    //var addBetFragment = new AddBetFragment() { Arguments = new Bundle() };
                    ////calculateBetFragment.Arguments.PutString("PID", teams[e.Position - 1].Id.ToString());                        
                    //TabsFragmentPagerAdapter adapter = FindViewById<ViewPager>
                    //(Resource.Id.pager).Adapter as TabsFragmentPagerAdapter;                                            
                    //adapter.AddFragment(addBetFragment, 0);
                    #endregion
                    break;
                case Resource.Id.nav_edit:
                    Intent intent2 = new Intent(this, typeof(SecondaryActivity));
                    intent2.PutExtra("fragmentNumber", 2);
                    StartActivityForResult(intent2,1);

                    #region viewpager mode
                    //var modBetFragment = new ModifyBetFragment() { Arguments = new Bundle() };                    
                    //TabsFragmentPagerAdapter adp = FindViewById<ViewPager>
                    //(Resource.Id.pager).Adapter as TabsFragmentPagerAdapter;
                    //adp.AddFragment(modBetFragment, 0);
                    #endregion
                    break;
                case Resource.Id.nav_close:
                    ft = SupportFragmentManager.BeginTransaction();
                    //Remove fragment else it will crash as it is already added to backstack
                    Android.Support.V4.App.Fragment prev = SupportFragmentManager
                    .FindFragmentByTag("closeBetDialog");
                    if (prev != null)
                    {
                        ft.Remove(prev);
                    }

                    ft.AddToBackStack(null);

                    // Create and show the dialog.
                    CloseBetDialogFragment newFragment = CloseBetDialogFragment.NewInstance(null);
                    newFragment.SetStyle(Android.Support.V4.App.DialogFragment.StyleNormal, Resource.Style.CustomDialog);
                    //Add fragment
                    newFragment.Show(ft, "closeBetDialog");
                    break;
                case Resource.Id.nav_preferences:
                    ft = SupportFragmentManager.BeginTransaction();
                    Android.Support.V4.App.Fragment p = SupportFragmentManager
                    .FindFragmentByTag("providerChoiceDialog");
                    if (p != null)
                    {
                        ft.Remove(p);
                    }
                    ft.AddToBackStack(null);
                    ProviderChoiceFragment pcFragment = ProviderChoiceFragment.NewInstance(null);
                    pcFragment.SetStyle(Android.Support.V4.App.DialogFragment.StyleNormal, Resource.Style.CustomDialog);
                    pcFragment.Show(ft, "providerChoiceDialog");
                    break;
                default:
                    break;
            }
            drawer.CloseDrawer(GravityCompat.Start);
            return true;

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

