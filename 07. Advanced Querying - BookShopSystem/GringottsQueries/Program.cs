using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottsQueries
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new GringottsContext();
            //========== 19.	Deposits Sum for Ollivander Family ==========\\
            //DepositSumForOlivanderFaamily(ctx);

            //========== 20.	Deposits Filter ==========\\
            DepositFilter(ctx);

        }

        private static void DepositFilter(GringottsContext ctx)
        {
            const decimal lowestAmount = 150000m;
            Dictionary<string, decimal?> filteredDepositGroups = new Dictionary<string, decimal?>();
            foreach (var gr in ctx.WizzardDeposits.Select(x => x.DepositGroup).Distinct())
            {
                filteredDepositGroups[gr] = ctx.WizzardDeposits
                    .Where(x => x.DepositGroup == gr)
                    .Where(w => w.MagicWandCreator == "Ollivander family")
                    .Sum(x => x.DepositAmount);
            }

            foreach (var filteredDepositGroup in filteredDepositGroups.Where(g => g.Value > lowestAmount).OrderByDescending(g => g.Value))
            {
                Console.WriteLine($"{filteredDepositGroup.Key} - {filteredDepositGroup.Value}");
            }
        }

        private static void DepositSumForOlivanderFaamily(GringottsContext ctx)
        {
            var depositGroups = ctx.WizzardDeposits.Select(x => x.DepositGroup).Distinct();
            foreach (var g in depositGroups)
            {
                Console.WriteLine($@"{g} - {ctx.WizzardDeposits
                    .Where(x => x.DepositGroup == g)
                    .Where(w => w.MagicWandCreator == "Ollivander family")
                    .Sum(x => x.DepositAmount)}");
            }
        }
    }
}
