namespace WristCare.Model
{
	public class Account
	{
		public int AccountId { get; set; }
		public string AccountNumber { get; set; }
		public int? AccountTypeId { get; set; }
		public string AccountTypeName { get; set; }
		public int? UserId { get; set; }
		public int? EmployeeId { get; set; }
		public User User { get; set; }
		public AccountType AccountType { get; set; }
	}
}