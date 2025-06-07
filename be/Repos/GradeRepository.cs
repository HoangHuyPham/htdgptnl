using be.Contexts;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class GradeRepository(ApplicationDbContext _dbContext) : IRepository<Grade>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Grade?> Create(Grade target)
        {
            try
            {
                await dbContext.Grades.AddAsync(target);
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
                var existGrade = await dbContext.Grades.FirstOrDefaultAsync(x => x.Id == id);
                if (existGrade != null)
                {
                    dbContext.Grades.Remove(existGrade);
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

        public async Task<ApiPaginationResponse<List<Grade>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryGrades = dbContext.Grades.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryGrades = queryGrades.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryGrades = queryGrades.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryGrades.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryGrades = queryGrades.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryGrades.ToListAsync(),
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

        public async Task<Grade?> FindById(Guid id)
        {
            try
            {
                var existGrade = await dbContext.Grades.FirstOrDefaultAsync(x => x.Id == id);
                if (existGrade != null)
                    return existGrade;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Grade?> Update(Grade data)
        {
            dbContext.Grades.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}