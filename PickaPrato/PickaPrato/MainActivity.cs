using Android.App;
using Android.Widget;
using Android.OS;

namespace PickaPrato {
    
    [Activity(Label = "Pick'a Prato", MainLauncher = true, Icon = "@mipmap/icon")]

    public class MainActivity : Activity {
        
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var registarButtom = FindViewById<Button>(Resource.Id.registarcliente);
            registarButtom.Click += (sender, e) => {
                StartActivity(typeof(RegistarCliente));
			};

            var iniciarSessaoButtom = FindViewById<Button>(Resource.Id.iniciarsessao);
			iniciarSessaoButtom.Click += (sender, e) => {
                StartActivity(typeof(PagInicCliente));
			};
        }
    }
}