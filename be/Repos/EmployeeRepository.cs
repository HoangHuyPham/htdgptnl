using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Contexts;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class EmployeeRepository(ApplicationDbContext _dbContext) : IRepository<Employee>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Employee?> Create(Employee target)
        {
            try
            {
                await dbContext.Employees.AddAsync(target);
                await dbContext.SaveChangesAsync();
                return target;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var existEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (existEmployee != null)
                {
                    dbContext.Employees.Remove(existEmployee);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<ApiPaginationResponse<List<Employee>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryEmployees = dbContext.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryEmployees = queryEmployees.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryEmployees = queryEmployees.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryEmployees.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryEmployees = queryEmployees.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryEmployees.ToListAsync(),
                    Message = "success",
                    Pagination = new()
                    {
                        Total = total,
                        Limit = query.Limit,
                        Page = query.Page,
                        Sort = query.Sort,
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new()
            {
                Data = [],
                Message = "success",
                Pagination = new()
                {
                    Total = 0,
                    Limit = query.Limit,
                    Page = query.Page,
                    Sort = query.Sort,
                }
            };
        }

        public async Task<Employee?> FindById(Guid id)
        {
            try
            {
                var existEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (existEmployee != null)
                    return existEmployee;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Employee?> Update(Employee data)
        {
            dbContext.Employees.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}