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

			EditText nome = FindViewById<EditText>(Resource.Id.nome);
            EditText localidade = FindViewById<EditText>(Resource.Id.morada);
			EditText contacto = FindViewById<EditText>(Resource.Id.contacto);
			EditText email = FindViewById<EditText>(Resource.Id.email);

			nome.Text = Facade.atualUserP.Nome;
            localidade.Text = Facade.atualUserP.Localizacao;
            contacto.Text = Facade.atualUserP.Telefone;
            email.Text = Facade.atualUserP.Email;
        }
    }
}
