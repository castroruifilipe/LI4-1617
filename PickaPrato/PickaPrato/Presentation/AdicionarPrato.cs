﻿﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using PickaPrato.Business;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace PickaPrato.Presentation {
    
    [Activity(Label = "AdicionarPrato")]


    public class AdicionarPrato : Activity {

        public static readonly int PickImageId = 1000;
        private SupportToolbar toolbar;
		private ImageView imageView;
		private CardView cardv;
        private Android.Net.Uri uri;
        private bool imagem = false;
        
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Preferencias);

			cardv = FindViewById<CardView>(Resource.Id.cardview);
			cardv.Visibility = ViewStates.Invisible;

			imageView = FindViewById<ImageView>(Resource.Id.imageview);
			Button button = FindViewById<Button>(Resource.Id.escolherimg);
			button.Click += ButtonOnClick;

			toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			TextView mTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
            mTitle.SetText("Adicionar prato", TextView.BufferType.Normal);

            EditText designacao = FindViewById<EditText>(Resource.Id.designacao);
            EditText tipo = FindViewById<EditText>(Resource.Id.tipo);
            EditText preco = FindViewById<EditText>(Resource.Id.preco);

            Button botaoAddIngrediente = FindViewById<Button>(Resource.Id.addingrediente);
            botaoAddIngrediente.Click += (sender, e) => {
				StartActivity(typeof(InserirIngredientesPrato));
            };

            Button botaoAdicionar = FindViewById<Button>(Resource.Id.adicionar);
            botaoAdicionar.Click += (sender, e) => {
				List<Ingrediente> ingredientes = InserirIngredientesPrato.listaIngr;
				if (imagem == true) {
					Bitmap mBitmap = MediaStore.Images.Media.GetBitmap(this.ContentResolver, uri);
					var stream = new MemoryStream();
					mBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
					var bytes = stream.ToArray();
					var foto = Convert.ToBase64String(bytes);
                    Facade.AdicionaPrato(designacao.Text, tipo.Text, Convert.ToDouble(preco.Text), foto, ingredientes);
                } else {
                    Facade.AdicionaPrato(designacao.Text, tipo.Text, Convert.ToDouble(preco.Text), "", ingredientes);
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
