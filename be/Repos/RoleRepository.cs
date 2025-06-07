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
    public class RoleRepository(ApplicationDbContext _dbContext) : IRepository<Role>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Role?> Create(Role target)
        {
            try
            {
                await dbContext.Roles.AddAsync(target);
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
                var existRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
                if (existRole != null)
                {
                    dbContext.Roles.Remove(existRole);
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

        public async Task<ApiPaginationResponse<List<Role>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryRoles = dbContext.Roles.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryRoles = queryRoles.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryRoles = queryRoles.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryRoles.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryRoles = queryRoles.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryRoles.ToListAsync(),
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

        public async Task<Role?> FindById(Guid id)
        {
            try
            {
                var existRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
                if (existRole != null)
                    return existRole;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Role?> Update(Role data)
        {
            dbContext.Roles.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}