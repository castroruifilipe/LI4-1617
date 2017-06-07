﻿using System;
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


namespace PickaPrato.Presentation {
    
    [Activity(Label = "Prato")]

    public class DescricaoPrato : Activity {

        public static Prato prato;
        private SupportToolbar toolbar;


        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DescricaoPrato);

			toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			TextView mTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
            mTitle.SetText("Resultado" + prato.Designacao, TextView.BufferType.Normal);

            ImageView foto = FindViewById<ImageView>(Resource.Id.foto);
            RatingBar classificacao = FindViewById<RatingBar>(Resource.Id.classificacao);

			byte[] a = Convert.FromBase64String(prato.Fotografia);
			Bitmap b = BitmapFactory.DecodeByteArray(a, 0, a.Length);
            foto.SetImageBitmap(b);
        }
    }
}
