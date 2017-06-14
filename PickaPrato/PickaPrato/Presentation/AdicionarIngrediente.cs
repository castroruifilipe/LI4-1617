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
    
    [Activity(Label = "AdicionarIngrediente")]


    public class AdicionarIngrediente : DialogFragment {

        private Button cancelarButton;
        private Button inserirButton;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

            var view = inflater.Inflate(Resource.Layout.AdicionarIngrediente, container, true);

            cancelarButton = view.FindViewById<Button>(Resource.Id.cancelar);
            cancelarButton.Click += (sender, e) => {
                Dismiss();
            };

            EditText descricao = view.FindViewById<EditText>(Resource.Id.descricao);

            inserirButton = view.FindViewById<Button>(Resource.Id.adicionar);
            inserirButton.Click += (sender, e) => {
                Facade.AdicionarIngrediente(descricao.Text);
                IngredientesPratoAdapter adapter = (IngredientesPratoAdapter)InserirIngredientesPrato.listview.Adapter;
                adapter.AdicionarItem(descricao.Text);
                Dismiss();
            };

			return view;
        }
	}
}
