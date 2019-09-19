using System;
using System.Collections.Generic;
using System.Text;

namespace WristCare.Model
{
	public class Patient
	{
		public int PatientId { get; set; }
		public int? PatientNumber { get; set; }
		public int? AccountId { get; set; }
		public Account Account { get; set; }
		public List<CourseHistory> CourseHistories { get; set; }
		public List<Room> Rooms { get; set; }
	}
}
