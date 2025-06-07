using be.Contexts;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class CriteriaRepository(ApplicationDbContext _dbContext) : IRepository<Criteria>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Criteria?> Create(Criteria target)
        {
            try
            {
                await dbContext.Criterias.AddAsync(target);
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
                var existCriteria = await dbContext.Criterias.FirstOrDefaultAsync(x => x.Id == id);
                if (existCriteria != null)
                {
                    dbContext.Criterias.Remove(existCriteria);
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

        public async Task<ApiPaginationResponse<List<Criteria>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryCriterias = dbContext.Criterias.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryCriterias = queryCriterias.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryCriterias = queryCriterias.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryCriterias.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryCriterias = queryCriterias.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryCriterias.ToListAsync(),
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

        public async Task<Criteria?> FindById(Guid id)
        {
            try
            {
                var existCriteria = await dbContext.Criterias.FirstOrDefaultAsync(x => x.Id == id);
                if (existCriteria != null)
                    return existCriteria;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Criteria?> Update(Criteria data)
        {
            dbContext.Criterias.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}