﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace PickaPrato.Presentation {
    
    [Activity(Label = "EditarPreferencias")]


    public class EditarPreferencias : Activity {
        
        private List<string> listaIngr;
        private List<string> listaPref;
        private SupportToolbar toolbar;

        
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Preferencias);

			toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			TextView mTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
			mTitle.SetText("Editar preferências", TextView.BufferType.Normal);

            listaIngr = Facade.GetIngredientes();
			listaPref = Facade.GetPreferencias();
            ListView listview = FindViewById<ListView>(Resource.Id.listview);
            listview.Adapter = new HomeScreenAdapter(this, listaIngr, listaPref);
            listview.ItemClick += OnListItemClick;


            for (int i = 0; i < listaIngr.Count; i++) {
                if (listaPref.Contains(listaIngr[i])) {
                    listview.SetItemChecked(i, true);
                }
            }

            Button guardarButtom = FindViewById<Button>(Resource.Id.guardar);
            guardarButtom.Click += (sender, e) => {
                List<string> selecionados = new List<string>();
				long[] ids = listview.GetCheckedItemIds();
                for (int i = 0; i < listaIngr.Count; i++) {
                    long id = listview.Adapter.GetItemId(i);
                    if (ids.Contains(id)) {
                        selecionados.Add(listaIngr[i]);
                    }
                }
                Facade.EditarPreferencias(selecionados);
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
        List<string> ativos;
		Activity context;

        public HomeScreenAdapter(Activity context, List<string> items, List<string> ativos) : base() {
			this.context = context;
			this.items = items;
            this.ativos = ativos;
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
            ViewHolder row = new ViewHolder();
            if (convertView == null) {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.ListItemChekbox, null);
            }
			row.text = convertView.FindViewById<TextView>(Resource.Id.descricao);
			row.check = convertView.FindViewById<CheckBox>(Resource.Id.checkBox);
            row.text.Text = items[position];
            if (this.ativos.Contains(items[position])) {
				row.check.Checked = true;
            } else {
                row.check.Checked = false;
            }
			return convertView;
        }

        public class ViewHolder {
            public TextView text;
            public CheckBox check;
        }
	}
}
