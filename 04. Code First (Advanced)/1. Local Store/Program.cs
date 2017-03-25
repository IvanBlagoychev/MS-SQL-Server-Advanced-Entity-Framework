using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Local_Store
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new GickaContext();
            //context.Database.Initialize(true);

            using (context)
            {
                Product liutenica = new Product()
                {
                    name = "Liutenica",
                    distributorName = "Deroni EOOD",
                    description = "90% tikveno piure i 10% domaten sos.",
                    price = 3.00m
                };
                context.Products.Add(liutenica);

                Product bonboni = new Product()
                {
                    name = "Milka",
                    distributorName = "Distribucii EOOD",
                    description = "Bonboni Milka s fin mlechen vkus.",
                    price = 4.50m
                };
                context.Products.Add(bonboni);

                Product hlqb = new Product()
                {
                    name = "Dobrudja",
                    distributorName = "Hlqb OOD",
                    description = "",
                    price = 0.80m
                };
                context.Products.Add(hlqb);
                context.SaveChanges();
            }

            
        }
    }
}
