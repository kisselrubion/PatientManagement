using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.BaseService
{
	public interface IBaseService<T>
	{
		Task<T> Post(T item);
		bool Put(T item);
		Task<T> Get(string id);
		Task<ICollection<T>> GetRange(bool all);
		Task<bool> Remove(string id);

		void Add(T item);
		void AddRange(ICollection<T> items);
		void Remove(T item);
		void RemoveRange(ICollection<T> items);
		void Update(T item);
		void Update(ICollection<T> items);

		IQueryable<T> All();
	}
}
