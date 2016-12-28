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

namespace AWSBetting
{
    public class TeamListAdapter : BaseAdapter<Team>
    {

        Activity context;
        List<Team> teams;


        public TeamListAdapter(Activity context, List<Team> teams)
            :base()
        {
            this.context = context;
            this.teams = teams;

        }

        public override Team this[int position]
        {
            get
            {
                return teams[position];
            }
        }

        public override int Count
        {
            get
            {
                return teams.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(
                    Resource.Layout.ActiveBetRow, parent, false);
            }

            Team team = this[position];
            view.FindViewById<TextView>(Resource.Id.team).Text = team.Name;
            view.FindViewById<TextView>(Resource.Id.activeBet).Text = team.Bet;
            TextView lastBet = view.FindViewById<TextView>(Resource.Id.lastBet);


            List<BetDetails>betDetails = AWSDataAccess.GetBetDetailsByTeamId(team.Id);
            if (betDetails.Count>0)
            {
                lastBet.Text = AWSDataAccess.DoFormat(betDetails[betDetails.Count - 1].Quantity);
                //
            }


            //Button nextBet = view.FindViewById<Button>(Resource.Id.nextBet);

            //nextBet.Click += delegate { nextBet.Text = string.Format("{0}!", team.Id); }; ;

            return view;
        }
        
    }
}