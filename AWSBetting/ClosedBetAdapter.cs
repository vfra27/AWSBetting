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
    public class ClosedBetAdapter : BaseAdapter<Team>
    {

        List<Team> closedBetTeams;
        Activity context;


        public ClosedBetAdapter(Activity context, List<Team> closedBetTeams)
        {
            this.context = context;
            this.closedBetTeams = closedBetTeams;

        }

        public override Team this[int position]
        {
            get
            {
                return closedBetTeams[position];
            }
        }

        public override int Count
        {
            get
            {
                return closedBetTeams.Count;
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
                    Resource.Layout.ClosedBetRow, parent, false);
            }

            Team team = this[position];
            view.FindViewById<TextView>(Resource.Id.closedBetTeam).Text = team.Name;
            view.FindViewById<TextView>(Resource.Id.closedBet).Text = team.Bet;
            view.FindViewById<TextView>(Resource.Id.win).Text= AWSDataAccess.DoFormat(team.Win) ;
            //Button nextBet = view.FindViewById<Button>(Resource.Id.nextBet);

            //nextBet.Click += delegate { nextBet.Text = string.Format("{0}!", team.Id); }; ;

            return view;
        }

        
    }
}