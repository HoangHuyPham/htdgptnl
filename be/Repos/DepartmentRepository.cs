using be.Contexts;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class DepartmentRepository(ApplicationDbContext _dbContext) : IRepository<Department>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Department?> Create(Department target)
        {
            try
            {
                await dbContext.Departments.AddAsync(target);
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
                var existDepartment = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
                if (existDepartment != null)
                {
                    dbContext.Departments.Remove(existDepartment);
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

        public async Task<ApiPaginationResponse<List<Department>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryDepartments = dbContext.Departments.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryDepartments = queryDepartments.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryDepartments = queryDepartments.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryDepartments.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryDepartments = queryDepartments.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryDepartments.ToListAsync(),
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

        public async Task<Department?> FindById(Guid id)
        {
            try
            {
                var existDepartment = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
                if (existDepartment != null)
                    return existDepartment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Department?> Update(Department data)
        {
            dbContext.Departments.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}