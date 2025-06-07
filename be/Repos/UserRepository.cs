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
    public class UserRepository(ApplicationDbContext _dbContext) : IUserRepository
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<User?> Create(User target)
        {
            try
            {
                await dbContext.Users.AddAsync(target);
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
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var existUser = await dbContext.Users.Include(x=>x.Role).FirstOrDefaultAsync(x => x.Id == id);
                if (existUser != null)
                {
                    // Phải xóa thế này vì sql server không thể cascade 2 cùng lúc fk được
                    var scoresToDelete = await dbContext.EvaluationScores
                    .Where(x => x.TargetId == existUser.Id)
                    .ToListAsync();

                    dbContext.EvaluationScores.RemoveRange(scoresToDelete);
                    dbContext.Users.Remove(existUser);
                    await dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<ApiPaginationResponse<List<User>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryUsers = dbContext.Users.Include(x=>x.Role).AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryUsers = queryUsers.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryUsers = queryUsers.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryUsers.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryUsers = queryUsers.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryUsers.ToListAsync(),
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

        public async Task<User?> FindById(Guid id)
        {
            try
            {
                var existUser = await dbContext.Users
                .Include(x => x.Role)
                .Include(x => x.Employee!).ThenInclude(x => x.Employees)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!)

                .Include(x => x.Employee!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Grade)
                .Include(x => x.Employee!).ThenInclude(x => x.Detail!).ThenInclude(x => x.PositionE)
                .Include(x => x.Employee!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Plant)
                .Include(x => x.Employee!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Department)
                .Include(x => x.Employee!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Process)
                .Include(x => x.Employee!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Operation)
                .Include(x => x.Employee!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Group)
                .Include(x => x.Employee!).ThenInclude(x => x.Detail!).ThenInclude(x => x.WorkingDetail)

                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Grade)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.PositionE)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Plant)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Department)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Process)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Operation)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Group)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.WorkingDetail)

                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Grade)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.PositionE)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Plant)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Department)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Process)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Operation)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.Group)
                .Include(x => x.Employee!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Supervisor!).ThenInclude(x => x.Detail!).ThenInclude(x => x.WorkingDetail)
                
                .FirstOrDefaultAsync(x => x.Id == id);
                if (existUser != null)
                    return existUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<User?> FindByUserName(string username)
        {
            try
            {
                var existUser = await dbContext.Users.Include(x=>x.Role).FirstOrDefaultAsync(x => x.UserName == username);
                if (existUser != null)
                    return existUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<User?> Update(User data)
        {
            dbContext.Users.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}