using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.CourseHistoryService
{
	public class CouseHistoryService : ICourseHistoryService
	{
		private readonly PMDbContext _context;
		public CouseHistoryService(PMDbContext context)
		{
			_context = context;
		}
		public async Task<CourseHistory> Post(CourseHistory courseHistory)
		{
			try
			{
				var addedCourseHistory = await _context.CourseHistories.AddAsync(courseHistory);
				await _context.SaveChangesAsync();
				return addedCourseHistory.Entity;
			}
			catch
			{
				return new CourseHistory();
			}
		}

		public bool Put(CourseHistory courseHistory)
		{
			throw new NotImplementedException();
		}

		public async Task<CourseHistory> Get(string id)
		{
			var course = await _context.CourseHistories.FirstOrDefaultAsync(c => c.CourseHistoryNumber == id);
			return course ?? new CourseHistory();
		}

		public Task<bool> Remove(string id)
		{
			throw new NotImplementedException();
		}
	}
}
