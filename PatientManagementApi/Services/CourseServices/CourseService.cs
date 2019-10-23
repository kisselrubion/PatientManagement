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
			try
			{
				_context.Courses.Update(course);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<Course> Get(int id)
		{
			var course = await _context.Courses.FirstOrDefaultAsync(c => c.TransactionId == id && c.IsArchived == false);
			return course ?? new Course();
		}
		public List<Course> GetRange(bool all)
		{
			if(!all) return new List<Course>();
			var courses = _context.Courses.Where(c => c.IsArchived == false).ToList();
			return courses;
		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}
	}
}
