using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.DAL.Entities.Order_Aggregate
{
    public class Address
    {
        public Address()
        {
            #region comments
            //ف انا لما عملت الكونستركتور الى تحتى دا..راح دا اتلغى empty parameterless constructorبعتمد على ال entity frameworkدا انا اعمله لان ال
            //متطلعش ايرور EF ف عشان كدا انا كتبتو بايدي عشان ال
            #endregion
        }
        public Address(string firstName, string lastName, string country, string city, string street)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Street = street;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
