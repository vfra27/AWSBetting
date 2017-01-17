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
using Android.Support.V4.App;

namespace AWSBetting
{
    public class CalendarFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view =inflater.Inflate(Resource.Layout.Calendar, container, false);

            WebView webView = view.FindViewById<WebView>(Resource.Id.calendarsView);
            Spinner leagueSpinner = view.FindViewById<Spinner>(Resource.Id.calendarLeagueSpinner);

            var leagues = new List<League>() { League.SerieA, League.Premier,
                League.Bundesliga, League.Liga, League.SerieB };

            leagueSpinner.Adapter = new ArrayAdapter<League>(Activity,
                Android.Resource.Layout.SimpleDropDownItem1Line, leagues);

            leagueSpinner.ItemSelected += delegate
            {
                switch (leagues[leagueSpinner.SelectedItemPosition])
                {
                    case League.SerieA:
                        webView.LoadUrl(Calendar.SerieA);
                        break;
                    case League.Premier:
                        webView.LoadUrl(Calendar.Premier);
                        break;
                    case League.Liga:
                        webView.LoadUrl(Calendar.Liga);
                        break;
                    case League.Bundesliga:
                        webView.LoadUrl(Calendar.Bundesliga);
                        break;
                    case League.SerieB:
                        webView.LoadUrl(Calendar.SerieB);
                        break;
                    default:
                        break;
                }
            };

            webView.SetWebViewClient(new StandingsWebViewClient());
            webView.Settings.JavaScriptEnabled = true;
            //webView.Settings.LoadWithOverviewMode = true;
            //webView.Settings.UseWideViewPort = true;
            //webView.Settings.SetSupportZoom(true);
            webView.Settings.BuiltInZoomControls = true;
            webView.Settings.DisplayZoomControls = false;
            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            webView.ScrollbarFadingEnabled = false;

            return view;
        }
    }
}