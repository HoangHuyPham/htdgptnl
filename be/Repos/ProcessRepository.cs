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
    public class ProcessRepository(ApplicationDbContext _dbContext) : IRepository<Process>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Process?> Create(Process target)
        {
            try
            {
                await dbContext.Processs.AddAsync(target);
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
                var existProcess = await dbContext.Processs.FirstOrDefaultAsync(x => x.Id == id);
                if (existProcess != null)
                {
                    dbContext.Processs.Remove(existProcess);
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

        public async Task<ApiPaginationResponse<List<Process>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryProcesss = dbContext.Processs.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryProcesss = queryProcesss.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryProcesss = queryProcesss.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryProcesss.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryProcesss = queryProcesss.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryProcesss.ToListAsync(),
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

        public async Task<Process?> FindById(Guid id)
        {
            try
            {
                var existProcess = await dbContext.Processs.FirstOrDefaultAsync(x => x.Id == id);
                if (existProcess != null)
                    return existProcess;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Process?> Update(Process data)
        {
            dbContext.Processs.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}