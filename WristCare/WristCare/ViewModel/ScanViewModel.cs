using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
		private BluetoothDevice _selectedDevice;
		private bool _bleConnectionStatus;
		public ObservableCollection<string> Devices { get; set; }
		public ObservableCollection<BluetoothDevice> BleDevices { get; set; }
		public Color Indicator { get; set; }

		public bool BleConnectionStatus
		{
			get => _bleConnectionStatus;
			set
			{
				if (Set(ref _bleConnectionStatus, value))
				{
					if (_bleConnectionStatus)
					{
					}
				}

				
			}
		}

		public IDevice BleDevice
		{
			get => _bleDevice;
			set => Set(ref _bleDevice, value);
		}
		public BluetoothDevice SelectedDevice
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
			BleDevices = new ObservableCollection<BluetoothDevice>();
			Devices = new ObservableCollection<string>();
			Indicator = Color.FromHex("4FDEA4");
		}
		public ICommand StartBleScanCommand => new RelayCommand( () =>  BleStartConnection());


		/// Starts the scan and connection for available bluetooth devices
		public void BleStartConnection()
		{
			BleDevices.Clear();
			var devicesNames = new List<string>();
			var adapter = CrossBleAdapter.Current;

			if (CrossBleAdapter.Current.Status == AdapterStatus.PoweredOn)
			{
				var scanner = CrossBleAdapter.Current
					.ScanInterval(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10)).Subscribe(content =>
					{

						var newDevice = new BluetoothDevice
						{
							Name = content.Device.Name,
							Address = content.Device.NativeDevice,
							StatusIndicator = Indicator,
						};

						if (content.Device.IsPairingAvailable() && !devicesNames.Contains(newDevice.Name) &&
							content.Device.Name != null)
						{
							devicesNames.Add(newDevice.Name);
							BleDevices.Add(newDevice);
						}

						if (SelectedDevice != null)
						{
							if (content.Device.Name == SelectedDevice.Name)
							{
								BleDevice = content.Device;
								content.Device.Connect();
								BleConnectionStatus = content.Device.IsConnected();
							}
						}

					});
			}
		}

		public void GetConnectedDevices()
		{
			var devices = CrossBleAdapter.Current.GetConnectedDevices();
			devices.Subscribe(deviceResult =>
			{
				foreach (var device in deviceResult)
				{
					BleDevices.Add(new BluetoothDevice
					{
						Name = device.Name
					});
				}
			});
		}
	}
}
