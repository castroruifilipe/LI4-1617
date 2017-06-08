﻿﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using PickaPrato.Business;

namespace PickaPrato.Presentation {
    
    [Activity(Label = "AdicionarComentario")]


    public class AdicionarComentario : Android.Support.V4.App.DialogFragment {

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

			var view = inflater.Inflate(Resource.Layout.AdicionarComentario, container, true);

			// Handle dismiss button click
			Button_Dismiss = view.FindViewById<Button>(Resource.Id.Button_Dismiss);
			Button_Dismiss.Click += Button_Dismiss_Click;

			return view;
		}
	}
}
