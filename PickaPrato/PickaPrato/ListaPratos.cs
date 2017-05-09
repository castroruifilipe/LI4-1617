using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;

namespace PickaPrato {

    [Activity(Label = "ListaPratos")]
    
	public class ListaPratos : Activity {
        
		RecyclerView mRecyclerView;
		RecyclerView.LayoutManager mLayoutManager;
        PratoAdapter mAdapter;
        List<Prato> pratoList;


		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);

            pratoList = new List<Prato>();
            Prato teste = new Prato(1, "Francesinha");
            pratoList.Add(teste);
			pratoList.Add(teste);
			pratoList.Add(teste);
			pratoList.Add(teste);
			pratoList.Add(teste);
			pratoList.Add(teste);
			pratoList.Add(teste);


			SetContentView(Resource.Layout.ListaPratos);

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);

            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            mAdapter = new PratoAdapter(this.pratoList);
            mAdapter.ItemClick += OnItemClick;
            mRecyclerView.SetAdapter(mAdapter);
		}

		void OnItemClick(object sender, int position) {
			int pratoNum = position + 1;
            Toast.MakeText(this, "This is prato number " + pratoNum, ToastLength.Short).Show();
		}
	}
    
    public class PratoAdapter : RecyclerView.Adapter {

        public event EventHandler<int> ItemClick;
        public List<Prato> pratoList;


        public PratoAdapter(List<Prato> pratoList) {
            this.pratoList = pratoList;
        }

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CardView, parent, false);

            PratoHolder ph = new PratoHolder(itemView, OnClick);
			return ph;
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			PratoHolder ph = holder as PratoHolder;

            ph.Image.SetImageResource(pratoList[position].photo);
            ph.Caption.Text = pratoList[position].designacao;
		}

		public override int ItemCount {
            get { return pratoList.Count; }
		}

		void OnClick(int position) {
            if (ItemClick != null) {
                ItemClick(this, position);
            }
		}
    }


    public class PratoHolder : RecyclerView.ViewHolder {

		public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }


        public PratoHolder(View itemView, Action<int> listener) : base(itemView) {
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);

            itemView.Click += (sender, e) => listener(base.AdapterPosition);
        }
    }
}
