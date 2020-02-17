using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Rg.Plugins.Popup.Services;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.Bluetooth;
using WristCare.Service.MedicalPlan;
using WristCare.View;
using WristCare.ViewModel.Base;
using WristCare.Views;
using Xamarin.Forms;

namespace WristCare.ViewModel
{
	public class ScanViewModel : BaseViewModel
	{
		private IDevice _bleDevice;
		private IDevice _selectedDevice;
		private bool _bleConnectionStatus;
		private string _rfidData;
		private readonly MedicalPlanService _medicalPlanService;


		public ObservableCollection<IDevice> Devices { get; set; }
		public ObservableCollection<CourseHistory> CourseHistories { get; set; }
		public ObservableCollection<BleDevice> BleDevices { get; set; }
		public Color Indicator { get; set; }

		public string RfidData
		{
			get => _rfidData;
			set
			{
				if (Set(ref _rfidData, value))
				{
				}
			}
		}



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
			set
			{
				if (Set(ref _bleDevice, value))
				{
					
				}

				
			}
		}
		public IDevice SelectedDevice
		{
			get => _selectedDevice;
			set
			{
				if (Set(ref _selectedDevice, value))
				{
					Task.Run(async () => await BleDeviceConnect(SelectedDevice));
				}
			}
		}

		

		public string TestRfid { get; set; }
		public ScanViewModel(MedicalPlanService medicalPlanService)
		{
			_medicalPlanService = medicalPlanService;
			BleDevices = new ObservableCollection<BleDevice>();
			CourseHistories = new ObservableCollection<CourseHistory>();
			Devices = new ObservableCollection<IDevice>();
			Indicator = Color.FromHex("4FDEA4");
			IsVisible = false;
			Task.Run(async () => await GetAllCourseHistories());
		}
		public ICommand StartBleScanCommand => new RelayCommand(  async () => await  BleStartConnection());
		public ICommand ScanWristBandCommand => new RelayCommand(ScanWristband);


		/// Starts the scan and connection for available bluetooth devices
		public async Task BleStartConnection()
		{
			//var deviceVersion = await DependencyService.Get<IBleService>().GetBluetoothContent();
			Devices.Clear();
			var devicesNames = new List<string>();
			var ble = CrossBluetoothLE.Current;
			var adapter = CrossBluetoothLE.Current.Adapter;

			adapter.DeviceDiscovered += (s, a) =>
			{
				BleDevice = a.Device;
				if (a.Device.Name != null)
				{
					if (a.Device.Name == "MLT-BT05" && !Devices.Contains(a.Device))
					{
						BleDeviceConnect(a.Device);
					}
				}
			};
			await adapter.StartScanningForDevicesAsync();
			//if (SelectedDevice != null)
			//{
			//	var result = await adapter.ConnectToDeviceAsync(SelectedDevice);
			//	var service = await SelectedDevice.GetServicesAsync();
			//}
			#region Initial BLE Implementation

			//var adapter = CrossBleAdapter.Current;
			//if (CrossBleAdapter.Current.Status == AdapterStatus.PoweredOn)
			//{
			//	var scanner = CrossBleAdapter.Current
			//		.ScanInterval(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10)).Subscribe(content =>
			//		{

			//			var newDevice = new BleDevice
			//			{
			//				Name = content.Device.Name,
			//				Address = content.Device.NativeDevice,
			//				StatusIndicator = Indicator,
			//			};

			//			if (content.Device.IsPairingAvailable() && !devicesNames.Contains(newDevice.Name) && content.Device.Name != null)
			//			{
			//				devicesNames.Add(newDevice.Name);
			//				BleDevices.Add(newDevice);
			//			}

			//			if (SelectedDevice != null)
			//			{
			//				if (content.Device.Name == SelectedDevice.Name)
			//				{
			//					BleDevice = content.Device;
			//					content.Device.Connect();
			//					content.Device.ReadRssi();
			//					BleConnectionStatus = content.Device.IsConnected();
			//				}
			//			}

			//			if (BleDevice != null)
			//			{
			//				var coms = BleDevice.WhenAnyCharacteristicDiscovered().Subscribe(  async characteristic =>
			//				{
			//					//{
			//					//	var output = result.Data;
			//					//	//await Application.Current.MainPage.DisplayAlert("Read", output.ToString(), "ok");
			//					//});

			//					//characteristic.EnableNotifications();
			//					//characteristic.WhenNotificationReceived().Subscribe(result =>
			//					//{
			//					//	var output = result.Data;
			//					//});
			//					//await Application.Current.MainPage.DisplayAlert("Read", result.Data.ToString(), "ok");
			//					//if (response.Description == TestRfid)
			//					//{
			//					//	SelectedDevice.StatusIndicator = Color.Crimson;
			//					//}
			//				});
			//			}

			//		});


			//}

			#endregion
		}

		public async Task GetAllCourseHistories()
		{
			var courseHistories = await _medicalPlanService.GetAllCourseHistories();
			if (courseHistories.Count != 0)
			{
				foreach (var courseHistory in courseHistories)
				{
					CourseHistories.Add(courseHistory);
				}
			}
		}

		private async Task BleDeviceConnect(IDevice selectedDevice)
		{
			if (selectedDevice == null) return;
			IsBusy = true;
			try
			{
				var adapter = CrossBluetoothLE.Current.Adapter;
				var param = new ConnectParameters(forceBleTransport: true);
				await adapter.ConnectToDeviceAsync(selectedDevice, param, CancellationToken.None);
				await PopupHelper.ActionResultMessage("Success", "Connected to scanner");
				IsVisible = true;
				IsDisabled = true;
				await ReadDevice(selectedDevice, true);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			IsBusy = false;
		}

		public async Task ReadDevice(IDevice selectedDevice, bool status)
		{
			if (status)
			{
				//var service = await selectedDevice.GetServicesAsync();
				var service = await selectedDevice.GetServiceAsync(Guid.Parse("0000FFE0-0000-1000-8000-00805F9B34FB"));

				var characteristic = await service.GetCharacteristicAsync(Guid.Parse("0000FFE1-0000-1000-8000-00805F9B34FB"));

				await characteristic.StartUpdatesAsync();

				characteristic.ValueUpdated += (o, args) =>
				{
					var conts = args.Characteristic.Value;
					string result = System.Text.Encoding.UTF8.GetString(conts);
					RfidData = result.Replace("\r\n", "");

				};

				var bytes = characteristic.Value;
				string content = System.Text.Encoding.UTF8.GetString(bytes);

				RfidData = content;

				
					
			}
		}

		private void ScanWristband()
		{
			if (!string.IsNullOrEmpty(RfidData))
			{
				var courseHistory = CourseHistories.FirstOrDefault(c => c.UserAccountNumber == RfidData);
				var courses = App.Locator.CourseViewModel.Courses;
				if (courses.Count != 0 && courseHistory != null)
				{
					var selectedCourse = courses.FirstOrDefault(c => courseHistory != null && c.CourseId == courseHistory.CourseId);

					App.Locator.CourseDetailsViewModel.SelectedCourse = selectedCourse;
					navigationService.NavigateTo(Locator.CourseDetailsPage);
				}
			}
		}
	}
}
