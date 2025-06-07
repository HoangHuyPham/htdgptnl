using be.Contexts;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class WorkingDetailRepository(ApplicationDbContext _dbContext) : IRepository<WorkingDetail>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<WorkingDetail?> Create(WorkingDetail target)
        {
            try
            {
                await dbContext.WorkingDetails.AddAsync(target);
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
                var existWorkingDetail = await dbContext.WorkingDetails.FirstOrDefaultAsync(x => x.Id == id);
                if (existWorkingDetail != null)
                {
                    dbContext.WorkingDetails.Remove(existWorkingDetail);
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

        public async Task<ApiPaginationResponse<List<WorkingDetail>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryWorkingDetails = dbContext.WorkingDetails.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryWorkingDetails = queryWorkingDetails.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryWorkingDetails = queryWorkingDetails.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryWorkingDetails.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryWorkingDetails = queryWorkingDetails.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryWorkingDetails.ToListAsync(),
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

        public async Task<WorkingDetail?> FindById(Guid id)
        {
            try
            {
                var existWorkingDetail = await dbContext.WorkingDetails.FirstOrDefaultAsync(x => x.Id == id);
                if (existWorkingDetail != null)
                    return existWorkingDetail;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<WorkingDetail?> Update(WorkingDetail data)
        {
            dbContext.WorkingDetails.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}