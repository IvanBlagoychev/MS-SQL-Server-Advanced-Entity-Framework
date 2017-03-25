using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Local_Store_Improvement
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ProductsNewContext();
            //context.Database.Initialize(true);
            Product liutenica = new Product("Liutenica", "Deroni EOOD", "90% tikveno piure i 10% domaten sos.", 3.00m, 3, 2.00);
            Product bonboni = new Product("Milka", "Distribucii EOOD", "Bonboni Milka s fin mlechen vkus.", 4.50m, 10, 3.50);   
            Product hlqb = new Product("Dobrudja", "Hlqb OOD", "Bql hlqb", 0.80m, 8, 3.00);
            context.ProductsImproved.Add(liutenica);
            context.ProductsImproved.Add(bonboni);
            context.ProductsImproved.Add(hlqb);
            context.SaveChanges();
        }
    }
}
