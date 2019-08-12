using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Util;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using WristCare.Droid;
using WristCare.Model;
using WristCare.Service.Bluetooth;
using Console = System.Console;

[assembly: Xamarin.Forms.Dependency(typeof(BluetoothService))]

namespace WristCare.Droid
{
	public class BluetoothService : IBleService
	{
		public async Task<BleDevice> GetBluetoothContent()
		{
			string rfid = "";

			var adapter = BluetoothAdapter.DefaultAdapter;

			if (adapter == null)
				throw new Exception("No Bluetooth adapter found.");

			if (!adapter.IsEnabled)
				throw new Exception("Bluetooth adapter is not enabled.");

			var bleDevice = (from bd in adapter.BondedDevices where bd.Name =="MLT-BT05"  select bd).FirstOrDefault();

			if (bleDevice != null)
			{
				var bleSocket = bleDevice.CreateRfcommSocketToServiceRecord(UUID.FromString("0000FFE0-0000-1000-8000-00805F9B34FB"));
				try
				{
					if (bleSocket != null)
					{
						await bleSocket.ConnectAsync();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}

				if (bleSocket != null && bleSocket.IsConnected)
				{
					
				}

				var mReader = new InputStreamReader(bleSocket.InputStream);
				var buffer = new BufferedReader(mReader);

				//if (device.State == DeviceState.Connected)
				//{
					rfid = await buffer.ReadLineAsync();
				//}
			}

			return new BleDevice
			{
				//Name = device.Name,
				Value =  rfid,
			};
		}
	}
}