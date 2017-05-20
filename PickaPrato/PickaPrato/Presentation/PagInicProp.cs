﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Speech;
using Android.Views.InputMethods;

namespace PickaPrato.Presentation {
    
    [Activity(Label = "PagInicProp")]

    public class PagInicProp : Activity {


        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PagInicProp);

        }
    }
}
