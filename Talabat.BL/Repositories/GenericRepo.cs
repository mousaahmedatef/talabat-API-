using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;
using Talabat.BL.Repositories.Specifications;
using Talabat.DAL.Data;
using Talabat.DAL.Entities;

namespace Talabat.BL.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepo(StoreContext context)
        {
            this.context = context;
        }
        public async Task<IReadOnlyList<T>> GeAllAsync()
            => await context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await context.Set<T>().FindAsync(id);

        public  IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>() , spec);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
            //context.set<Product>.Where(p=>p.id==id).Include(P=>P.productBrand).Include(P=>P.ProductType).FirstOrDefaultAsync();

        }

        public async Task<IReadOnlyList<T>> GeAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
            => await ApplySpecifications(spec).CountAsync();

        public async Task Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);

        }
    }
}
