using System;
using System.Collections.Generic;
using System.Text;

namespace WristCare.Model
{
	public class Room
	{
		public int RoomId { get; set; }
		public int? RoomNumber { get; set; }
		public int? PatientId { get; set; }
		public Patient Patient { get; set; }
		public List<Transfer> Transfers { get; set; }
	}
}
