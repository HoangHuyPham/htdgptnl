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
    public class EvaluationScoreRepository(ApplicationDbContext _dbContext) : IEvaluationScoreRepository
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<EvaluationScore?> Create(EvaluationScore target)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var newSource = dbContext.Users.Include(x => x.Role).FirstOrDefault(x => x.Id == target.SourceId);
                var newTarget = dbContext.Users.Include(x => x.Role).FirstOrDefault(x => x.Id == target.TargetId);

                // check level
                if (newSource?.Role?.Level < newTarget?.Role?.Level)
                {
                    return null;
                }

                // check performance evaluation from criteria
                var newCriteria = await dbContext.Criterias
                .Include(x => x.AchievementItem!)
                .ThenInclude(x => x.Achievement!)
                .ThenInclude(x => x.PerformanceEvaluation).FirstOrDefaultAsync();

                var performanceEvaluationId = newCriteria?.AchievementItem?.Achievement?.PerformanceEvaluation?.Id;

                // check schedule source (time + same performance evaluation)
                var sourceSchedule = await dbContext.EvaluationSchedules
                .Where(x => (x.PerformanceEvaluationId == performanceEvaluationId) && (x.RoleId == newSource!.RoleId) && (x.End > DateTimeOffset.UtcNow.ToUnixTimeSeconds()))
                .FirstOrDefaultAsync();

                // check schedule target (same performance evaluation)
                var targetSchedule = await dbContext.EvaluationSchedules
                .Where(x => (x.PerformanceEvaluationId == performanceEvaluationId) && (x.RoleId == newTarget!.RoleId))
                .FirstOrDefaultAsync();

                // check valid score
                var isValidScore = target.Score >= newCriteria?.AchievementItem?.Threshold &&
                                    target.Score <= newCriteria?.AchievementItem?.Stretch;

                if (sourceSchedule != null && targetSchedule != null && isValidScore)
                {
                    await dbContext.EvaluationScores.AddAsync(target);
                    await transaction.CommitAsync();
                    await dbContext.SaveChangesAsync();
                    return target;
                }
                else
                {
                    Console.WriteLine("Rollback!");
                    await transaction.RollbackAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Rollback!");
                await transaction.RollbackAsync();
            }
            return null;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var existEvaluationScore = await dbContext.EvaluationScores.FirstOrDefaultAsync(x => x.Id == id);
                if (existEvaluationScore != null)
                {
                    dbContext.EvaluationScores.Remove(existEvaluationScore);
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

        public async Task<ApiPaginationResponse<List<EvaluationScore>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryEvaluationScores = dbContext.EvaluationScores.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryEvaluationScores = queryEvaluationScores.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryEvaluationScores = queryEvaluationScores.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryEvaluationScores.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryEvaluationScores = queryEvaluationScores.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryEvaluationScores.ToListAsync(),
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

        public async Task<List<EvaluationScore>> FindAllBy(Guid? sourceId, Guid? targetId, Guid? criteriaId)
        {
            var queryEvaluationScores = dbContext.EvaluationScores.AsQueryable();
            if (criteriaId != null)
            {
                queryEvaluationScores = queryEvaluationScores.Where(x => x.CriteriaId == criteriaId);
            }

            if (sourceId != null)
            {
                queryEvaluationScores = queryEvaluationScores.Where(x => (x.SourceId == sourceId) || (x.TargetId == targetId));
            }

            return await queryEvaluationScores.ToListAsync();
        }

        public async Task<EvaluationScore?> FindById(Guid id)
        {
            try
            {
                var existEvaluationScore = await dbContext.EvaluationScores.FirstOrDefaultAsync(x => x.Id == id);
                if (existEvaluationScore != null)
                    return existEvaluationScore;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<EvaluationScore?> Update(EvaluationScore data)
        {
            dbContext.EvaluationScores.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}