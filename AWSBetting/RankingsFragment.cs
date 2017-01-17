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
    public class RankingsFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.Rankings, container, false);


            WebView webView = view.FindViewById<WebView>(Resource.Id.rankingsView);
            Spinner leagueSpinner = view.FindViewById<Spinner>(Resource.Id.leagueSpinner);

            var leagues = new List<League>() { League.SerieA, League.Premier,
                League.Bundesliga, League.Liga, League.SerieB };

            leagueSpinner.Adapter = new ArrayAdapter<League>(Activity,
                Android.Resource.Layout.SimpleDropDownItem1Line, leagues);

            leagueSpinner.ItemSelected += delegate 
            {
                switch (leagues[leagueSpinner.SelectedItemPosition])
                {
                    case League.SerieA:
                        webView.LoadUrl(Ranking.SerieA);
                        break;
                    case League.Premier:
                        webView.LoadUrl(Ranking.Premier);
                        break;
                    case League.Liga:
                        webView.LoadUrl(Ranking.Liga);
                        break;
                    case League.Bundesliga:
                        webView.LoadUrl(Ranking.Bundesliga);
                        break;
                    case League.SerieB:
                        webView.LoadUrl(Ranking.SerieB);
                        break;
                    default:
                        break;
                }

            };

            //WebView webView = view.FindViewById <WebView>()            

            //webView.LoadUrl();    
            webView.SetWebViewClient(new StandingsWebViewClient());
            webView.Settings.JavaScriptEnabled = true;
            //webView.Settings.LoadWithOverviewMode = true;
            //webView.Settings.UseWideViewPort = true;
            //webView.Settings.SetSupportZoom(true);
            webView.Settings.BuiltInZoomControls = true;
            webView.Settings.DisplayZoomControls = false;
            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            webView.ScrollbarFadingEnabled = false;

            

            //webView.Settings.UserAgentString = "Mozilla/5.0(Linux; Android 4.4.4; One Build/ KTU84L.H4) AppleWebKit/537.36(KHTML, like Gecko) Chrome/36.0.1985.135 Mobile Safari/ 537.36";

            //webView.LoadUrl("http://www.corrieredellosport.it/live/classifica-serie-a.html");
            //webView.LoadUrl("http://www.diretta.it/calcio/inghilterra/premier-league/classifiche/");

            return view;
        }
        
    }


    public class StandingsWebViewClient : WebViewClient
    {
        private string url = string.Empty;

        //public StandingsWebViewClient(WebView view,string url):base()
        //{
        //    view.LoadUrl(url);
        //    view.Settings.JavaScriptEnabled = true;
        //    //webView.Settings.SetLayoutAlgorithm(LayoutAlgorithm.SingleColumn);
        //    view.Settings.LoadWithOverviewMode = true;
        //    view.Settings.UseWideViewPort = true;

        //}

        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            //view.Settings.JavaScriptEnabled = true;
            //webView.Settings.SetLayoutAlgorithm(LayoutAlgorithm.SingleColumn);

            return true;
        }


    }
}