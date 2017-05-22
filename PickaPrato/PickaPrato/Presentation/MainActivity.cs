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

            var registarClienteButtom = FindViewById<Button>(Resource.Id.registarcliente);
            registarClienteButtom.Click += (sender, e) => {
                StartActivity(typeof(RegistarCliente));
			};
            var registarRestauranteButtom = FindViewById<Button>(Resource.Id.registarest);
			registarRestauranteButtom.Click += (sender, e) => {
                StartActivity(typeof(RegistarRestaurante));
			};

			TextView username = FindViewById<TextView>(Resource.Id.username);
            TextView password = FindViewById<TextView>(Resource.Id.password);
            var iniciarSessaoButtom = FindViewById<Button>(Resource.Id.iniciarsessao);
			iniciarSessaoButtom.Click += (sender, e) => {
                if (username.Text.Length * password.Text.Length == 0) {
					new AlertDialog.Builder(this).
					    SetPositiveButton("OK", (senderAlert, args) => {}).
						SetMessage("Insira todos os dados!").
						SetTitle("Erro").
						Show();
                } else {
	                try {
	                    int r = Facade.IniciarSessao(username.Text, password.Text);
	                    if (r == 1) {
	                        StartActivity(typeof(PagInicCliente));
	                    } else if (r == 2) {
	                        StartActivity(typeof(PagInicProp));
	                    }
	                } catch (UtilizadorExistsException) {
	                    new AlertDialog.Builder(this).
                            SetPositiveButton("OK", (senderAlert, args) => { }).
	                        SetMessage("Dados incorretos!").
	                        SetTitle("Erro").
	                        Show();
	                }
                }
			};
        }
    }
}