using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.CourseServices
{
	public class CourseService : ICourseService
	{
		private readonly PMDbContext _context;

		public CourseService(PMDbContext context)
		{
			_context = context;
		}

		public async Task<Course> Post(Course course)
		{
			try
			{
				var addedCourse = await _context.Courses.AddAsync(course);
				await _context.SaveChangesAsync();
				return addedCourse.Entity;
			}
			catch
			{
				return new Course();
			}
		}

		public bool Put(Course course)
		{
			throw new NotImplementedException();
		}

		public async Task<Course> Get(string id)
		{
			var course = await _context.Courses.FirstOrDefaultAsync(c => c.TransactionId == id);
			return course ?? new Course();
		}

		public Task<bool> Remove(string id)
		{
			throw new NotImplementedException();
		}
	}
}
