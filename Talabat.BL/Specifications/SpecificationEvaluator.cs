using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BL.Repositories.Specifications
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
        #region comments
        //IQueryable عموما بيبقا نوعها  query والمفروض ان ال query هنا المفروض ان الفانشن دى الى هترجعلى ال
        //inputQuery -- context.set<Product> دى كدا عف حالتى هنا مثلا هتمثلى ال
        //spec --.Where(p=>p.id==id) او .Include(P=>P.productBrand).Include(P=>P.ProductType) سواء query ودى طبعا بتمثلى بقا ال
        #endregion
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecification<TEntity> spec)
        {
            var query = inputQuery; //context.set<Product>()
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);  //context.set<Product>().Where(p=>p.id==id)


            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            //context.set<Product>().Where(p=>p.id==id).OrederBy(p=>p.Name)
            //or --> context.set<Product>().OrederBy(p=>p.Name)


            if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);
            //context.set<Product>().Where(p=>p.id==id).OrederByDesc(p=>p.Name)
            //or --> context.set<Product>().OrederByDesc(p=>p.Name)


            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);


            query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));
            #region comments
            //context.set<Product>().Include(P=>P.productBrand).Include(P=>P.ProductType)
            //or
            //context.set<Product>().Where(p=>p.id==id).Include(P=>P.productBrand).Include(P=>P.ProductType)
            //or
            //context.set<Product>().OrederBy(p=>p.Name).Include(P=>P.productBrand).Include(P=>P.ProductType)
            //or
            //context.set<Product>().Where(p=>p.id==id).OrederBy(p=>p.Name).Include(P=>P.productBrand).Include(P=>P.ProductType)
            //or
            //context.set<Product>().OrederByDesc(p=>p.Name).Include(P=>P.productBrand).Include(P=>P.ProductType)
            //or
            //context.set<Product>().Where(p=>p.id==id).OrederByDesc(p=>p.Name).Include(P=>P.productBrand).Include(P=>P.ProductType)
            //or
            //context.set<Product>().Where(p=>p.id==id).OrederByDesc(p=>p.Name).Include(P=>P.productBrand).Include(P=>P.ProductType).Skip(5).take(10);
            #endregion

            #region Comments
            //Aggregate --include ورا include واحده واحده وتضيف Includes ألى فوقى دا المفروض انها بعتدى على ال Syntax دى كدا بال
            #endregion

            return query;
        }
    }
}
