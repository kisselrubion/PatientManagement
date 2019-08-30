using System.Collections.Generic;

namespace WristCare.Model
{
	public class AccountType
	{
		public int AccountTypeId { get; set; }
		public int? AccountId { get; set; }
		public List<Account> Accounts { get; set; }
	}
}