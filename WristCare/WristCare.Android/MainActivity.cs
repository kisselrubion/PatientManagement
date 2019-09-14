using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Poz1.NFCForms.Abstract;
using Poz1.NFCForms.Droid;
using Xamarin.Forms;

namespace WristCare.Droid
{
	[Activity(Label = "WristCare", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			//For popup pages
			Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

			LoadApplication(new App());
		}
	}
}