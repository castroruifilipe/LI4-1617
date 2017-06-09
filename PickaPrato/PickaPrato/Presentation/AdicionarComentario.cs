﻿﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using PickaPrato.Business;
using Xamarin.Facebook;
using Xamarin.Facebook.Share.Model;
using Xamarin.Facebook.Share.Widget;

namespace PickaPrato.Presentation {
    
    [Activity(Label = "AdicionarComentario")]


    public class AdicionarComentario : DialogFragment {

        private Button cancelarButton;
        private Button partilharButton;
        private ShareDialog sharedialog;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

            var view = inflater.Inflate(Resource.Layout.AdicionarComentario, container, true);

            sharedialog = new ShareDialog(this);

            cancelarButton = view.FindViewById<Button>(Resource.Id.cancelar);
            cancelarButton.Click += Button_Dismiss_Click;

            EditText comentario = view.FindViewById<EditText>(Resource.Id.comentario);
            RatingBar classificacao = view.FindViewById<RatingBar>(Resource.Id.classificacao);
            Switch partilhar = view.FindViewById<Switch>(Resource.Id.switchpartilhar);

            partilharButton = view.FindViewById<Button>(Resource.Id.partilhar);
            partilharButton.Click += (sender, e) => {
                Facade.AdicionarClassificacao(comentario.Text, Convert.ToInt32(classificacao.Rating),
                                              DescricaoPrato.pratosel.IdPrato);

                if (partilhar.Checked == true) {
					ShareLinkContent content = new ShareLinkContent.Builder()
					                                               .SetContentTitle(comentario.Text)
					                                               .SetContentDescription("Estive no restaurante " + DescricaoPrato.pratosel.Restaurante.Nome +
					                                                                      " e experimentei o prato " + DescricaoPrato.pratosel.Designacao +
					                                                                      ". O que tenho a dizer é: " + comentario.Text + 
					                                                                      ". Atribui " + Convert.ToInt32(classificacao.Rating) + " de classificacao.")
					                                               .SetContentUrl(Android.Net.Uri.Parse("https://www.facebook.com/Picka-Prato-1900012193554780/"))
					                                               .JavaCast<ShareLinkContent.Builder>()
					                                               .Build();
					sharedialog.Show(content);
                } else {
                    Dismiss();
                }
				
            };
			return view;
        }

		


		private void Button_Dismiss_Click(object sender, EventArgs e) {
			Dismiss();
		}

        public override void OnResume() {
            Dialog.Window.SetLayout(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);

            Dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));

            SetStyle(DialogFragmentStyle.NoTitle, Android.Resource.Style.Theme);
			Dismiss();
            base.OnResume();
        }

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);

			// Unwire event
            if (disposing) {
                cancelarButton.Click -= Button_Dismiss_Click;
            }
		}
	}
}
