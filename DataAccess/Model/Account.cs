using System.Collections.Generic;

namespace DataAccess.Model
{
	public class Account
	{
		public int AccountId { get; set; }
		public int AccountTypeId { get; set; }
		public string AccountTypeName { get; set; }
		public int? UserId { get; set; }
		public int? EmployeeId { get; set; }

		public List<User> Users { get; set; }
		public List<AccountType> AccountTypes { get; set; }
	}
}