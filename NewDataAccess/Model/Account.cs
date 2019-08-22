using System;
using System.Collections.Generic;
using System.Text;

namespace NewDataAccess.Model
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
