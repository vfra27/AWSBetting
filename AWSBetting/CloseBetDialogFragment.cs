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
using Java.Lang;

namespace AWSBetting
{
    public class CloseBetDialogFragment : DialogFragment
    {
        List<Team> activeBetTeams = new List<Team>();

        public static CloseBetDialogFragment NewInstance(Bundle bundle)
        {
            CloseBetDialogFragment fragment = new CloseBetDialogFragment();
            fragment.Arguments = bundle;            
            return fragment;

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment           

            Dialog.SetTitle("You Win!!!");       

            View view= inflater.Inflate(Resource.Layout.CloseBetDialog, container, false);
            
            EditText win = view.FindViewById<EditText>(Resource.Id.win);
            win.FixDigits();
            Button saveWin = view.FindViewById<Button>(Resource.Id.saveWin);
            //Button closeWin = view.FindViewById<Button>(Resource.Id.cancelWin);

            Spinner activeBetTeamsSpinner = view.FindViewById<Spinner>(Resource.Id.activeTeamsSpinner);           
            activeBetTeams = AWSDataAccess.GetBetTeam(0);
            SpinnerTeamAdapter cba = new SpinnerTeamAdapter(Activity, activeBetTeams);
            activeBetTeamsSpinner.Adapter = cba;

            //activeBetTeamsSpinner.ItemSelected += ActiveBetTeamsSpinner_ItemSelected;

            saveWin.Click += delegate
             {
                 if (win.Text != string.Empty)
                 {
                     //salvataggio e chiusura dialog                     
                     SelectedTeam = activeBetTeams[activeBetTeamsSpinner.SelectedItemPosition];
                     SelectedTeam.Win = Convert.ToDecimal(win.Text);
                     SelectedTeam.Status = true;                     
                     if (AWSDataAccess.UpdateCloseWin(SelectedTeam) != Guid.Empty)
                     {
                         Toast.MakeText(Activity, "Win saved", ToastLength.Long).Show();
                         //FragmentTransaction ft = FragmentManager.BeginTransaction();
                         //ft.Replace(Resource.Id.frameLayout1, new HomeFragment());
                         //ft.Commit();
                         this.Dismiss();
                     }
                     else
                     {
                         Toast.MakeText(Activity, "Error in bet saving", ToastLength.Long).Show();
                     }
                 }
             };

            //closeWin.Click += delegate
            //{              
            //    this.Dismiss();
            //};

                        
            


            return view;
        }

        private Team SelectedTeam { get; set; }

        //private void ActiveBetTeamsSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        //{
        //    Spinner spin = sender as Spinner;
        //    SelectedTeamId = activeBetTeams[e.Position].Id; 
        //}
    }


    

}