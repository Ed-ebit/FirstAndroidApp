using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using System.Collections.Generic;
using Android.Content;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;

namespace FirstAndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        int gamesNumber = 0;
        public ListView lstNumber;
        public List<Game> games;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;

            //txtNumber = FindViewById<TextView>(Resource.Id.txtNumber);
            //FindViewById<Button>(Resource.Id.btnIncrement).Click += (o, e) =>
            //txtNumber.Text = (++number).ToString();
            //FindViewById<Button>(Resource.Id.btnDecrement).Click += (o, e) =>
            //txtNumber.Text = (--number).ToString();
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner1_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.gamesNumber_array, Android.Resource.Layout.SimpleSelectableListItem );

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
            spinner.Adapter = adapter;
            //gamesNumber = spinner.SelectedItemPosition;

            FindViewById<Button>(Resource.Id.btnStart).Click += (o, e) => GenerateNumbers();
               
            FindViewById<Button>(Resource.Id.btnExit).Click += (o, e) => OnBtnExitClicked();
            //lstNumber = (ListView)Game.InitializeGames(1).Count.ToString();
        }

        private void spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            gamesNumber = int.Parse((string)spinner.GetItemAtPosition(e.Position));
            string toast = string.Format("{0} Spiel(e) gewählt", gamesNumber);
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
        private void OnBtnStartClicked()
        {
            lstNumber = FindViewById<ListView>(Resource.Id.lstNumber);
            games = Game.InitializeGames(gamesNumber);
            var listAdapter = new GameListAdapter(this, games);
            lstNumber.Adapter = listAdapter;
        }
        private void OnBtnExitClicked()
        {
            gamesNumber = 0;
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void GenerateNumbers()
        {
            if (games == null)
            {
                OnBtnStartClicked();
                return;
            }
            ShowRefreshAlert();
        }

        private void ShowRefreshAlert()
        {
            AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
            alertDiag.SetTitle("Neue Zahlen generieren");
            alertDiag.SetMessage("Alte Zahlen verwerfen und Neue generieren?");
            alertDiag.SetPositiveButton("Ja", (senderAlert, args) => {
                Toast.MakeText(this, "bestätigt", ToastLength.Short).Show();

                OnBtnStartClicked();
            });
            alertDiag.SetNegativeButton("Nein", (senderAlert, args) => {
                alertDiag.Dispose();
            });
            Dialog diag = alertDiag.Create();
            diag.Show();
        }


        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(Resource.Menu.menu_main, menu);
        //    return true;
        //}

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    int id = item.ItemId;
        //    if (id == Resource.Id.action_settings)
        //    {
        //        return true;
        //    }

        //    return base.OnOptionsItemSelected(item);
        //}

        //private void FabOnClick(object sender, EventArgs eventArgs)
        //{
        //    View view = (View) sender;
        //    Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
        //        .SetAction("Action", (View.IOnClickListener)null).Show();
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
