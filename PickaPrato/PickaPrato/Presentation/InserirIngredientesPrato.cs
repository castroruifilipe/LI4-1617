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
        public static ListView listview;
        private SupportToolbar toolbar;

        
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EscolherIngredientes);

			toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			TextView mTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
			mTitle.SetText("Inserir ingredientes", TextView.BufferType.Normal);

            List<string> listastrings = Facade.GetIngredientes();
            listview = FindViewById<ListView>(Resource.Id.listview);
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
				foreach (Model2 m in adapter.items) {
                    if (m.isSelected()) {
                        Ingrediente i = new Ingrediente(m.getName(), m.isCostume());
                        listaIngr.Add(i);
                    }
                }
                Finish();
            };
		}

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e) {
            var listView = sender as ListView;
            Ingrediente i = listaIngr[e.Position];
            if (listaIngr.Contains(i)) {
                listaIngr.Remove(i);
            } else {
                listaIngr.Add(i);
            }
        }
    }

    public class Model2 {
    
        private string name;
        private bool selected;
        private bool costume;
        
        public Model2(string name) {
            this.name = name;
        }
        
        public string getName() {
            return name;
        }
        
        public bool isSelected() {
            return selected;
        }

        public bool isCostume() {
            return costume;
        }

        public void setCostume(bool costume) {
            this.costume = costume;
        }

        public void setSelected(bool selected) {
            this.selected = selected;
        }
    }

    public class IngredientesPratoAdapter : BaseAdapter<Model2> {
		
        public List<Model2> items;
		public Activity context;

        public IngredientesPratoAdapter(Activity context, List<string> items) : base() {
			this.context = context;
            this.items = new List<Model2>();
            foreach (string s in items) {
                Model2 m = new Model2(s);
                m.setSelected(false);
                m.setCostume(false);
                this.items.Add(m);
            }
		}

		public override long GetItemId(int position) {
			return position;
		}

		public override Model2 this[int position] {
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

            row.check.Click += (sender, e) => {
                int getposition = (int)row.check.Tag;
                items[getposition].setSelected(row.check.Checked);
                if (items[getposition].isSelected() == true) {
                    row.swit.Visibility = ViewStates.Visible;
                } else {
                    row.swit.Visibility = ViewStates.Invisible;
                }
            };
            row.swit.Click += (sender, e) => {
				int getposition = (int)row.swit.Tag;
                items[getposition].setCostume(row.swit.Checked);
			};

			row.text.Text = items[position].getName();
            row.check.Tag = position;
            row.check.Checked = items[position].isSelected();
            row.swit.Tag = position;
            row.swit.Checked = items[position].isCostume();
            if (items[position].isSelected() == true) {
                row.swit.Visibility = ViewStates.Visible;
            } else {
                row.swit.Visibility = ViewStates.Invisible;
            }
            
			return convertView;
        }

        public void AdicionarItem(string i) {
            Model2 m = new Model2(i);
            m.setSelected(true);
            m.setCostume(false);
            items.Add(m);
            NotifyDataSetChanged();
        }

        public class ViewHolder {
            public TextView text;
            public CheckBox check;
            public Switch swit;
        }
	}
}
