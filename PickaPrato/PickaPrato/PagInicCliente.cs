using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Speech;

namespace PickaPrato {
    
    [Activity(Label = "PagInicCliente")]

    public class PagInicCliente : Activity {

        string[] historico;

        private AutoCompleteTextView textView;
        private ImageView recButton;
		private readonly int VOICE = 10;
		private string resultado;


        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PagInicCliente);

            historico = new string[] {
                "Francesinha", "Arroz de pato"
            };

			textView = FindViewById<AutoCompleteTextView>(Resource.Id.autocomplete_prato);
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.ListItem, historico);
			textView.Adapter = adapter;

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

		protected override void OnActivityResult(int requestCode, Result resultVal, Intent data) {
			if (requestCode == VOICE) {
				if (resultVal == Result.Ok) {
					var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
					if (matches.Count != 0) {
						string textInput = resultado + matches[0];

						if (textInput.Length > 500) {
							textInput = textInput.Substring(0, 500);
						}
						resultado = textInput;
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
			}
			base.OnActivityResult(requestCode, resultVal, data);
		}
    }
}
