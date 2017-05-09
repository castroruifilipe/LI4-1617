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
		private Button recButton;
		private bool isRecording;
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

            recButton = FindViewById<Button>(Resource.Id.rec);

			isRecording = false;

			string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
			if (rec != "android.hardware.microphone") {
				var alert = new AlertDialog.Builder(recButton.Context);
				alert.SetTitle("Não é possível fazer o reconhecimento de voz no seu dispositivo.");
				alert.SetPositiveButton("OK", (sender, e) => {
					textView.Text = "Não foi encontrado nenhum microfone.";
					recButton.Enabled = false;
					return;
				});
				alert.Show();
            } else {
                recButton.Click += delegate {
					recButton.Text = "End Recording";
					isRecording = !isRecording;
					if (isRecording == true) {
						var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
						voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, "pr-BR");
						voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Fale agora");
						voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
						voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
						voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
						voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
						StartActivityForResult(voiceIntent, VOICE);
					}
				};
			}
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
					recButton.Text = "Falar";
				}
			}
			base.OnActivityResult(requestCode, resultVal, data);
		}
    }
}
