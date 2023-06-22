using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.DAL.Entities
{
    public class CustomerBasket
    {
        #region Comments
        //int نوعه id الى فيه ال BaseEntity ومليتوش يورث ليه عل طول من ال string نوعه  id انا هنا ليه عامل ال 
        // string فيها لازم يتخزن من نوع Key ف ال Key => value ودى هبقا عباره عن  redis db لان انا هخزن الداتا الى هنا فى ال
        //string نوعه  id فعشان كدا انا عملت ال
        #endregion
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public CustomerBasket()
        {

        }
        public CustomerBasket(string basketId)
        {
            Id = basketId;
        }
    }
}
