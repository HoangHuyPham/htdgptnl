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
    public class GroupRepository(ApplicationDbContext _dbContext) : IRepository<Group>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Group?> Create(Group target)
        {
            try
            {
                await dbContext.Groups.AddAsync(target);
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
                var existGroup = await dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);
                if (existGroup != null)
                {
                    dbContext.Groups.Remove(existGroup);
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

        public async Task<ApiPaginationResponse<List<Group>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryGroups = dbContext.Groups.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryGroups = queryGroups.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryGroups = queryGroups.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryGroups.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryGroups = queryGroups.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryGroups.ToListAsync(),
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

        public async Task<Group?> FindById(Guid id)
        {
            try
            {
                var existGroup = await dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);
                if (existGroup != null)
                    return existGroup;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Group?> Update(Group data)
        {
            dbContext.Groups.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}