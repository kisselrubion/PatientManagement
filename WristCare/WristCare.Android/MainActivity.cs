﻿using System;

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
		public NfcAdapter NFCdevice;
		public NfcForms x;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			//For popup pages
			Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

			//Required according to the Docs
			NfcManager NfcManager = (NfcManager)Android.App.Application.Context.GetSystemService(Context.NfcService);
			NFCdevice = NfcManager.DefaultAdapter;

			Xamarin.Forms.DependencyService.Register<INfcForms, NfcForms>();
			x = Xamarin.Forms.DependencyService.Get<INfcForms>() as NfcForms;

			LoadApplication(new App());
		}

		protected override void OnResume()
		{
			base.OnResume();
			if (NFCdevice != null)
			{
				var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
				NFCdevice.EnableForegroundDispatch
				(
					this,
					PendingIntent.GetActivity(this, 0, intent, 0),
					new[] { new IntentFilter(NfcAdapter.ActionTechDiscovered) },
					new String[][] {new string[] {
							NFCTechs.Ndef,
						},
						new string[] {
							NFCTechs.MifareClassic,
						},
					}
				);
			}
		}

		protected override void OnPause()
		{
			base.OnPause();
			NFCdevice.DisableForegroundDispatch(this);
		}

		protected override void OnNewIntent(Intent intent)
		{
			base.OnNewIntent(intent);
			x.OnNewIntent(this, intent);
		}
	}
}