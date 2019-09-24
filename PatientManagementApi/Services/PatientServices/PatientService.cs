using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementApi.Services.BaseService;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.PatientServices
{
	public class PatientService : IBaseService<Patient>
	{
		private readonly PMDbContext _context;

		public PatientService(PMDbContext context)
		{
			_context = context;
		}

		public async Task<Patient> Post(Patient item)
		{
			try
			{
				var addedPatient = await _context.Patients.AddAsync(item);
				await _context.SaveChangesAsync();
				return addedPatient.Entity;
			}
			catch
			{
				return new Patient();
			}
		}

		public bool Put(Patient item)
		{
			try
			{
				_context.Patients.Update(item);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<Patient> Get(string id)
		{
			var patient = await _context.Patients.FirstOrDefaultAsync(c => c.PatientNumber == int.Parse(id));
			return patient ?? new Patient();
		}

		public async Task<ICollection<Patient>> GetRange(bool all)
		{
			if (!all) return new List<Patient>();
			var patients = await _context.Patients.ToListAsync();
			return patients;
		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}

		public void Add(Patient item)
		{
			throw new NotImplementedException();
		}

		public void AddRange(ICollection<Patient> items)
		{
			throw new NotImplementedException();
		}

		public void Remove(Patient item)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(ICollection<Patient> items)
		{
			throw new NotImplementedException();
		}

		public void Update(Patient item)
		{
			throw new NotImplementedException();
		}

		public void Update(ICollection<Patient> items)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Patient> All()
		{
			throw new NotImplementedException();
		}
	}
}
