﻿namespace _3.Sales_Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StoreLocation
    {
        public int Id { get; set; }

        public string LocationName { get; set; }

        public List<Sale> SalesInStore { get; set; }
    }
}
