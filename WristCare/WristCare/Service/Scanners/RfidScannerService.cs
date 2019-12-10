using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace WristCare.Service.Scanners
{
	public class RfidScannerService
	{
		public RfidScannerService()
		{

		}

		public async Task<List<IDevice>> BleStartConnection()
		{
			var devices = new List<IDevice>();
			var adapter = CrossBluetoothLE.Current.Adapter;

			adapter.DeviceDiscovered += (s, a) =>
			{
				if (a.Device.Name != null)
				{
					devices.Add(a.Device);
				}
			};
			await adapter.StartScanningForDevicesAsync();
			return devices;
		}

		public async Task BleDeviceConnect(IDevice selectedDevice)
		{
			if (selectedDevice == null) return;
			try
			{
				var adapter = CrossBluetoothLE.Current.Adapter;
				var param = new ConnectParameters(forceBleTransport: true);
				await adapter.ConnectToDeviceAsync(selectedDevice, param, CancellationToken.None);
				await ReadDevice(selectedDevice, true);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

		}

		public async Task<string> ReadDevice(IDevice selectedDevice, bool status)
		{
			var data = "";
			if (status)
			{
				//var service = await selectedDevice.GetServicesAsync();
				var service = await selectedDevice.GetServiceAsync(Guid.Parse("0000FFE0-0000-1000-8000-00805F9B34FB"));

				var characteristic =
					await service.GetCharacteristicAsync(Guid.Parse("0000FFE1-0000-1000-8000-00805F9B34FB"));

				await characteristic.StartUpdatesAsync();

				characteristic.ValueUpdated += (o, args) =>
				{
					var conts = args.Characteristic.Value;
					var result = System.Text.Encoding.UTF8.GetString(conts);
					data = result.Replace("\r\n", "");

				};

				var bytes = characteristic.Value;
				var content = System.Text.Encoding.UTF8.GetString(bytes);
				data = content;
			}
			return data;
		}
	}
}
