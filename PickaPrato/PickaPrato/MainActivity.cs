using Android.App;
using Android.Widget;
using Android.OS;

namespace PickaPrato
{
    [Activity(Label = "Pick'a Prato", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.IniciarSessao);

        }
    }
}

