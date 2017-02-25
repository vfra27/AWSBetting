using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Amazon.DynamoDBv2.DataModel;

namespace AWSBetting
{    

    public class Recharge
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public BetProvider BetProvider { get; set; }
    }


    public class Team
    {

        
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public bool Status { get; set; }
        
        public string Bet { get; set; }

        public decimal Win { get; set; }

        public BetProvider BetProvider { get; set; }
        public decimal TotalCost { get; set; }

        public decimal BetProfit
        {
            get { return Win - TotalCost; }
            
        }
    }

    public enum BetProvider
    {
        PaddyPower=0,
        Bet365=1
    }
    //public class ClosedBetTeam
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get;  set; }
    //    public string Bet { get; set; }
        
    //}

    public enum League
    {
        SerieA, Premier, Liga, Bundesliga, SerieB
    }

    public sealed class Ranking
    {

        public static string SerieA { get { return "http://www.corrieredellosport.it/live/classifica-serie-a.html"; } }

        public static string Premier { get { return "http://www.corrieredellosport.it/live/classifica-PremierLeague.html"; } }
        public static string Liga { get { return "http://www.corrieredellosport.it/live/classifica-LaLiga.html"; } }

        public static string Bundesliga { get { return "http://www.corrieredellosport.it/live/classifica-Bundesliga.html"; } }

        //public string Liga { get { return "http://www.corrieredellosport.it/live/classifica-LaLiga.html"; } }
        //Premier= ,
        public static string SerieB { get { return "http://www.corrieredellosport.it/live/classifica-serie-b.html"; } }
    }       

    public sealed class Calendar
    {
        public static string SerieA { get { return "http://www.corrieredellosport.it/live/calendario-serie-a.html"; } }

        public static string Premier { get { return "http://www.gazzetta.it/speciali/risultati_classifiche/2017/calcio/premierleague/"; } }
        public static string Liga { get { return "http://www.gazzetta.it/speciali/risultati_classifiche/2017/calcio/liga/calendario.shtml"; } }
        ////https://www.google.it/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#safe=off&q=calendario+liga
        //http://sport.sky.it/statistiche/calcio/liga/calendario.shtml
        public static string Bundesliga { get { return "http://www.gazzetta.it/speciali/risultati_classifiche/2017/calcio/bundesliga/index.shtml"; } }
        
        public static string SerieB { get { return "http://www.gazzetta.it/speciali/risultati_classifiche/2017/calcio/serieb/index.shtml"; } }

    }


    public class BetDetails
    {
        public Guid Id { get; set; }

        public decimal Quantity { get; set; }
        public Guid Team_Id { get; set; }
        public DateTime Date { get; set; }

    }    

    
    public static class ApplicationState
    {
        private static bool isFirstRun = true;
        public static BetProvider ActiveProvider { get; set; }
        public static bool IsFirstRun {
            get { return isFirstRun; }
            set {

                isFirstRun = value;
            }

        }
    }
    
    //public enum Bet { one=1, x=0, two=2}
}