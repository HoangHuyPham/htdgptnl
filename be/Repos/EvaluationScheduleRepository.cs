using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Contexts;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using be.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class EvaluationScheduleRepository(ApplicationDbContext _dbContext) : IEvaluationScheduleRepository
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<EvaluationSchedule?> Create(EvaluationSchedule target)
        {
            try
            {
                await dbContext.EvaluationSchedules.AddAsync(target);
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
                var existEvaluationSchedule = await dbContext.EvaluationSchedules.FirstOrDefaultAsync(x => x.Id == id);
                if (existEvaluationSchedule != null)
                {
                    dbContext.EvaluationSchedules.Remove(existEvaluationSchedule);
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

        public async Task<ApiPaginationResponse<List<EvaluationSchedule>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryEvaluationSchedules = dbContext.EvaluationSchedules.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryEvaluationSchedules = queryEvaluationSchedules.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryEvaluationSchedules = queryEvaluationSchedules.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryEvaluationSchedules.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryEvaluationSchedules = queryEvaluationSchedules.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryEvaluationSchedules.ToListAsync(),
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

        public async Task<List<EvaluationSchedule>> FindAllAvailable()
        {
            return await dbContext.EvaluationSchedules.Where(x => (x.End > DateTimeOffset.UtcNow.ToUnixTimeSeconds()) && (x.Start < DateTimeOffset.UtcNow.ToUnixTimeSeconds()))
            .Include(x => x.PerformanceEvaluation!)
            .ThenInclude(x => x.Achievements)
            .ThenInclude(x => x.AchievementItems)
            .ThenInclude(x => x.Criterias)
            .ToListAsync();
        }

        public async Task<EvaluationSchedule?> FindById(Guid id)
        {
            try
            {
                var existEvaluationSchedule = await dbContext.EvaluationSchedules.FirstOrDefaultAsync(x => x.Id == id);
                if (existEvaluationSchedule != null)
                    return existEvaluationSchedule;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<EvaluationSchedule?> Update(EvaluationSchedule data)
        {
            dbContext.EvaluationSchedules.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}