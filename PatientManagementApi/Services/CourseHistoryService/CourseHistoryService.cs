using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.CourseHistoryService
{
	public class CourseHistoryService : ICourseHistoryService
	{
		private readonly PMDbContext _context;
		public CourseHistoryService(PMDbContext context)
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
			try
			{
				_context.CourseHistories.Update(courseHistory);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<CourseHistory> Get(int id)
		{
			var course = await _context.CourseHistories.FirstOrDefaultAsync(c => c.CourseHistoryNumber == id);
			return course ?? new CourseHistory();
		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}
	}
}
