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

namespace FirstAndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        int number = 0;
        public ListView lstNumber;
        public List<Game> games;
        public List<string> showGames;
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



            //games = new List<Game>();
            lstNumber = FindViewById<ListView>(Resource.Id.lstNumber);
            games = Game.InitializeGames(5);

            var listAdapter = new GameListAdapter(this, games);
            lstNumber.Adapter = listAdapter;


            //FindViewById<Button>(Resource.Id.btnStart).Click += (o, e) =>
            //lstNumber = (ListView)Game.InitializeGames(1).Count.ToString();
        }
        private void spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("{0} Spiel(e) gewählt", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void OnBtnStartClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
        ListView lstNumber;
        List<Game> games;
            //lstNumber = (ListView)Game.InitializeGames(1).Count.ToString();
            games = Game.InitializeGames(1);

            //lstNumber = games;
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
    public class Game
    {

        public int Count { get; }
        public string LottoNumbers { get; private set; }

        private readonly byte[] GuteZahlen = { 45, 13, 28, 21, 20, 8, 12, 15, 46, 14, 30, 34, 37, 23, 40, 29, 35, 10, 17, 39, 44, 5, 24, 48 };
        private readonly byte[] NormaleZahlen = { 1, 2, 3, 4, 6, 7, 9, 11, 15, 16, 17, 18, 19, 22, 25, 26, 27, 31, 32, 33, 35, 36, 38, 41, 42, 43, 47, 49 };

        public Game(int count)
        {
            Count = count;
            LottoNumbers = CreateLottoNumbers(GuteZahlen, NormaleZahlen);
        }

        private string CreateLottoNumbers(byte[] guteZahlen, byte[] normaleZahlen)
        {
            const byte goodNumberCount = 4;
            const byte normalNumberCount = 2;
            HashSet<byte> goodNumbers = NumberSelector(guteZahlen, goodNumberCount);
            HashSet<byte> normalNumbers = NumberSelector(normaleZahlen, normalNumberCount);
            HashSet<byte> combinedNumbers = goodNumbers;
            combinedNumbers.UnionWith(normalNumbers);

            return string.Join(" | ", combinedNumbers);
        }
        private HashSet<byte> NumberSelector(byte[] array, byte desiredNumbers)
        {
            HashSet<byte> selectedNumbers = new HashSet<byte>();
            bool numberAdded = false;
            Random randomNumber = new Random();

            for (byte i = 0; i < desiredNumbers; i++)
            {
                do
                {
                    int randomIndex = randomNumber.Next(0, (array.Length - 1));
                    numberAdded = selectedNumbers.Add(array[randomIndex]);
                }
                while (numberAdded == false);
            }
            return selectedNumbers;
        }


        public static List<Game> InitializeGames(int gamesNumber)
        {
            List<Game> games = new List<Game>();
            for (int i = 1; i <= gamesNumber; i++)
            {
                games.Add(new Game(i));
            }
            return games;
        }
    }

    public class GameListAdapter : BaseAdapter<string>
    {

        private List<Game> objects;
        private Activity context;

        public GameListAdapter(Activity context, List<Game> objects) : base()
        {
            this.objects=objects;
            this.context = context;
            // getItem o.ä überschreiben und obige Probblematik darin lösen
        }

        public override string this[int position] => objects[position].Count + " -- " + objects[position].LottoNumbers;

        public override int Count => objects.Count; // ???!!!

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = objects[position].Count + " -- " + objects[position].LottoNumbers;
            return view;

        }
    }
}
