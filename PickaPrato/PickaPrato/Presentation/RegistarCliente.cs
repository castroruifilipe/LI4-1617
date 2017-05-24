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

using PickaPrato.Data;
using PickaPrato.Exceptions;
using System.Threading.Tasks;

namespace PickaPrato.Presentation {

    [Activity(Label = "RegistarCliente")]

    public class RegistarCliente : Activity {

		public static readonly int PickImageId = 1000;
		private ImageView imageView;
        private CardView cardv;
        private SupportToolbar toolbar;
        private Android.Net.Uri uri;
        private bool imagem = false;

        
        protected override void OnCreate(Bundle savedInstanceState) {
            
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RegistarCliente);

            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			TextView mTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
            mTitle.SetText("Registar cliente",TextView.BufferType.Normal);

            cardv = FindViewById<CardView>(Resource.Id.cardview);
            cardv.Visibility = ViewStates.Invisible;

            imageView = FindViewById<ImageView>(Resource.Id.imageview);
            Button button = FindViewById<Button>(Resource.Id.escolherimg);
			button.Click += ButtonOnClick;
           
            EditText user = FindViewById<EditText>(Resource.Id.username_edittext);
            EditText pass = FindViewById<EditText>(Resource.Id.password_edittext);
            Button botaoRegistar = FindViewById<Button>(Resource.Id.bregistar);
            Boolean r;
            botaoRegistar.Click += (sender, e) => {
                if (imagem == true) {
                    Bitmap mBitmap = MediaStore.Images.Media.GetBitmap(this.ContentResolver, uri);
                    var stream = new MemoryStream();
                    mBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                    var bytes = stream.ToArray();
                    var foto = Convert.ToBase64String(bytes);
                    r = Facade.RegistarCliente(user.Text, pass.Text, foto);
                } else {
                    r = Facade.RegistarCliente(user.Text, pass.Text, null);
                }
                if (r == false) {
					new AlertDialog.Builder(this).
						SetPositiveButton("OK", (senderAlert, args) => { }).
						SetMessage("Utilizador já registado!").
						SetTitle("Erro").
						Show();
                } else {
					new AlertDialog.Builder(this).
						SetPositiveButton("OK", (senderAlert, args) => { this.Finish(); }).
						SetMessage("Registado com sucesso!").
						SetTitle("Sucesso").
						Show();
                }
            };

        }



        private void ButtonOnClick(object sender, EventArgs eventArgs) {
			Intent = new Intent();
			Intent.SetType("image/*");
			Intent.SetAction(Intent.ActionGetContent);
			StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
            imagem = true;
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
			if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null)) {
				uri = data.Data;
				imageView.SetImageURI(uri);
                cardv.Visibility = ViewStates.Visible;
			}
		}
    }
}
