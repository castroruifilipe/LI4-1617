using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Graphics;
using Android.Util;
using Android.Provider;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;


namespace PickaPrato.Presentation {

    [Activity(Label = "RegistarRestaurante")]

    public class RegistarRestaurante : Activity {

		public static readonly int PickImageId = 1000;
		private List<ImageView> imagens;
        private int nImagens;
        private SupportToolbar toolbar;
        private Android.Net.Uri uri;
        
        
        protected override void OnCreate(Bundle savedInstanceState) {
            
            base.OnCreate(savedInstanceState);

            nImagens = 0;

            SetContentView(Resource.Layout.RegistarRestaurante);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			TextView mTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
            mTitle.SetText("Registar restaurante",TextView.BufferType.Normal);

            imagens = new List<ImageView>();
            imagens.Add(FindViewById<ImageView>(Resource.Id.imagem1));
			imagens.Add(FindViewById<ImageView>(Resource.Id.imagem2));
			imagens.Add(FindViewById<ImageView>(Resource.Id.imagem3));
			imagens.Add(FindViewById<ImageView>(Resource.Id.imagem4));
			imagens.Add(FindViewById<ImageView>(Resource.Id.imagem5));
			imagens.Add(FindViewById<ImageView>(Resource.Id.imagem6));

            Button button = FindViewById<Button>(Resource.Id.escolherimg);
			button.Click += ButtonOnClick;
           
            EditText user = FindViewById<EditText>(Resource.Id.username_edittext);
            EditText pass = FindViewById<EditText>(Resource.Id.password_edittext);
            EditText nome = FindViewById<EditText>(Resource.Id.nome_edittext);
            EditText morada = FindViewById<EditText>(Resource.Id.username_edittext);
            EditText telefone = FindViewById<EditText>(Resource.Id.telefone_edittext);
            EditText email = FindViewById<EditText>(Resource.Id.email_edittext);

            List<string> fotos = new List<string>();
            Button botaoRegistar = FindViewById<Button>(Resource.Id.bregistar);
            botaoRegistar.Click += (sender, e) => {
                for (int i = 0; i < nImagens; i++) {
					Bitmap mBitmap = MediaStore.Images.Media.GetBitmap(this.ContentResolver, uri);
					var stream = new MemoryStream();
					mBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
					var bytes = stream.ToArray();
					var foto = Convert.ToBase64String(bytes);
                    fotos.Add(foto);
                }
                Facade.RegistarRestaurante(user.Text, pass.Text, nome.Text, morada.Text, telefone.Text, email.Text, fotos);
            };
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs) {
            if (nImagens == 6) {
				new AlertDialog.Builder(this).
					SetPositiveButton("OK", (senderAlert, args) => { }).
					SetMessage("Já inseriu todas as imagens!").
					SetTitle("Erro").
					Show();
			}
			Intent = new Intent();
			Intent.SetType("image/*");
			Intent.SetAction(Intent.ActionGetContent);
			StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
			if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null)) {
				uri = data.Data;
                imagens[nImagens].SetImageURI(uri);
                imagens[nImagens].Visibility = ViewStates.Visible;
                nImagens++;
			}
		}
    }
}
