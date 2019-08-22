using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NewDataAccess.Model
{
	public class PmContextFactory : IDesignTimeDbContextFactory<PmDbContext>
	{
		public PmDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<PmDbContext>();

			optionsBuilder.UseSqlServer(
				"Data Source=ASUS-AVARUS\\SQLEXPRESS,1433; Initial Catalog=PatientManagementDb;User ID=sa;Password=kissel");

			return new PmDbContext(optionsBuilder.Options);
		}
	}
}
