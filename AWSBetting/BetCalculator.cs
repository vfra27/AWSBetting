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
using System.Globalization;
using Android.Text.Method;

namespace AWSBetting
{
    public static class BetCalculator
    {

        public static BettingPerding Calculate(string history, double quota
            , double nextBet, int offset)
        {
            //string historyTxt = history.Text;

            if (offset==0)
            {
                offset += 5;
            }

            BettingPerding result = new BettingPerding();

            string[] quote = history.Split(',');
            double sum = 0;
            foreach (var q in quote)
            {
                sum += Int32.Parse(q);
            }
            //int lastBet = Int32.Parse(quote[quote.Length - 1]);
            int lastBet = (int)(sum / quote.Length);

            while (lastBet % 5 != 0)
            {
                lastBet++;
            }            


            bool isPositive = false;
            //double nextBet = 0;
            double profit = 0;

            if (nextBet == 0)
            {
                do
                {

                    nextBet = lastBet + offset;                    
                    sum += nextBet;
                    //Double.Parse(quota.Text)
                    double temp = nextBet * quota;
                    profit = temp - sum;
                    isPositive = profit > 0;
                    if (!isPositive)
                    {
                        lastBet = (int)nextBet;
                        sum -= lastBet;
                    }


                } while (!isPositive);

                //nextBetTxt.Text = nextBet.ToString();
                result.NextBet = (int)nextBet;
            }
            else
            {
                sum += nextBet;
                double temp = nextBet * quota;
                profit = temp - sum;
            }


            //profit = Math.Round(profit);
            result.Profit = Math.Round(profit);
            return result;

        }


    }

    public class BettingPerding
    {
        public int NextBet { get; set; }
        public double Profit { get; set; }

    }

    public static class EditTextExtension
    {
        public static void FixDigits(this EditText text)
        {
            var decimalSign = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var digits = $"0123456789{decimalSign}-";
            text.KeyListener = DigitsKeyListener.GetInstance(digits);
        }
    }
}