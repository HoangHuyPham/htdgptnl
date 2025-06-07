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
    public class OperationRepository(ApplicationDbContext _dbContext) : IRepository<Operation>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Operation?> Create(Operation target)
        {
            try
            {
                await dbContext.Operations.AddAsync(target);
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
                var existOperation = await dbContext.Operations.FirstOrDefaultAsync(x => x.Id == id);
                if (existOperation != null)
                {
                    dbContext.Operations.Remove(existOperation);
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

        public async Task<ApiPaginationResponse<List<Operation>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryOperations = dbContext.Operations.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryOperations = queryOperations.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryOperations = queryOperations.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryOperations.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryOperations = queryOperations.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryOperations.ToListAsync(),
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

        public async Task<Operation?> FindById(Guid id)
        {
            try
            {
                var existOperation = await dbContext.Operations.FirstOrDefaultAsync(x => x.Id == id);
                if (existOperation != null)
                    return existOperation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Operation?> Update(Operation data)
        {
            dbContext.Operations.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}