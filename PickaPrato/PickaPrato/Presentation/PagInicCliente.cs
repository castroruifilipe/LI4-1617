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
using Android.Content.PM;
using Java.Security;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Facebook.Login;

namespace PickaPrato.Presentation {
    
    [Activity(Label = "PagInicCliente")]

    public class PagInicCliente : Activity, IFacebookCallback {

        string[] historico;

        private AutoCompleteTextView textView;
        private ImageView recButton;
		private readonly int VOICE = 10;
		private string resultado;
        private ICallbackManager mCallBackManager;


        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
			FacebookSdk.SdkInitialize(this.ApplicationContext);

            SetContentView(Resource.Layout.PagInicCliente);

            historico = Facade.GetPesquisas();

            // Facebook
            LoginButton loginbutton = FindViewById<LoginButton>(Resource.Id.login_button);
            loginbutton.SetReadPermissions("user_friends");

            mCallBackManager = CallbackManagerFactory.Create();

            loginbutton.RegisterCallback(mCallBackManager, this);

            PackageInfo info = this.PackageManager.GetPackageInfo("com.uminhomieili4.pick_a_prato", PackageInfoFlags.Signatures);

            foreach (Android.Content.PM.Signature signature in info.Signatures) {
                MessageDigest md = MessageDigest.GetInstance("SHA");
                md.Update(signature.ToByteArray());

                string keyhash = Convert.ToBase64String(md.Digest());
                Console.WriteLine("KeyHash: " + keyhash);
            }
            ///////////////////////

			var imageuser = FindViewById<ImageView>(Resource.Id.foto);
			byte[] a = Convert.FromBase64String(Facade.atualUserC.Foto);
			Bitmap b = BitmapFactory.DecodeByteArray(a, 0, a.Length);
			imageuser.SetImageBitmap(b);

            var preferenciasButtom = FindViewById<Button>(Resource.Id.pref);
            preferenciasButtom.Click += (sender, e) => {
                StartActivity(typeof(EditarPreferencias));
            };

			var switchpref = FindViewById<Switch>(Resource.Id.switchpref);
            switchpref.Checked = true;

            Button guardadosbottom = FindViewById<Button>(Resource.Id.selecoes);
            guardadosbottom.Click += (sender, e) => {
                ListaPratos.pratoList = Facade.GetPratosGuardados();
                StartActivity(typeof(ListaPratos));
            };

            var gobottom = FindViewById<ImageView>(Resource.Id.go);

            textView = FindViewById<AutoCompleteTextView>(Resource.Id.autocomplete_prato);
            textView.Click += (sender, e) => {
                textView.Text = "";
            };
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.ListItem, historico);
            textView.Adapter = adapter;
            gobottom.Click += (sender, e) => {
                if (textView.Text.Length != 0) {
	                List<Prato> pratos;
	                if (switchpref.Checked == true) {
	                    pratos = Facade.PesquisaPrato(textView.Text, true);
	                } else {
	                    pratos = Facade.PesquisaPrato(textView.Text, false);
	                }
                    if (pratos.Count == 0) {
						new AlertDialog.Builder(this).
							SetPositiveButton("OK", (senderAlert, args) => { }).
							SetMessage("Não encontramos o que procura :(").
							SetTitle("Sem resultados").
							Show();
                    } else {
						ListaPratos.pratoList = pratos;
						ListaPratos.pesquisa = textView.Text;
		                StartActivity(typeof(ListaPratos));
                    }
                }
	        };

            recButton = FindViewById<ImageView>(Resource.Id.rec);
            recButton.Click += delegate {
				var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
				voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, "pr-BR");
				voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Fale agora");
				voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
				voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
				voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
				voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
				StartActivityForResult(voiceIntent, VOICE);
			};
        }

        public void OnCancel() {
            Console.WriteLine("Insucesso!!! \n\n\n\n");
        }

        public void OnError(FacebookException error) {
            Console.WriteLine("Erro!!! \n\n\n\n" + error.StackTrace);
        }

        public void OnSuccess(Java.Lang.Object result) {
            LoginResult loginresult = result as LoginResult;
            Facade.token = loginresult.AccessToken;
        }

		protected override void OnActivityResult(int requestCode, Result resultVal, Intent data) {
			if (requestCode == VOICE) {
				if (resultVal == Result.Ok) {
					var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
					if (matches.Count != 0) {
						resultado = matches[0];

						if (resultado.Length > 500) {
							resultado = resultado.Substring(0, 500);
						}

						this.textView.Text = resultado;
					} else {
						var alert = new AlertDialog.Builder(recButton.Context);
						alert.SetTitle("Não percebi");
						alert.SetPositiveButton("OK", (sender, e) => {
							textView.Text = "Não percebi";
							recButton.Enabled = false;
							return;
						});
						alert.Show();
					}
				}
				base.OnActivityResult(requestCode, resultVal, data);
            } else {
                base.OnActivityResult(requestCode, resultVal, data);
                mCallBackManager.OnActivityResult(requestCode, (int)resultVal, data);
            }
		}
    }

}
