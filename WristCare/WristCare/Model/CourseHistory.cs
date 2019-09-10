namespace WristCare.Model
{
	public class CourseHistory
	{
		public int CourseHistoryId { get; set; }
		public int? CourseHistoryNumber { get; set; }
		public int? CourseId { get; set; }
		public int? AccountId { get; set; }
		public Account Account { get; set; }
		public Course Course { get; set; }
	}
}