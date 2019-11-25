using System;
using System.Collections.Generic;
using System.Text;

namespace WristCare.Model
{
	public class Nurse
	{
		public int NurseId { get; set; }
		public string NurseNumber { get; set; }
		public int? AccountId { get; set; }

		public Account Account { get; set; }
		public List<CourseHistory> CourseHistories { get; set; }
	}
}
