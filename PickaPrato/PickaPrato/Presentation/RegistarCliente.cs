
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Newtonsoft.Json;
using System.Json;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using System.Threading.Tasks;
using PickaPrato.Business;
using System.Net;
using System.IO;
using PickaPrato.Data;

namespace PickaPrato.Presentation {

    [Activity(Label = "RegistarCliente")]

    public class RegistarCliente : Activity {

		public static readonly int PickImageId = 1000;
		private ImageView imageView;
        private CardView cardv;
        private SupportToolbar toolbar;
        private ServiceEngine se = new ServiceEngine("http://10.0.2.2:5001/");
        
        
        protected override void OnCreate(Bundle savedInstanceState) {
            
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RegistarCliente);

            

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			TextView mTitle = (TextView)toolbar.FindViewById(Resource.Id.toolbar_title);
            mTitle.SetText("Registar cliente",TextView.BufferType.Normal);

            cardv = FindViewById<CardView>(Resource.Id.cardview);
            cardv.Visibility = ViewStates.Invisible;

            imageView = FindViewById<ImageView>(Resource.Id.imageview);
            Button button = FindViewById<Button>(Resource.Id.escolherimg);
			button.Click += ButtonOnClick;
           
            EditText user = FindViewById<EditText>(Resource.Id.username_edittext);
            EditText pass = FindViewById<EditText>(Resource.Id.password_edittext);
            Console.Out.WriteLine("\n\n\n\n\n\nOlá\n\n\n\n\n\n");
            Button botaoRegistar = FindViewById<Button>(Resource.Id.bregistar);
            botaoRegistar.Click += (sender, e) =>
            {
                Cliente c = se.GetCliente().Result;
                user.Text = c.Username;
            };

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
				imageView.SetImageURI(uri);
                cardv.Visibility = ViewStates.Visible;
			}
		}
    }
}
