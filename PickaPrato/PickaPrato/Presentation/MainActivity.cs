﻿using Android.App;
using Android.Widget;
using Android.OS;

using PickaPrato.Presentation;
using PickaPrato.Data;
using PickaPrato.Exceptions;

namespace PickaPrato.Business {
    
    [Activity(Label = "Pick'a Prato", MainLauncher = true, Icon = "@mipmap/icon")]

    public class MainActivity : Activity {


        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var registarButtom = FindViewById<Button>(Resource.Id.registarcliente);
            registarButtom.Click += (sender, e) => {
                StartActivity(typeof(RegistarCliente));
			};

			TextView username = FindViewById<TextView>(Resource.Id.username);
            TextView password = FindViewById<TextView>(Resource.Id.password);
            var iniciarSessaoButtom = FindViewById<Button>(Resource.Id.iniciarsessao);
			iniciarSessaoButtom.Click += (sender, e) => {
                try {
                    int r = Facade.IniciarSessao(username.Text, password.Text);
                    if (r == 1) {
                        StartActivity(typeof(PagInicCliente));
                    } else if (r == 2) {
                        StartActivity(typeof(PagInicCliente));
                    }
                } catch (UtilizadorExistsException) {
                    new AlertDialog.Builder(this).
                        SetMessage("Dados incorretos!").
                        SetTitle("Erro").
                        Show();
                }
			};
        }
    }
}