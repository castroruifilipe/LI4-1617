
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

    [Activity(Label = "Registar")]

    public class Registar : Activity {
        
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Registar);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Registar";

            var tablayout = FindViewById<TabLayout>(Resource.Id.tabs);
			tablayout.AddTab(tablayout.NewTab().SetText("Cliente"));
            tablayout.AddTab(tablayout.NewTab().SetText("Proprietário"));
        }
    }
}
