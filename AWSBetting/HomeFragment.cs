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
using Android.Webkit;
using static Android.Webkit.WebSettings;
using Android.Graphics;
using OxyPlot.Xamarin.Android;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Threading;

namespace AWSBetting
{
    public class HomeFragment : Fragment
    {


        

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var progressDialog = ProgressDialog.Show(Activity, "", "Creating plot", true);
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);

            progressDialog.Show();

            // Use this to return your custom view for this Fragment
            View view= inflater.Inflate(Resource.Layout.Home, container, false);
            //Button newBetBtn = view.FindViewById<Button>(Resource.Id.newBetBtn);
            //Button closeBetBtn = view.FindViewById<Button>(Resource.Id.closeBetBtn);
            //Button modifyBetBtn = view.FindViewById<Button>(Resource.Id.modifyBetBtn);
            
                                   

            //betProviders.Visibility = ViewStates.Visible;
            //var providers = new List<BetProvider>() { BetProvider.PaddyPower, BetProvider.Bet365 };
            //betProviders.Adapter = new ArrayAdapter<BetProvider>(Activity,
            //    Android.Resource.Layout.SimpleDropDownItem1Line, providers);
            string model = Build.Model;

            //betProviders.Visibility = ViewStates.Visible;
            //var providers = new List<BetProvider>() { BetProvider.PaddyPower, BetProvider.Bet365 };
            //betProviders.Adapter = new ArrayAdapter<BetProvider>(Activity,
            //    Android.Resource.Layout.SimpleDropDownItem1Line, providers);

            //betProviders.ItemSelected += delegate
            //{
            //    ApplicationState.ActiveProvider = providers[betProviders.SelectedItemPosition];

            //};
            if (model.StartsWith("frd", StringComparison.CurrentCultureIgnoreCase))
            {
                ApplicationState.ActiveProvider = BetProvider.Bet365;

            }



                #region button click
                //newBetBtn.Click += delegate
                //{
                //    var addBetFragment = new AddBetFragment() { Arguments = new Bundle() };                
                //    //calculateBetFragment.Arguments.PutString("PID", teams[e.Position - 1].Id.ToString());
                //    FragmentTransaction ft = this.Activity.FragmentManager.BeginTransaction();
                //    ft.Replace(Resource.Id.frameLayout1, addBetFragment, "AddBet");
                //    ft.Commit();

                //};


                //modifyBetBtn.Click += delegate
                //{
                //    var modBetFragment = new ModifyBetFragment() { Arguments = new Bundle() };
                //    //calculateBetFragment.Arguments.PutString("PID", teams[e.Position - 1].Id.ToString());
                //    FragmentTransaction ft = this.Activity.FragmentManager.BeginTransaction();
                //    ft.Replace(Resource.Id.frameLayout1, modBetFragment, "ModifyBet");
                //    ft.Commit();

                //};


                //closeBetBtn.Click += delegate
                // {
                //     FragmentTransaction ft = FragmentManager.BeginTransaction();
                //     //Remove fragment else it will crash as it is already added to backstack
                //     Fragment prev = FragmentManager.FindFragmentByTag("closeBetDialog");
                //     if (prev != null)
                //     {
                //         ft.Remove(prev);
                //     }

                //     ft.AddToBackStack(null);

                //     // Create and show the dialog.
                //     CloseBetDialogFragment newFragment = CloseBetDialogFragment.NewInstance(null);
                //     //newFragment.Arguments.PutString("TEAMID",)

                //     //Add fragment
                //     newFragment.Show(ft, "closeBetDialog");
                // };
                #endregion


                new Thread(new ThreadStart(delegate
            {
                #region Calculation
                List<Team> activeTeam = AWSDataAccess.GetBetTeam(0);
                List<Team> closedTeam = AWSDataAccess.GetBetTeam(1);
                decimal spentMoney = 100;
                foreach (var t in activeTeam)
                {
                    List<BetDetails> details =
                        AWSDataAccess.GetBetDetailsByTeamId(t.Id);

                    foreach (var d in details)
                    {
                        spentMoney += d.Quantity;
                    }

                }
                decimal winnedMoney = 0;
                foreach (var ti in closedTeam)
                {
                    List<BetDetails> details =
                        AWSDataAccess.GetBetDetailsByTeamId(ti.Id);
                    winnedMoney += ti.Win;
                    foreach (var de in details)
                    {
                        spentMoney += de.Quantity;
                    }

                }
                #endregion

                Activity.RunOnUiThread(() => { PlotView plotView = view.FindViewById<PlotView>(Resource.Id.plot_view); 
                plotView.Model = CreatePlotModel(winnedMoney, spentMoney); });
                Thread.Sleep(10);
                progressDialog.Dismiss(); 

            })).Start();

            

            //float percentComplete = (float)Math.Round((100 * winnedMoney) / spentMoney);


            //LinearLayout balanceLayout = (LinearLayout)view.FindViewById(Resource.Id.balanceLayout);
            //balanceLayout.AddView(new Rectangle(Activity, percentComplete, winnedMoney));

            


            var editToolbar = view.FindViewById<Toolbar>(Resource.Id.edit_toolbar);
            editToolbar.Title = "Editing";
            editToolbar.InflateMenu(Resource.Menu.edit_menus);


            if (model.StartsWith("frd", StringComparison.CurrentCultureIgnoreCase))
            {

                editToolbar.Menu.FindItem(Resource.Id.menu_preferences).SetVisible(true);
                //ApplicationState.ActiveProvider = BetProvider.Bet365;
                

            }
            else
            {
                editToolbar.Menu.FindItem(Resource.Id.menu_preferences).SetVisible(false);
            }



            editToolbar.MenuItemClick += (sender, e) =>
            {
                FragmentTransaction ft;
                switch (e.Item.ItemId)
                {
                    case Resource.Id.menu_create:
                        var addBetFragment = new AddBetFragment() { Arguments = new Bundle() };
                        //calculateBetFragment.Arguments.PutString("PID", teams[e.Position - 1].Id.ToString());
                        ft = this.Activity.FragmentManager.BeginTransaction();
                        ft.Replace(Resource.Id.frameLayout1, addBetFragment, "AddBet");
                        ft.Commit();
                        break;
                    case Resource.Id.menu_edit:
                        var modBetFragment = new ModifyBetFragment() { Arguments = new Bundle() };
                        //calculateBetFragment.Arguments.PutString("PID", teams[e.Position - 1].Id.ToString());
                        ft = this.Activity.FragmentManager.BeginTransaction();
                        ft.Replace(Resource.Id.frameLayout1, modBetFragment, "ModifyBet");
                        ft.Commit();
                        break;
                    case Resource.Id.menu_close:
                        ft = FragmentManager.BeginTransaction();
                        //Remove fragment else it will crash as it is already added to backstack
                        Fragment prev = FragmentManager.FindFragmentByTag("closeBetDialog");
                        if (prev != null)
                        {
                            ft.Remove(prev);
                        }

                        ft.AddToBackStack(null);

                        // Create and show the dialog.
                        CloseBetDialogFragment newFragment = CloseBetDialogFragment.NewInstance(null);
                        //newFragment.Arguments.PutString("TEAMID",)

                        //Add fragment
                        newFragment.Show(ft, "closeBetDialog");
                        break;

                    case Resource.Id.menu_preferences:
                        ft = FragmentManager.BeginTransaction();
                        Fragment p = FragmentManager.FindFragmentByTag("providerChoiceDialog");
                        if (p != null)
                        {
                            ft.Remove(p);
                        }
                        ft.AddToBackStack(null);
                        ProviderChoiceFragment pcFragment = ProviderChoiceFragment.NewInstance(null);
                        pcFragment.Show(ft, "providerChoiceDialog");
                        break;
                    default:
                        break;
                }
            };
            return view;
        }


        private PlotModel CreatePlotModel(decimal winnedMoney, decimal spentMoney)
        {
            var plotModel = new PlotModel
            {
                Title = "Bet Profit",
                LegendPlacement= LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation= LegendOrientation.Horizontal,
                LegendBorderThickness=0,
                Background= OxyColors.AntiqueWhite

            };


            var cost = new ColumnSeries
            {
                Title = "Cost",
                FillColor = OxyColors.Red,
                //IsStacked = true,
                StrokeColor = OxyColors.Black,
                StrokeThickness = 1
            };

            var win = new ColumnSeries
            {
                Title = "Win",
                FillColor = OxyColors.LightYellow,
                //IsStacked = true,
                StrokeColor = OxyColors.White,
                StrokeThickness = 1,                
            };


            var profit = new ColumnSeries
            {
                Title="Profit",
                FillColor=OxyColors.LightGreen,
                //IsStacked=true,
                StrokeThickness=1,
                StrokeColor=OxyColors.Black
            };

            var bottomAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom
            };

            var valueAxis = new LinearAxis()
            {
                Position=AxisPosition.Left,
                MinimumPadding=0,
                MaximumPadding=0.06,
               // AbsoluteMinimum=0
            };

            cost.Items.Add(new ColumnItem(Convert.ToDouble(spentMoney)));
            win.Items.Add(new ColumnItem(Convert.ToDouble(winnedMoney)));
            var profitValue = winnedMoney - spentMoney;
            profit.Items.Add(new ColumnItem(Convert.ToDouble(profitValue)));

            plotModel.Series.Add(cost);
            plotModel.Series.Add(win);
            plotModel.Series.Add(profit);

            plotModel.Axes.Add(bottomAxis);
            plotModel.Axes.Add(valueAxis);
            #region Linear Model
            //plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            //plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 10, Minimum = 0 });

            //var series1 = new LineSeries
            //{
            //    MarkerType = MarkerType.Circle,
            //    MarkerSize = 4,
            //    MarkerStroke = OxyColors.White
            //};

            //series1.Points.Add(new DataPoint(0.0, 6.0));
            //series1.Points.Add(new DataPoint(1.4, 2.1));
            //series1.Points.Add(new DataPoint(2.0, 4.2));
            //series1.Points.Add(new DataPoint(3.3, 2.3));
            //series1.Points.Add(new DataPoint(4.7, 7.4));
            //series1.Points.Add(new DataPoint(6.0, 6.2));
            //series1.Points.Add(new DataPoint(8.9, 8.9));
            //plotModel.Series.Add(series1);
            #endregion
            return plotModel;

        }

    }


    class Rectangle :View
    {
        //Paint paint = new Paint();
        float percentage = 0;
        decimal winnedMoney = 0;
        public Rectangle(Context context, float percentage, 
            decimal winnedMoney):base(context)
        {
            this.winnedMoney = winnedMoney;
            this.percentage = percentage;
        }


        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            Paint green = new Paint
            {
                AntiAlias = true,
                Color = Color.Rgb(0x99, 0xcc, 0),                
            };
            green.SetStyle(Paint.Style.Fill);
            

            Paint red = new Paint
            {
                AntiAlias = true,
                Color= Color.Rgb(0xff, 0x44, 0x44),
            };
            red.SetStyle(Paint.Style.FillAndStroke);

            //0.25f
            float middle = canvas.Width * percentage;
            canvas.DrawPaint(red);
            canvas.DrawText("Winned " + winnedMoney, 20, 20, green);
            //canvas.DrawRect(0, 0, middle,canvas.Height, green);
            
            //paint.Color = Color.Green;
            //Rect rect = new Rect(20, 56, 200, 112);
            ////
            //canvas.DrawRect(rect, paint);
        }
    }
    
}