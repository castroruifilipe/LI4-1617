using System;
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
using Java.Lang;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace PickaPrato.Presentation {
    
    [Activity(Label = "EditarPreferencias")]


    public class EditarPreferencias : Activity {
        
        private List<string> listaIngr;
        private List<string> listaPref;
        private List<string> selecionados;
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

			selecionados = new List<string>();
            Button guardarButtom = FindViewById<Button>(Resource.Id.guardar);
            HomeScreenAdapter adapter = (HomeScreenAdapter)listview.Adapter;
            guardarButtom.Click += (sender, e) => {
                /*for (int i = 0; i < listaIngr.Count; i++) {
                    var t = adapter.GetView(i, null, listview);
                    string text = t.FindViewById<TextView>(Resource.Id.descricao).Text;
                    bool check = t.FindViewById<CheckBox>(Resource.Id.checkBox).Checked;
                    if (check == true) {
						Console.WriteLine("\n\n\n" + text);
                        selecionados.Add(text);
                    }
                }*/

                Facade.EditarPreferencias(adapter.Ativos);
                this.Finish();
            };
		}

		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e) {
			var listView = sender as ListView;
            string t = listaIngr[e.Position];
		}
    }

	public class HomeScreenAdapter : BaseAdapter<string> {
		
        public List<string> items;
        public List<string> Ativos { get; }
		public Activity context;

        public HomeScreenAdapter(Activity context, List<string> items, List<string> PreAtivos) : base() {
			this.context = context;
			this.items = items;
            this.Ativos = new List<string>();
            /*foreach (string s in PreAtivos) {
                this.Ativos.Add(s);
            }*/
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
			row.text.Text = items[position];
            row.check = convertView.FindViewById<CheckBox>(Resource.Id.checkBox);
            row.check.Click += (sender, e) => {
                if (row.check.Checked == true && Ativos.Contains(row.text.Text) == false) {
                    this.Ativos.Add(row.text.Text);
                } else if (row.check.Checked == false && Ativos.Contains(row.text.Text) == true) {
                    this.Ativos.Remove(row.text.Text);
                }
            };

            /*if (Ativos.Contains(items[position])) {
                row.check.Checked = true;
            } else {
                row.check.Checked = false;
            }*/

			return convertView;
        }

        public class ViewHolder {
            public TextView text;
            public CheckBox check;
        }
	}
}
