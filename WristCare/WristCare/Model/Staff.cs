using System;
using System.Collections.Generic;
using System.Text;

namespace WristCare.Model
{
	public class Staff
	{
		public int StaffId { get; set; }
		public int? StaffNumber { get; set; }
		public int? AccountId { get; set; }
		public Account Account { get; set; }
	}
}
