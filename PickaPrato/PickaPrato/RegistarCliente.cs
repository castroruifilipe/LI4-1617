
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;

namespace PickaPrato
{

    [Activity(Label = "RegistarCliente")]

    public class Registar : Activity {

		public static readonly int PickImageId = 1000;
		private ImageView _imageView;
        
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RegistarCliente);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Registar novo cliente";

            _imageView = FindViewById<ImageView>(Resource.Id.imgview);
            Button button = FindViewById<Button>(Resource.Id.escolherimg);
			button.Click += ButtonOnClick;


        }

		private void ButtonOnClick(object sender, EventArgs eventArgs) {
			Intent = new Intent();
			Intent.SetType("image/*");
			Intent.SetAction(Intent.ActionGetContent);
			StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
			if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null)) {
				Android.Net.Uri uri = data.Data;
				_imageView.SetImageURI(uri);
			}
		}
    }
}
