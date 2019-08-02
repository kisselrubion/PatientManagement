using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Plugin.BluetoothLE;
using WristCare.Model;
using WristCare.ViewModel.Base;
using Xamarin.Forms;

namespace WristCare.ViewModel
{
	public class ScanViewModel : BaseViewModel
	{
		private IDevice _bleDevice;
		private string _selectedDevice;
		public ObservableCollection<string> Devices { get; set; }
		public ObservableCollection<BluetoothDevice> BleDevices { get; set; }
		public IDevice BleDevice
		{
			get => _bleDevice;
			set => Set(ref _bleDevice, value);
		}

		public string SelectedDevice
		{
			get => _selectedDevice;
			set
			{
				if (Set(ref _selectedDevice, value))
				{
					
				}
			}
		}


		public ScanViewModel()
		{
			Devices = new ObservableCollection<string>();
			BleDevices = new ObservableCollection<BluetoothDevice>();

		}


		public ICommand StartBleScanCommand => new RelayCommand( () =>  StartBleScan());


		/// Starts the scan for available bluetooth devices
		public void  StartBleScan()
		{
			BleDevices.Clear();
			try
			{
				if (CrossBleAdapter.Current.Status == AdapterStatus.PoweredOn)
				{
					var scanner = CrossBleAdapter.Current.ScanInterval(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10)).Subscribe(content =>
					{
						var newDevice = new BluetoothDevice
						{
							Name = content.Device.Name,
							Address = content.Device.NativeDevice,
						};

						if (content.Device.IsPairingAvailable() && !Devices.Contains(newDevice.Name) && content.Device.Name != null)
						{
							//if (content.Device.Name != null)
							//{
								Devices.Add(content.Device.Name);
							//}
							if (string.IsNullOrEmpty(content.Device.Name))
							{
								Devices.Add(newDevice.Address.ToString());
							}
							//else
							//{
							//}
						}
					});
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}


			//var bleAdapter = CrossBleAdapter.Current.Scan().Subscribe(scanResult => { });

			//var state = bluetooth.State;
			//if (state != BluetoothState.On) return;

			//adapter.DeviceDiscovered += (s, a) =>
			//{
			//	if (a.Device.Name != null && !Devices.Contains(a.Device.Name))
			//	{
			//		Devices.Add(a.Device.Name);
			//	}
			//};

			//if (!bluetooth.Adapter.IsScanning)
			//{
			//	await adapter.StartScanningForDevicesAsync();
			//}
		}

		public bool ConnectToBleDevice()
		{
			return true;
		}
	}
}
