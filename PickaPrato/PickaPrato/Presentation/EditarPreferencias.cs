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
                foreach (Model m in adapter.items) {
                    if (m.isSelected()) {
                        selecionados.Add(m.getName());
                    }
                }

                Facade.EditarPreferencias(selecionados);
                this.Finish();
            };
		}

		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e) {
			var listView = sender as ListView;
            string t = listaIngr[e.Position];
		}
    }

    public class Model {
    
	    private string name;
	    private bool selected;
	    
	    public Model(string name) {
	        this.name = name;
	    }
	    
	    public string getName() {
	        return name;
	    }
	    
	    public bool isSelected() {
	        return selected;
	    }
	    
	    public void setSelected(bool selected) {
	        this.selected = selected;
	    }
	} 

	public class HomeScreenAdapter : BaseAdapter<Model> {
		
        public List<Model> items;
		public Activity context;

        public HomeScreenAdapter(Activity context, List<string> items, List<string> PreAtivos) : base() {
			this.context = context;
            this.items = new List<Model>();
            foreach (string s in items) {
                Model m = new Model(s);
                if (PreAtivos.Contains(s)) {
                    m.setSelected(true);
                }
                this.items.Add(m);
            }
		}

		public override long GetItemId(int position) {
			return position;
		}

		public override Model this[int position] {
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

            row.check.Click += (sender, e) => {
                int getposition = (int)row.check.Tag;
                items[getposition].setSelected(row.check.Checked);
            };

			row.check.Tag = position;
            row.check.Checked = items[position].isSelected();
            row.text.Text = items[position].getName();
			return convertView;
        }

        public class ViewHolder {
            public TextView text;
            public CheckBox check;
        }
	}
}
