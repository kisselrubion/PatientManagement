using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementApi.Services.BaseService;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.MedicineServices
{
	public class MedicineService : IBaseService<Medicine>
	{
		private readonly PMDbContext _context;

		public MedicineService(PMDbContext context)
		{
			_context = context;
		}

		public async Task<Medicine> Post(Medicine item)
		{
			try
			{
				var addedMedicine = await _context.Medicines.AddAsync(item);
				await _context.SaveChangesAsync();
				return addedMedicine.Entity;
			}
			catch
			{
				return new Medicine();
			}
		}

		public bool Put(Medicine item)
		{
			try
			{
				_context.Medicines.Update(item);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<Medicine> Get(string id)
		{
			var medicine = await _context.Medicines.FirstOrDefaultAsync(c => c.MedicineNumber == int.Parse(id));
			return medicine ?? new Medicine();
		}

		public async Task<List<Medicine>> GetByForeignKey(int id)
		{
			var medicine = await _context.Medicines.Where(c => c.CourseId == id).ToListAsync();
			return medicine;
		}

		public async Task<ICollection<Medicine>> GetRange(bool all)
		{
			if (!all) return new List<Medicine>();
			var medicines = await _context.Medicines.ToListAsync();
			return medicines;
		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}

		public void Add(Medicine item)
		{
			throw new NotImplementedException();
		}

		public void AddRange(ICollection<Medicine> items)
		{
			throw new NotImplementedException();
		}

		public void Remove(Medicine item)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(ICollection<Medicine> items)
		{
			throw new NotImplementedException();
		}

		public void Update(Medicine item)
		{
			throw new NotImplementedException();
		}

		public void Update(ICollection<Medicine> items)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Medicine> All()
		{
			throw new NotImplementedException();
		}
	}
}
