using System.Collections.Generic;

namespace WristCare.Model
{
	public class Transfer
	{
		public int TransferId { get; set; }
		public int? TransferNumber { get; set; }
		public int? CourseId { get; set; }
		public int? RoomId { get; set; }
		public Course Course { get; set; }
		public Room Rooms { get; set; }

	}
}