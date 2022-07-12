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
    public class GameListAdapter : BaseAdapter<string>
    {

        private List<Game> objects;
        private Activity context;

        public GameListAdapter(Activity context, List<Game> objects) : base()
        {
            this.objects = objects;
            this.context = context;
        }

        public override string this[int position] => String.Format("Spiel {0} -- {1}", objects[position].Count, objects[position].LottoNumbers);

        public override int Count => objects.Count;

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