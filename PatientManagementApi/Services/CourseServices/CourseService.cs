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
				var lastCourse = await _context.Courses.LastAsync();
				//reference ID indexer
				course.TransactionId = lastCourse.TransactionId + 1;

				var addedCourse = await _context.Courses.AddAsync(course);

				await _context.SaveChangesAsync();

				//every instance of a course added there's also a course history created
				var newCourseHist = new CourseHistory { CourseId = addedCourse.Entity.CourseId };
				var lastCourseHistory = await _context.CourseHistories.LastAsync();
				//auto indexer
				newCourseHist.CourseHistoryNumber = lastCourseHistory.CourseHistoryNumber + 1;
				await _context.CourseHistories.AddAsync(newCourseHist);

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
				_context.SaveChanges();
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
			if (course == null) return new Course();
			{
				course.Medicines = await _context.Medicines.Where(c => c.CourseId == course.CourseId).ToListAsync();
				course.Measurements = await _context.Measurements.Where(c => c.CourseId == course.CourseId).ToListAsync();
				course.Procedures = await _context.Procedures.Where(c => c.CourseId == course.CourseId).ToListAsync();
				course.Transfers = await _context.Transfers.Where(c => c.CourseId == course.CourseId).ToListAsync();
			}
			return course;
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
