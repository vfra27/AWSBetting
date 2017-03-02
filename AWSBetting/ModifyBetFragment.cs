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
    public class ModifyBetFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        private ProgressDialog progressDialog;
        private List<Team> activeBetTeams= new List<Team>();
        private List<BetDetails> betDetails = new List<BetDetails>();
        private EditText lastBet;
        private EditText betType;
        private EditText betTeamName;
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view =inflater.Inflate(Resource.Layout.ModifyBet, container, false);
            //Button saveMod = view.FindViewById<Button>(Resource.Id.modSave);
            //Button deleteMod = view.FindViewById<Button>(Resource.Id.modDelete);
            //Button deleteAllMod = view.FindViewById<Button>(Resource.Id.modDeleteAll);


            this.progressDialog = ProgressDialog.Show(Activity, "", "Loading bet", true);
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);

            progressDialog.Show();


            this.lastBet = view.FindViewById<EditText>(Resource.Id.modLastBet);
            this.betType = view.FindViewById<EditText>(Resource.Id.modBetType);
            this.betTeamName = view.FindViewById<EditText>(Resource.Id.modBetTeamName);
            lastBet.FixDigits();
            Spinner betTeamsSpinner = view.FindViewById<Spinner>(Resource.Id.modTeamName);

            

           Thread mainThread=  new Thread(new ThreadStart(delegate
            {
                //List<Team> activeBetTeams = new List<Team>();

                activeBetTeams = AWSDataAccess.GetBetTeam(0);

                Activity.RunOnUiThread(() => { betTeamsSpinner.Adapter = 
                    new SpinnerTeamAdapter(Activity, activeBetTeams); });
                #region old code
                //if (activeBetTeams.Count > 0)
                //{
                //    List<BetDetails> betDetails = AWSDataAccess.GetBetDetailsByTeamId(
                //        activeBetTeams[0].Id);
                //    if (betDetails.Count > 0)
                //    {
                //        Activity.RunOnUiThread(() =>
                //        {
                //            lastBet.Text = AWSDataAccess.DoFormat(betDetails
                //                [betDetails.Count - 1].Quantity);
                //        });

                //        Activity.RunOnUiThread(() => { betType.Text = activeBetTeams[0].Bet; });
                //    }
                //}

                //});

                //betList.AddHeaderView(header, null, false);
                //betList.Adapter = new TeamListAdapter(Activity, this.teams);
                //betList.ItemClick += ActiveBetFragment_ItemClick;
                #endregion
                Thread.Sleep(10);
                progressDialog.Dismiss();
                running = 0;
            }));

            mainThread.Name = "ModifyMain";
            mainThread.Start();


            #region Button click

            //saveMod.Click += delegate
            //{
            //    if (lastBet.Text != string.Empty)
            //    {
            //        BetDetails modifiedTeamDetail = new BetDetails()
            //        {
            //            Id = betDetails[betDetails.Count - 1].Id,
            //            Quantity = Decimal.Parse(lastBet.Text),
            //            Team_Id = activeBetTeams[betTeamsSpinner.SelectedItemPosition].Id,
            //        };
            //        if (AWSDataAccess.UpdateBetTeamDetail(modifiedTeamDetail)!=Guid.Empty)
            //        {
            //            Toast.MakeText(Activity, "Bet updated", ToastLength.Long).Show();
            //            BackHome();
            //        }
            //        else
            //        {
            //            Toast.MakeText(Activity, "Error in bet updating", ToastLength.Long).Show();
            //        }
            //    }
            //    else
            //    {
            //        lastBet.Error = "Required!";
            //    }


            //};

            //deleteMod.Click += delegate
            // {
            //     if (lastBet.Text != string.Empty)
            //     {
            //         BetDetails modifiedTeamDetail = new BetDetails()
            //         {
            //             Id = betDetails[betDetails.Count - 1].Id,
            //             Quantity = Decimal.Parse(lastBet.Text),
            //             Team_Id = activeBetTeams[betTeamsSpinner.SelectedItemPosition].Id,
            //         };
            //         if (AWSDataAccess.DeleteBetDetail(modifiedTeamDetail))
            //         {
            //             Toast.MakeText(Activity, "Bet deleted", ToastLength.Long).Show();
            //             BackHome();
            //         }
            //         else
            //         {
            //             Toast.MakeText(Activity, "Error in bet deleting", ToastLength.Long).Show();
            //         }
            //     }
            //     else
            //     {
            //         lastBet.Error = "Required!";
            //     }
            // };

            //deleteAllMod.Click += delegate
            //{
            //    Team t = new Team()
            //    {
            //        Id = activeBetTeams[betTeamsSpinner.SelectedItemPosition].Id,
            //    };

            //    if (AWSDataAccess.DeleteAllBetTeam(t))
            //    {
            //        Toast.MakeText(Activity, "All team bet deleted", ToastLength.Long).Show();
            //        BackHome();
            //    }
            //    else
            //    {
            //        Toast.MakeText(Activity, "Error in team bet deleting", ToastLength.Long).Show();
            //    }
            //};

            #endregion

            var editToolbar = view.FindViewById<Toolbar>(Resource.Id.modifyBetToolbar);
            //editToolbar.Title = "Editing";
            editToolbar.InflateMenu(Resource.Menu.modifyBet_menus);
            #region old code
            //Spinner betProviders= editToolbar.FindViewById<Spinner>(Resource.Id.providerSpinnerToolbar);
            //var providers = new List<BetProvider>() { BetProvider.PaddyPower, BetProvider.Bet365 };
            //betProviders.Adapter = new ArrayAdapter<BetProvider>(Activity,
            //    Android.Resource.Layout.SimpleDropDownItem1Line, providers);

            //betProviders.ItemSelected += delegate
            //{
            //    ApplicationState.ActiveProvider = providers[betProviders.SelectedItemPosition];

            //};

            //Spinner providerSpinner = (Spinner) editToolbar.FindViewById<Resource.Id.sp
            #endregion
            editToolbar.MenuItemClick += (sender, e) =>
            {                
                switch (e.Item.ItemId)
                {
                    case Resource.Id.menu_save:
                        if (lastBet.Text != string.Empty && betTeamName.Text!= string.Empty)
                        {

                            Team teamModified = new Team()
                            {
                                Id = selectedTeamId,
                                Name = betTeamName.Text
                            };

                            if (AWSDataAccess.UpdateTeamName(teamModified)!= Guid.Empty)
                            {
                                BetDetails modifiedTeamDetail = new BetDetails()
                                {
                                    Id = betDetails[betDetails.Count - 1].Id,
                                    Quantity = Decimal.Parse(lastBet.Text),
                                    Team_Id = activeBetTeams[betTeamsSpinner.SelectedItemPosition].Id,
                                };
                                if (AWSDataAccess.UpdateBetTeamDetail(modifiedTeamDetail) != Guid.Empty)
                                {
                                    Intent intent = new Intent();
                                    intent.PutExtra("type", Resources.GetString(Resource.String.modifyBet));
                                    Activity.SetResult(Result.Ok, intent);
                                    Activity.Finish();
                                    //Toast.MakeText(Activity, "Bet updated", ToastLength.Long).Show();
                                    //BackHome();
                                }
                                else
                                {
                                    Toast.MakeText(Activity, "Error in bet updating", ToastLength.Long).Show();
                                }

                            }
                            else
                            {
                                Toast.MakeText(Activity, "Error in bet updating", ToastLength.Long).Show();
                            }
                            
                        }
                        else
                        {
                            lastBet.Error = "Required!";
                            betTeamName.Error = "Required!";

                        }
                        break;
                    case Resource.Id.menu_delete:
                        if (lastBet.Text != string.Empty)
                        {
                            BetDetails modifiedTeamDetail = new BetDetails()
                            {
                                Id = betDetails[betDetails.Count - 1].Id,
                                Quantity = Decimal.Parse(lastBet.Text),
                                Team_Id = activeBetTeams[betTeamsSpinner.SelectedItemPosition].Id,
                            };
                            if (AWSDataAccess.DeleteBetDetail(modifiedTeamDetail))
                            {
                                Intent intent = new Intent();
                                intent.PutExtra("type", Resources.GetString(Resource.String.deleteBet));
                                Activity.SetResult(Result.Ok, intent);
                                Activity.Finish();
                                //Toast.MakeText(Activity, "Bet deleted", ToastLength.Long).Show();
                                //BackHome();
                            }
                            else
                            {
                                Toast.MakeText(Activity, "Error in bet deleting", ToastLength.Long).Show();
                            }
                        }
                        else
                        {
                            lastBet.Error = "Required!";
                        }
                
                        break;
                    case Resource.Id.menu_deleteAll:
                        Team t = new Team()
                        {
                            Id = activeBetTeams[betTeamsSpinner.SelectedItemPosition].Id,
                        };

                        if (AWSDataAccess.DeleteAllBetTeam(t))
                        {
                            Intent intent = new Intent();
                            intent.PutExtra("type", Resources.GetString(Resource.String.deleteAllBetTeam));
                            Activity.SetResult(Result.Ok, intent);
                            Activity.Finish();

                            //Toast.MakeText(Activity, "All team bet deleted", ToastLength.Long).Show();
                            //BackHome();
                        }
                        else
                        {
                            Toast.MakeText(Activity, "Error in team bet deleting", ToastLength.Long).Show();
                        }
                        break;
                    default:
                        break;
                }
            };

            betTeamsSpinner.ItemSelected += BetTeamsSpinner_ItemSelected;




            return view;
        }


        private void BackHome()
        {
            //Android.Support.V4.App.FragmentTransaction ft = this.Activity.SupportFragmentManager.BeginTransaction();
            ////ft.Replace(Resource.Id.frameLayout1, new HomeFragment());
            //ft.Commit();
            TabsFragmentPagerAdapter adapter = Activity.FindViewById<ViewPager>
                             (Resource.Id.pager).Adapter as TabsFragmentPagerAdapter;            
            adapter.RemoveFragment();
        }

        private int running=1;
        private Guid selectedTeamId = Guid.Empty;
        private void BetTeamsSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            //this.progressDialog = ProgressDialog.Show(Activity, "", "Loading bet", true);
            //progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            //progressDialog.Show();

            #region With Progress Dialog
            if (activeBetTeams.Count > 0)
            {
                this.betDetails = AWSDataAccess.GetBetDetailsByTeamId(
                    activeBetTeams[e.Position].Id);
                if (betDetails.Count > 0)
                {

                    lastBet.Text = AWSDataAccess.DoFormat(betDetails
                        [betDetails.Count - 1].Quantity);
                    //Activity.RunOnUiThread(() =>
                    //{
                    //});
                    betTeamName.Text = activeBetTeams[e.Position].Name;
                    betType.Text = activeBetTeams[e.Position].Bet;
                    selectedTeamId = activeBetTeams[e.Position].Id;
                    //Activity.RunOnUiThread(() =>
                    //{
                        
                    //});
                }
            }
            #endregion
            #region old code
            //new Thread(new ThreadStart(delegate
            //{

            //    if (activeBetTeams.Count > 0)
            //    {
            //        List<BetDetails> betDetails = AWSDataAccess.GetBetDetailsByTeamId(
            //            activeBetTeams[e.Position].Id);
            //        if (betDetails.Count > 0)
            //        {
            //            Activity.RunOnUiThread(() =>
            //            {
            //                lastBet.Text = AWSDataAccess.DoFormat(betDetails
            //                    [betDetails.Count - 1].Quantity);
            //            });

            //            Activity.RunOnUiThread(() =>
            //            {
            //                betType.Text = activeBetTeams[e.Position].Bet;
            //            });
            //        }
            //    }

            //    Thread.Sleep(10);
            //    progressDialog.Dismiss();


            //})).Start();

            //if (Interlocked.CompareExchange(ref running,1,0)==0)
            //{

            //}
            #endregion


        }
        //if (threadCollection.Select(p => p.)))
        //{
        //    if (activeBetTeams.Count > 0)
        //    {
        //        List<BetDetails> betDetails = AWSDataAccess.GetBetDetailsByTeamId(
        //            activeBetTeams[e.Position].Id);
        //        if (betDetails.Count > 0)
        //        {

        //            lastBet.Text = AWSDataAccess.DoFormat(betDetails
        //                [betDetails.Count - 1].Quantity);
        //            //Activity.RunOnUiThread(() =>
        //            //{
        //            //});
        //            betType.Text = activeBetTeams[e.Position - 1].Bet;
        //            //Activity.RunOnUiThread(() => {

        //            //});
        //        }
        //    }
        //}
        //else
        //{

        //    //}



        //}
    }
}