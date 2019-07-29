using System;
using System.Collections.Generic;
using System.Text;

namespace WristCare.Service.Bluetooth
{
	public interface IBle
	{
		void Start(string name, int sleepTime, bool readAsCharArray);
		void Stop();
		List<string> GetPairedDevices();
	}
}
