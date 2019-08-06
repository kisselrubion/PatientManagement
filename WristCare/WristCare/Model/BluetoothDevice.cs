using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WristCare.Model
{
	public class BluetoothDevice
	{
		public Guid DeviceId { get; set; }
		public object Address { get; set; }
		public string Name { get; set; }

		public Color StatusIndicator { get; set; }
		//public override string ToString()
		//{
		//	return Name +" "+ Address;
		//}
	}
}
