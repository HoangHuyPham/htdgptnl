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
    public class PerformanceEvaluationRepository(ApplicationDbContext _dbContext) : IRepository<PerformanceEvaluation>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<PerformanceEvaluation?> Create(PerformanceEvaluation target)
        {
            try
            {
                await dbContext.PerformanceEvaluations.AddAsync(target);
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
                var existPerformanceEvaluation = await dbContext.PerformanceEvaluations.FirstOrDefaultAsync(x => x.Id == id);
                if (existPerformanceEvaluation != null)
                {
                    dbContext.PerformanceEvaluations.Remove(existPerformanceEvaluation);
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

        public async Task<ApiPaginationResponse<List<PerformanceEvaluation>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryPerformanceEvaluations = dbContext.PerformanceEvaluations.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryPerformanceEvaluations = queryPerformanceEvaluations.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryPerformanceEvaluations = queryPerformanceEvaluations.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryPerformanceEvaluations.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryPerformanceEvaluations = queryPerformanceEvaluations.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryPerformanceEvaluations.ToListAsync(),
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

        public async Task<PerformanceEvaluation?> FindById(Guid id)
        {
            try
            {
                var existPerformanceEvaluation = await dbContext.PerformanceEvaluations.FirstOrDefaultAsync(x => x.Id == id);
                if (existPerformanceEvaluation != null)
                    return existPerformanceEvaluation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<PerformanceEvaluation?> Update(PerformanceEvaluation data)
        {
            dbContext.PerformanceEvaluations.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}