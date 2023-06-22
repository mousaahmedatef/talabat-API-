using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BL.Repositories.Specifications;
using Talabat.DAL.Entities;

namespace Talabat.BL.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GeAllAsync();
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GeAllWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCountAsync(ISpecification<T> spec);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
