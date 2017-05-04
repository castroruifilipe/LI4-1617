using Android.App;
using Android.Widget;
using Android.OS;

namespace PickaPrato {
    
    [Activity(Label = "Pick'a Prato", MainLauncher = true, Icon = "@mipmap/icon")]

    public class MainActivity : Activity {
        
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var registarButtom = FindViewById<Button>(Resource.Id.registarcliente);
            registarButtom.Click += (sender, e) => {
                StartActivity(typeof(Registar));
			};
        }
    }
}

