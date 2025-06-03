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
    public class EvidenceRepository(ApplicationDbContext _dbContext) : IRepository<Evidence>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Evidence?> Create(Evidence target)
        {
            try
            {
                await dbContext.Evidences.AddAsync(target);
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
                var existEvidence = await dbContext.Evidences.FirstOrDefaultAsync(x => x.Id == id);
                if (existEvidence != null)
                {
                    dbContext.Evidences.Remove(existEvidence);
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

        public async Task<ApiPaginationResponse<List<Evidence>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryEvidences = dbContext.Evidences.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryEvidences = queryEvidences.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryEvidences = queryEvidences.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryEvidences.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryEvidences = queryEvidences.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryEvidences.ToListAsync(),
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

        public async Task<Evidence?> FindById(Guid id)
        {
            try
            {
                var existEvidence = await dbContext.Evidences.FirstOrDefaultAsync(x => x.Id == id);
                if (existEvidence != null)
                    return existEvidence;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Evidence?> Update(Evidence data)
        {
            dbContext.Evidences.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}