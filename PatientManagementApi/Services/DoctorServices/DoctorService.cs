using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementApi.Services.BaseService;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.DoctorServices
{
	public class DoctorService : IBaseService<Doctor>
	{
		private readonly PMDbContext _context;

		public DoctorService(PMDbContext context)
		{
			_context = context;
		}
		public async Task<Doctor> Post(Doctor item)
		{
			try
			{
				var addedDoctor = await _context.Doctors.AddAsync(item);
				await _context.SaveChangesAsync();
				return addedDoctor.Entity;
			}
			catch
			{
				return new Doctor();
			}
		}

		public bool Put(Doctor item)
		{
			try
			{
				_context.Doctors.Update(item);
				_context.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<Doctor> Get(string id)
		{
			var doctor = await _context.Doctors.FirstOrDefaultAsync(c => c.DoctorNumber == id);
			return doctor ?? new Doctor();
		}

		public async Task<ICollection<Doctor>> GetRange(bool all)
		{
			if(!all) return new List<Doctor>();
			var doctors = await _context.Doctors.ToListAsync();
			return doctors;
		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}

		public void Add(Doctor item)
		{
			throw new NotImplementedException();
		}

		public void AddRange(ICollection<Doctor> items)
		{
			throw new NotImplementedException();
		}

		public void Remove(Doctor item)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(ICollection<Doctor> items)
		{
			throw new NotImplementedException();
		}

		public void Update(Doctor item)
		{
			throw new NotImplementedException();
		}

		public void Update(ICollection<Doctor> items)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Doctor> All()
		{
			throw new NotImplementedException();
		}
	}
}
