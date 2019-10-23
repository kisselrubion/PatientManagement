namespace WristCare.Model
{
	public class CourseHistory
	{
		public int CourseHistoryId { get; set; }
		public int CourseHistoryNumber { get; set; }
		public int? CourseId { get; set; }
		public Course Course { get; set; }
		public int? PatientId { get; set; }
		public int? NurseId { get; set; }
		public int? DoctorId { get; set; }
		public int? RoomId { get; set; }

		public Patient Patient { get; set; }
		public Nurse Nurse { get; set; }
		public Doctor Doctor { get; set; }
		public Room Room { get; set; }
	}
}