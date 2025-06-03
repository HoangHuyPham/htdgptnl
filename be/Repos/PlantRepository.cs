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
    public class PlantRepository(ApplicationDbContext _dbContext) : IRepository<Plant>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Plant?> Create(Plant target)
        {
            try
            {
                await dbContext.Plants.AddAsync(target);
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
                var existPlant = await dbContext.Plants.FirstOrDefaultAsync(x => x.Id == id);
                if (existPlant != null)
                {
                    dbContext.Plants.Remove(existPlant);
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

        public async Task<ApiPaginationResponse<List<Plant>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryPlants = dbContext.Plants.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryPlants = queryPlants.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryPlants = queryPlants.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryPlants.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryPlants = queryPlants.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryPlants.ToListAsync(),
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

        public async Task<Plant?> FindById(Guid id)
        {
            try
            {
                var existPlant = await dbContext.Plants.FirstOrDefaultAsync(x => x.Id == id);
                if (existPlant != null)
                    return existPlant;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Plant?> Update(Plant data)
        {
            dbContext.Plants.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}