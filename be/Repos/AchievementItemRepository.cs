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
    public class AchievementItemRepository(ApplicationDbContext _dbContext) : IRepository<AchievementItem>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<AchievementItem?> Create(AchievementItem target)
        {
            try
            {
                await dbContext.AchievementItems.AddAsync(target);
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
                var existAchievementItem = await dbContext.AchievementItems.FirstOrDefaultAsync(x => x.Id == id);
                if (existAchievementItem != null)
                {
                    dbContext.AchievementItems.Remove(existAchievementItem);
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

        public async Task<ApiPaginationResponse<List<AchievementItem>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryAchievementItems = dbContext.AchievementItems.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryAchievementItems = queryAchievementItems.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryAchievementItems = queryAchievementItems.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryAchievementItems.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryAchievementItems = queryAchievementItems.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryAchievementItems.ToListAsync(),
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

        public async Task<AchievementItem?> FindById(Guid id)
        {
            try
            {
                var existAchievementItem = await dbContext.AchievementItems.FirstOrDefaultAsync(x => x.Id == id);
                if (existAchievementItem != null)
                    return existAchievementItem;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<AchievementItem?> Update(AchievementItem data)
        {
            dbContext.AchievementItems.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}