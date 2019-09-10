using System.Collections.Generic;

namespace WristCare.Model
{
	public class Transfer
	{
		public int TransferId { get; set; }
		public int? TransferNumber { get; set; }

		public List<Course> Courses { get; set; }

	}
}