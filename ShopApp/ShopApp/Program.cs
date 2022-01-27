using System;
using System.Threading.Tasks;
using ShopApp.Queries;

namespace ShopApp.Main
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await using (var context = new ContextFactory().CreateDbContext(args))
            {
                await new Queries.Queries(context).FirstQuery();
                await new Queries.Queries(context).SecondQuery();
                await new Queries.Queries(context).ThirdQuery();
                await new Queries.Queries(context).FourthQuery();
                await new Queries.Queries(context).FifthQuery();
                await new Queries.Queries(context).SixthQuery();
            }

            Console.ReadKey();
        }
    }
}
