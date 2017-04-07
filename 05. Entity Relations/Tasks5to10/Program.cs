using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks5to10.Data;
using Tasks5to10.Models;

namespace Tasks5to10
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new PhotographerContext();
            ctx.Database.Initialize(true);
        }
    }
}
