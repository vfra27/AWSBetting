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
    public class OddsFragment : Android.Support.V4.App.Fragment
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
            View view = inflater.Inflate(Resource.Layout.Odds, container, false);
            
            WebView webView = view.FindViewById<WebView>(Resource.Id.oddsView);
            

            webView.SetWebViewClient(new StandingsWebViewClient());
            webView.Settings.JavaScriptEnabled = true;
            //webView.Settings.LoadWithOverviewMode = true;
            //webView.Settings.UseWideViewPort = true;
            //webView.Settings.SetSupportZoom(true);
            webView.Settings.BuiltInZoomControls = true;
            webView.Settings.DisplayZoomControls = false;
            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            webView.ScrollbarFadingEnabled = false;
            if (ApplicationState.ActiveProvider == BetProvider.Bet365)
            {
                webView.LoadUrl(Resources.GetString(Resource.String.todayBet365OddsUrl));
            }
            else if (ApplicationState.ActiveProvider == BetProvider.PaddyPower)
            {
                webView.LoadUrl(Resources.GetString(Resource.String.todayPaddyPowerOddsUrl));
            }

            return view;
        }
    }
}