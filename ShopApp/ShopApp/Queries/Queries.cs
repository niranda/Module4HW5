using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;

namespace ShopApp.Queries
{
    public class Queries
    {
        private readonly ApplicationContext _context;

        public Queries(ApplicationContext context)
        {
            _context = context;
        }

        public async Task FirstQuery()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var data = from office in _context.Offices
                               join employee in _context.Employees on office.OfficeId equals employee.OfficeId into smth
                               from employee in smth.DefaultIfEmpty()
                               select new
                               {
                                   HiredDate = employee.HiredDate,
                                   TitleId = employee.TitleId,
                                   OfficeLocation = office.Location,
                               };

                    var res = from someData in data
                              join title in _context.Titles on someData.TitleId equals title.TitleId
                              select new
                              {
                                  CompanyName = title.Name,
                                  OfficeLocation = someData.OfficeLocation,
                                  HiredDate = someData.HiredDate,
                              };

                    foreach (var i in res)
                    {
                        Console.WriteLine($"{i.CompanyName} {i.OfficeLocation} {i.HiredDate}");
                    }

                    Console.WriteLine("1 query done. Left Join");

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e}");
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task SecondQuery()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    DbFunctions dbFunctions = null;
                    var data = await _context.Employees
                        .Select(x => SqlServerDbFunctionsExtensions.DateDiffDay(dbFunctions, x.HiredDate, DateTime.UtcNow))
                        .ToListAsync();

                    foreach (var item in data)
                    {
                        Console.WriteLine($"{item} days have passed");
                    }

                    Console.WriteLine("2 query done. Time");

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e}");
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task ThirdQuery()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var firstData = await _context.Projects
                        .FirstOrDefaultAsync(item => item.Name.Contains("a"));

                    var secondData = _context.Offices
                        .FirstOrDefault(item => item.Title.StartsWith("D"));

                    if (firstData != null && secondData != null)
                    {
                        firstData.Name = "My Own Company";
                        secondData.Title = "MAIN OFFICE";
                        _context.Projects.Update(firstData);
                        _context.Offices.Update(secondData);
                    }

                    Console.WriteLine("3 query done. Entities updated");

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e}");
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task FourthQuery()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.AddAsync(new Employee()
                    {
                        FirstName = "Alex",
                        LastName = "Brown",
                        HiredDate = new DateTime(2015, 7, 20),
                        Title = new Title() { Name = "Some title" },
                        Office = new Office() { Location = "London", Title = "Big One" }
                    });

                    Console.WriteLine("4 query done. Entities added");

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e}");
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task FifthQuery()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var entityOnDelete = await _context.Employees
                        .FirstOrDefaultAsync(item => item.LastName == "Brown");

                    if (entityOnDelete != null)
                    {
                        _context.Remove(entityOnDelete);
                    }

                    Console.WriteLine("5 query done. Entities removed");

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e}");
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task SixthQuery()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var entity = _context.Employees
                        .AsEnumerable()
                        .GroupBy(item => item.TitleId)
                        .Select(item => _context.Titles.Where(i => i.TitleId == item.Key).Select(i => i.Name).ToList());

                    foreach (var item in entity)
                    {
                        foreach (var i in item)
                        {
                            Console.WriteLine($"{i}");
                        }
                    }

                    Console.WriteLine("6 query done. Titles downloaded");

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e}");
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
