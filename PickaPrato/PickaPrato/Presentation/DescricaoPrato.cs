using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Speech;
using Android.Views.InputMethods;
using Android.Views;
using Android.Graphics;
using System.Collections.Generic;
using PickaPrato.Business;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using FragmentManagerApp = Android.Support.V4.App.FragmentManager;
using Java.Lang;

namespace PickaPrato.Presentation {
    
    [Activity(Label = "DescricaoPrato")]

    public class DescricaoPrato : Activity {

        public static Prato pratosel;
        private SupportToolbar toolbar;


        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DescricaoPrato);

			toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			TextView mTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
            mTitle.SetText("Resultado " + pratosel.Designacao, TextView.BufferType.Normal);

            ImageView foto = FindViewById<ImageView>(Resource.Id.foto);
            RatingBar classificacao = FindViewById<RatingBar>(Resource.Id.classificacao);
            TextView prato = FindViewById<TextView>(Resource.Id.prato);
            TextView restaurante = FindViewById<TextView>(Resource.Id.restaurante);
            TextView morada = FindViewById<TextView>(Resource.Id.morada);
            TextView telefone = FindViewById<TextView>(Resource.Id.telefone);
            TextView preco = FindViewById<TextView>(Resource.Id.preco);

			byte[] a = Convert.FromBase64String(pratosel.Fotografia);
			Bitmap b = BitmapFactory.DecodeByteArray(a, 0, a.Length);
            foto.SetImageBitmap(b);

            classificacao.Rating = (float)pratosel.Classificacao;
            prato.Text = pratosel.Designacao;
            restaurante.Text = pratosel.Restaurante.Nome;
            morada.Text = pratosel.Restaurante.Localizacao;
            telefone.Text = "Contacto: " + pratosel.Restaurante.Telefone;
            preco.Text = "Preço: " + pratosel.Preco.ToString() + " €";

            List<Classificacao> classificacoes = pratosel.Classificacoes;
			
			ListView listview = FindViewById<ListView>(Resource.Id.listview);
			listview.Adapter = new ComentariosAdapter(this, classificacoes);

            ImageView addComment = FindViewById<ImageView>(Resource.Id.addBo);
            addComment.Click += (sender, e) => {
                Console.WriteLine("\n\n\nBOTAOCOMENTARIOS");
                var dialog = new AdicionarComentario();
                Console.WriteLine("\n\n\nAdicionaComentario");
                dialog.Show(FragmentManager, "dialog");
            };

            Button guardarbuttom = FindViewById<Button>(Resource.Id.guardar);
			guardarbuttom.Click += (sender, e) => {
                Facade.GuardarPrato(pratosel);
				new AlertDialog.Builder(this).
					SetPositiveButton("OK", (senderAlert, args) => { }).
					SetMessage("Prato guardado nas seleções!").
					SetTitle("Sucesso").
					Show();
			};

            Button mapabuttom = FindViewById<Button>(Resource.Id.mapa);
            mapabuttom.Click += (sender, e) => {
                var geoUri = Android.Net.Uri.Parse("geo:0,0?q=" + pratosel.Restaurante.Localizacao);
				var mapIntent = new Intent(Intent.ActionView, geoUri);
				StartActivity(mapIntent);
            };
        }
    }

    public class ComentariosAdapter : BaseAdapter<Classificacao> {
        
        public List<Classificacao> items;
        public Activity context;

        public ComentariosAdapter(Activity context, List<Classificacao> items) : base() {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position) {
            return position;
        }

        public override Classificacao this[int position] {
            get { return items[position]; }
        }
        
        public override int Count {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent) {
            var item = items[position];
            ViewHolder row = new ViewHolder();
            if (convertView == null) {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.ListItemComentario, null);
            }
            row.imagem = convertView.FindViewById<ImageView>(Resource.Id.foto);
            row.nome = convertView.FindViewById<TextView>(Resource.Id.nome);
            row.comentario = convertView.FindViewById<TextView>(Resource.Id.comentario);
            row.classificacao = convertView.FindViewById<RatingBar>(Resource.Id.classificacao);

            byte[] a = Convert.FromBase64String(items[position].Foto);
			Bitmap b = BitmapFactory.DecodeByteArray(a, 0, a.Length);
            row.imagem.SetImageBitmap(b);

			row.nome.Text = items[position].Utilizador;
            row.comentario.Text = items[position].Comentario;
            row.classificacao.Rating = items[position].Atribuicao;

            return convertView;
        }

        public class ViewHolder {
            public ImageView imagem;
			public TextView nome;
			public TextView comentario;
            public RatingBar classificacao;
        }
    }
}
