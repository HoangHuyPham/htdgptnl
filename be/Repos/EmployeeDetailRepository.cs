using be.Contexts;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class EmployeeDetailRepository(ApplicationDbContext _dbContext) : IRepository<EmployeeDetail>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<EmployeeDetail?> Create(EmployeeDetail target)
        {
            try
            {
                await dbContext.EmployeeDetails.AddAsync(target);
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
                var existEmployeeDetail = await dbContext.EmployeeDetails.FirstOrDefaultAsync(x => x.Id == id);
                if (existEmployeeDetail != null)
                {
                    dbContext.EmployeeDetails.Remove(existEmployeeDetail);
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

        public async Task<ApiPaginationResponse<List<EmployeeDetail>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryEmployeeDetails = dbContext.EmployeeDetails.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryEmployeeDetails = queryEmployeeDetails.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryEmployeeDetails = queryEmployeeDetails.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryEmployeeDetails.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryEmployeeDetails = queryEmployeeDetails.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryEmployeeDetails.ToListAsync(),
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

        public async Task<EmployeeDetail?> FindById(Guid id)
        {
            try
            {
                var existEmployeeDetail = await dbContext.EmployeeDetails.FirstOrDefaultAsync(x => x.Id == id);
                if (existEmployeeDetail != null)
                    return existEmployeeDetail;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<EmployeeDetail?> Update(EmployeeDetail data)
        {
            dbContext.EmployeeDetails.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}