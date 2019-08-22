using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace NewDataAccess.Model
{
	public class PmDbContext : DbContext
	{
		public PmDbContext(DbContextOptions<PmDbContext> options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured) return;
			var connection = "data source=ASUS-AVARUS\\SQLEXPRESS,1433; Initial Catalog=PatientManagementDb;User ID=sa;Password=kissel";
			optionsBuilder.UseSqlServer(connection);
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<AccountType> AccountTypes { get; set; }
	}
}
