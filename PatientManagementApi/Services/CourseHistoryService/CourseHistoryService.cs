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
				var lastCourseHistory = await _context.CourseHistories.LastAsync();

				var patient = await _context.Patients.FirstOrDefaultAsync(c => c.PatientNumber == courseHistory.UserAccountNumber);
				courseHistory.PatientId = patient.PatientId;

				//auto indexer
				courseHistory.CourseHistoryNumber = lastCourseHistory.CourseHistoryNumber + 1;
				var addedCourseHistory = await _context.CourseHistories.AddAsync(courseHistory);
				await _context.SaveChangesAsync();
				return addedCourseHistory.Entity;
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
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

		public async Task<CourseHistory> GetHistoryByCourse(int id)
		{
			try
			{
				var course = await _context.CourseHistories.FirstOrDefaultAsync(c => c.CourseId == id);
				if (course != null)
				{
					course.Patient = await _context.Patients.FirstOrDefaultAsync(c => c.PatientId == course.PatientId);
				}

				return course ?? new CourseHistory();
			}
			catch
			{
				return new CourseHistory();
			}


		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}
	}
}
