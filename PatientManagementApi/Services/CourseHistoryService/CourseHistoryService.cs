﻿using System;
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


				var patient = await _context.Patients.FirstOrDefaultAsync(c => c.PatientNumber == courseHistory.UserAccountNumber);
				courseHistory.PatientId = patient.PatientId;

				//var doctor = await _context.Doctors.FirstOrDefaultAsync(c =>c.DoctorNumber == courseHistory.UserAccountNumber)

				//auto indexer
				var addedCourseHistory = _context.CourseHistories.Update(courseHistory);
				await _context.SaveChangesAsync();
				return addedCourseHistory.Entity;
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
				return new CourseHistory();
			}
		}

		public CourseHistory Put(CourseHistory courseHistory)
		{
			try
			{
				var content = _context.CourseHistories.Update(courseHistory);
				_context.SaveChanges();
				return content.Entity;
			}
			catch
			{
				return new CourseHistory();
			}
		}

		public async Task<CourseHistory> Get(int id)
		{

			var course = await _context.CourseHistories.FirstOrDefaultAsync(c => c.CourseHistoryNumber == id);
			return course ?? new CourseHistory();
		}

		public List<CourseHistory> GetRange(bool all)
		{
			if (!all) return new List<CourseHistory>();
			var courseHistories = _context.CourseHistories.ToList();
			return courseHistories;
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
