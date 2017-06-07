using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using PickaPrato.Business;
using Android.Graphics;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;


namespace PickaPrato.Presentation {

    [Activity(Label = "ListaPratos")]
    
	public class ListaPratos : Activity {

        public static List<Prato> pratoList;
        public static string pesquisa;

		private RecyclerView mRecyclerView;
		private RecyclerView.LayoutManager mLayoutManager;
        private PratoAdapter mAdapter;
        private SupportToolbar toolbar;


		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.ListaPratos);

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);

			toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			TextView mTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
			mTitle.SetText("Resultados de " + pesquisa, TextView.BufferType.Normal);

            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            mAdapter = new PratoAdapter(pratoList, this);
            mRecyclerView.SetAdapter(mAdapter);
		}
	}
    
    public class PratoAdapter : RecyclerView.Adapter {

        public event EventHandler<int> ItemClick;
        public List<Prato> pratoList;
        private Context context;


        public PratoAdapter(List<Prato> pratoList, Context context) {
            this.pratoList = pratoList;
            this.context = context;
        }

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CardView, parent, false);

            PratoHolder ph = new PratoHolder(itemView, OnClick);
			return ph;
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			PratoHolder ph = holder as PratoHolder;

			byte[] a = Convert.FromBase64String(pratoList[position].Fotografia);
			Bitmap b = BitmapFactory.DecodeByteArray(a, 0, a.Length);
            ph.Image.SetImageBitmap(b);

            ph.Restaurante.Text = pratoList[position].Restaurante.Nome;
            ph.Prato.Text = pratoList[position].Designacao;
		}

		public override int ItemCount {
            get { return pratoList.Count; }
		}

		void OnClick(int position) {
            if (ItemClick != null) {
                ItemClick(this, position);
                DescricaoPrato.prato = pratoList[position];
                context.StartActivity(typeof(ListaPratos));
            }
		}
    }


    public class PratoHolder : RecyclerView.ViewHolder {

		public ImageView Image { get; set; }
        public TextView Restaurante { get; set; }
        public TextView Prato { get; set; }


        public PratoHolder(View itemView, Action<int> listener) : base(itemView) {
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            Restaurante = itemView.FindViewById<TextView>(Resource.Id.restaurante);
            Prato = itemView.FindViewById<TextView>(Resource.Id.prato);

            itemView.Click += (sender, e) => listener(base.AdapterPosition);
        }
    }
}
