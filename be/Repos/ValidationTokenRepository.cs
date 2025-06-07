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
    public class ValidationTokenRepository(ApplicationDbContext _dbContext) : IRepository<ValidationToken>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<ValidationToken?> Create(ValidationToken target)
        {
            try
            {
                await dbContext.ValidationTokens.AddAsync(target);
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
                var existValidationToken = await dbContext.ValidationTokens.FirstOrDefaultAsync(x => x.Id == id);
                if (existValidationToken != null)
                {
                    dbContext.ValidationTokens.Remove(existValidationToken);
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

        public async Task<ApiPaginationResponse<List<ValidationToken>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryValidationTokens = dbContext.ValidationTokens.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryValidationTokens = queryValidationTokens.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryValidationTokens = queryValidationTokens.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryValidationTokens.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryValidationTokens = queryValidationTokens.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryValidationTokens.ToListAsync(),
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

        public async Task<ValidationToken?> FindById(Guid id)
        {
            try
            {
                var existValidationToken = await dbContext.ValidationTokens.FirstOrDefaultAsync(x => x.Id == id);
                if (existValidationToken != null)
                    return existValidationToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<ValidationToken?> Update(ValidationToken data)
        {
            dbContext.ValidationTokens.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}