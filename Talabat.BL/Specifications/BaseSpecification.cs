using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.BL.Repositories.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }
        #region comments
        // context.set<Product> الكلاس دا عباره عن الى هيتكتب بعد دا
        //context.set<Product>.Where(p=>p.id==id) فكدا هيبقا فيه كوندشن الى هوا كدا id فلو انا هجيب منتج معين مثلا بال 
        //عشان navigation properties like ProductBrand لكل ال include ف انا محتاج اعمل products لكن انا لو هجيب كل ال  include فدا كدا مش محتاج اعمل 
        //context.set<Product>.Include(P=>P.productBrand).Include(P=>P.ProductType).ToList() بيانات البرندات بتاعه المنتجات تيجي معايا ف هتبقا كدا مثلا
        //expression لكن ف الحاله التانيه محتجتش اكتب اى , p=>p.id==id الى هوا دا expression ف ف الحاله الاولى انا احتجت اكتب 
        //includes  لكن ف الحاله التانيه احتجت اكتب , includes  ف ف الحاله الاولى انا  م احتجتش اكتب اى
        //null ولو ف  الحاله التانيه هتبقا ب p=>p.id==id ولو ف الحاله الاولى هتبقا ب expression بتمثلى ال Criteria ف هنا بقا ال
        //  Include(P=>P.productBrand).Include(P=>P.ProductType) وفالحاله التانيه ب null هتبقا ف الحاله الاولى ب  Includes وكذلك ال
        #endregion
        public Expression<Func<T, bool>> Criteria { get; set ; }
        #region comment
        //Criteria --p.id==id الى هيا نتيجه ال bool ويرجع P او Product الى هيا عندى هنا مثلا T بياخد دليجيت بياخد ال Expression ف بالتالى لازم يكون نوعها lamda expression عشان دى هتشيل ال
        #endregion
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
        #region comments
        //referenceيعتبر Includes مش هيضيف لان الinitial value عشان لم اجى اضيف جواه يضيف لانى لو مديتوش  null هنا نا اديتو قيمه مبدأيه عشان متبقاش ب 
        // Includes --Include لان لو عندى مثلا اكتر من List يعنى وطبعا دى ممكن تاخد Includes لو فيه  Includes عشان دى هتشيل ال
        //بتاعتو navigation property وبيرجع ال Product وبياخد ديليجيت والديليجيت دا بيستقبل مثلا  Expression ف المفروض ان هيبقا نوعها طبعا
        //ف كدا هيرجع اوبجيكت Id,name دى المفروض انها عباره عن اوبجيكت مكون من navigation property وال 
        #endregion

        public BaseSpecification(Expression<Func<T, bool>> Criteria)
        {
            this.Criteria = Criteria;
        }

        public void AddInclude(Expression<Func<T, object>> Include)
        {
            Includes.Add(Include);
        }

        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        public void AddOrderByDescending(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDescending = orderByDesc;
        }

        public void ApplyPaginaton(int skip , int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
