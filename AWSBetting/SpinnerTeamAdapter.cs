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
    public class SpinnerTeamAdapter:BaseAdapter<Team>,ISpinnerAdapter
    {
        List<Team> teams = new List<Team>();
        Activity context;

        public SpinnerTeamAdapter(Activity context, List<Team> teams)
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
            TextView text;
            if (convertView != null)
            {
                text = (TextView)convertView;
            }
            else
            {
                text = (TextView)context.LayoutInflater.Inflate(
                    Android.Resource.Layout.SimpleDropDownItem1Line,
                    parent, false);
            }

            Team item = teams[position];
            text.SetText(item.Name,TextView.BufferType.Normal);

            return text;

        }
    }
}