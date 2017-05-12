using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PickaPrato {
    
    [Activity(Label = "EditarPreferencias")]

    public class EditarPreferencias : Activity {

        private List<string> listaIngr;
        
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Preferencias);

            listaIngr = new List<string>();
			listaIngr.Add("Tomate2");
			listaIngr.Add("Tomate");
			listaIngr.Add("Tomate2");
			listaIngr.Add("Tomate");
			listaIngr.Add("Tomate2");
			listaIngr.Add("Tomate");
			listaIngr.Add("Tomate2");
			listaIngr.Add("Tomate");
			listaIngr.Add("Tomate2");
			listaIngr.Add("Tomate");

            ListView listview = FindViewById<ListView>(Resource.Id.listview);
            listview.Adapter = new HomeScreenAdapter(this, listaIngr);
            listview.ItemClick += OnListItemClick;


            Button guardarButtom = FindViewById<Button>(Resource.Id.guardar);
			guardarButtom.Click += (sender, e) => {
                //
            };
		}

		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e) {
			var listView = sender as ListView;
            var t = listaIngr[e.Position];
			Toast.MakeText(this, t, ToastLength.Short).Show();
		}
    }

	public class HomeScreenAdapter : BaseAdapter<string> {
		
        List<string> items;
		Activity context;

		public HomeScreenAdapter(Activity context, List<string> items) : base() {
			this.context = context;
			this.items = items;
		}

		public override long GetItemId(int position) {
			return position;
		}

		public override string this[int position] {
			get { return items[position]; }
		}
		
        public override int Count {
			get { return items.Count; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent) {
			var item = items[position];
			View view = convertView;
            if (view == null) {
                view = context.LayoutInflater.Inflate(Resource.Layout.ListItemChekbox, null);
            }
            view.FindViewById<TextView>(Resource.Id.descricao).Text = item;
			return view;
		}
	}
}
