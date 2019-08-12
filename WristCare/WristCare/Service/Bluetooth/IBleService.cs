using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE.Abstractions.Contracts;
using WristCare.Model;

namespace WristCare.Service.Bluetooth
{
	public interface IBleService
	{
		Task<BleDevice> GetBluetoothContent();
	}
}
