using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Quick.Xamarin.BLE;
using Quick.Xamarin.BLE.Abstractions;
using WristCare.ViewModel.Base;
using Xamarin.Forms;

namespace WristCare.ViewModel
{
	public class ScanViewModel : BaseViewModel
	{
		public IDev ScannedDevice { get; set; }
		public ObservableCollection<IDev> Devices { get; set; }

		public  AdapterConnectStatus BleStatus { get; set; }
		public ScanViewModel()
		{
			Devices = new ObservableCollection<IDev>();
			//StartBluetoothScan();
		}

		public void StartBluetoothScan()
		{
			var bleConnection = CrossBle.Createble();
			bleConnection.OnScanDevicesIn += (sender, device) =>
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					//use device.Name or Uuid or Rssi or connect dev
					ScannedDevice = device;
					Devices.Add(device);
				});
			};
			//start scan
			bleConnection.StartScanningForDevices();
		}

		public bool ConnectToBluetoothDevice()
		{
			bool IsConnected;
			try
			{
				ScannedDevice.ConnectToDevice();
				IsConnected = true;
			}
			catch (Exception e)
			{
				IsConnected = false;
			}
			return IsConnected;
		}
	}
}
