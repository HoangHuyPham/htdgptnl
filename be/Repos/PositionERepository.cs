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
    public class PositionERepository(ApplicationDbContext _dbContext) : IRepository<PositionE>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<PositionE?> Create(PositionE target)
        {
            try
            {
                await dbContext.PositionEs.AddAsync(target);
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
                var existPositionE = await dbContext.PositionEs.FirstOrDefaultAsync(x => x.Id == id);
                if (existPositionE != null)
                {
                    dbContext.PositionEs.Remove(existPositionE);
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

        public async Task<ApiPaginationResponse<List<PositionE>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryPositionEs = dbContext.PositionEs.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryPositionEs = queryPositionEs.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryPositionEs = queryPositionEs.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryPositionEs.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryPositionEs = queryPositionEs.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryPositionEs.ToListAsync(),
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

        public async Task<PositionE?> FindById(Guid id)
        {
            try
            {
                var existPositionE = await dbContext.PositionEs.FirstOrDefaultAsync(x => x.Id == id);
                if (existPositionE != null)
                    return existPositionE;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<PositionE?> Update(PositionE data)
        {
            dbContext.PositionEs.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}