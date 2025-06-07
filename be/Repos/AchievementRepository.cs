using be.Contexts;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class AchievementRepository(ApplicationDbContext _dbContext) : IRepository<Achievement>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Achievement?> Create(Achievement target)
        {
            try
            {
                await dbContext.Achievements.AddAsync(target);
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
                var existAchievement = await dbContext.Achievements.FirstOrDefaultAsync(x => x.Id == id);
                if (existAchievement != null)
                {
                    dbContext.Achievements.Remove(existAchievement);
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

        public async Task<ApiPaginationResponse<List<Achievement>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryAchievements = dbContext.Achievements.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryAchievements = queryAchievements.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryAchievements = queryAchievements.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryAchievements.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryAchievements = queryAchievements.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryAchievements.ToListAsync(),
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

        public async Task<Achievement?> FindById(Guid id)
        {
            try
            {
                var existAchievement = await dbContext.Achievements.FirstOrDefaultAsync(x => x.Id == id);
                if (existAchievement != null)
                    return existAchievement;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Achievement?> Update(Achievement data)
        {
            dbContext.Achievements.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}