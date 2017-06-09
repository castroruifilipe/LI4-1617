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

using PickaPrato.Business;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace PickaPrato.Presentation {
    
    [Activity(Label = "InserirIngredientesPrato")]


    public class InserirIngredientesPrato : Activity {
        
        public static List<Ingrediente> listaIngr;
        private SupportToolbar toolbar;

        
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EscolherIngredientes);

			toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			TextView mTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
			mTitle.SetText("Inserir ingredientes", TextView.BufferType.Normal);

            List<string> listastrings = Facade.GetIngredientes();
            ListView listview = FindViewById<ListView>(Resource.Id.listview);
            listview.Adapter = new IngredientesPratoAdapter(this, listastrings);

            Button addingre = FindViewById<Button>(Resource.Id.addingrediente);
            addingre.Click += (sender, e) => {
                var dialog = new AdicionarIngrediente();
				dialog.Show(FragmentManager, "dialog");
            };

            Button guardarButtom = FindViewById<Button>(Resource.Id.guardar);
			listaIngr = new List<Ingrediente>();
			IngredientesPratoAdapter adapter = (IngredientesPratoAdapter)listview.Adapter;
            guardarButtom.Click += (sender, e) => {
				Dictionary<string, bool> ativos = adapter.Ativos;
				foreach (KeyValuePair<string, bool> entry in ativos) {
					Ingrediente ing = new Ingrediente(entry.Key, entry.Value);
                    listaIngr.Add(ing);
                }
                this.Finish();
            };
		}
    }

    public class IngredientesPratoAdapter : BaseAdapter<string> {
		
        public List<string> items;
        public Dictionary<string, bool> Ativos { get; }
		public Activity context;

        public IngredientesPratoAdapter(Activity context, List<string> items) : base() {
			this.context = context;
			this.items = items;
            this.Ativos = new Dictionary<string, bool>();
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
                convertView = context.LayoutInflater.Inflate(Resource.Layout.ListItemChekboxWithSwitch, null);
            }
			row.text = convertView.FindViewById<TextView>(Resource.Id.descricao);
			row.check = convertView.FindViewById<CheckBox>(Resource.Id.checkBox);
            row.swit = convertView.FindViewById<Switch>(Resource.Id.switch1);
            row.swit.Visibility = ViewStates.Invisible;
            row.text.Text = items[position];
			row.check.CheckedChange += (sender, e) => {
                if (row.check.Checked == true) {
                    Ativos.Add(row.text.Text, row.swit.Checked);
                    row.swit.Visibility = ViewStates.Visible;
                } else {
                    Ativos.Remove(row.text.Text);
                    row.swit.Visibility = ViewStates.Invisible;
                }
			};
            row.swit.CheckedChange += (sender, e) => {
                if (row.swit.Checked == true) {
                    Ativos[row.text.Text] = true;
                } else {
                    Ativos[row.text.Text] = false;
                }
            };
			return convertView;
        }

        public class ViewHolder {
            public TextView text;
            public CheckBox check;
            public Switch swit;
        }
	}
}
