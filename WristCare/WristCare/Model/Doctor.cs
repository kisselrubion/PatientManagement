using System;
using System.Collections.Generic;
using System.Text;

namespace WristCare.Model
{
	public class Doctor
	{
		public int DoctorId { get; set; }
		public string DoctorNumber { get; set; }

		public int? AccountId { get; set; }

		public Account Account { get; set; }
		public List<CourseHistory> CourseHistories { get; set; }
	}
}
