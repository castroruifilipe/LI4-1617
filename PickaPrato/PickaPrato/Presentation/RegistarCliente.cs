
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

namespace PickaPrato.Presentation {

    [Activity(Label = "RegistarCliente")]

    public class RegistarCliente : Activity {

		public static readonly int PickImageId = 1000;
		private ImageView imageView;
        private CardView cardv;
        private SupportToolbar toolbar;
        
        
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
            botaoRegistar.Click += async (sender, e) =>
            {
                string url = "http://localhost:50646/api/Default/12";
                Person p = new Person();
                p.Username = user.Text;
                p.Password = pass.Text;
                Console.Out.WriteLine("\n\n\n\n\n\nCriei Pessoa\n\n\n\n\n\n");
                JsonValue json = await PostClienteAsync(url, p);
            };

        }

        private async Task<JsonValue> PostClienteAsync(String url, Person person)
        {
            Console.Out.WriteLine("\n\n\n\n\n\nEntrei na Função \n\n\n\n\n\n");
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            Console.Out.WriteLine("\n\n\n\n\n\n Criei Coisas \n\n\n\n\n\n");
            var json = JsonConvert.SerializeObject(person);
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(json);

            Console.Out.WriteLine("\n\n\n\n\n\n Streams \n\n\n\n\n\n");
            Stream newStream = request.GetRequestStream();
            Console.Out.WriteLine("\n\n\n\n\n\n Get Request Stream Estoura \n\n\n\n\n\n");
            newStream.Write(bytes, 0, bytes.Length);
            //newStream.Close();
            Console.Out.WriteLine("\n\n\n\n\n\nRespostas\n\n\n\n\n\n");
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
            Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();
            Console.Out.WriteLine("\n\n\n\n\n\nRetorna\n\n\n\n\n\n");
            return jsonDoc;

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
