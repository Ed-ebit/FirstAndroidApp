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

namespace FirstAndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView txtNumber;
        int number = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_main);

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
                    this, Resource.Array.gamesNumber_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

        }
        private void spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
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
