using be.Contexts;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Repos
{
    public class ImageRepository(ApplicationDbContext _dbContext) : IRepository<Image>
    {
        private readonly ApplicationDbContext dbContext = _dbContext;
        public async Task<Image?> Create(Image target)
        {
            try
            {
                await dbContext.Images.AddAsync(target);
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
                var existImage = await dbContext.Images.FirstOrDefaultAsync(x => x.Id == id);
                if (existImage != null)
                {
                    dbContext.Images.Remove(existImage);
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

        public async Task<ApiPaginationResponse<List<Image>>> FindAll(PaginationQuery query)
        {
            try
            {
                var queryImages = dbContext.Images.AsQueryable();

                if (!string.IsNullOrEmpty(query.Sort))
                {
                    var sortPaths = query.Sort.Split(':');

                    if (sortPaths[1] == "desc")
                    {
                        queryImages = queryImages.OrderByDescending(x => EF.Property<object>(x, sortPaths[0]));
                    }
                    else
                    {
                        queryImages = queryImages.OrderBy(x => EF.Property<object>(x, sortPaths[0]));
                    }
                }

                var total = await queryImages.CountAsync();

                if (query.Page > 0 && query.Limit > 0)
                {
                    int skip = (query.Page - 1) * query.Limit;
                    queryImages = queryImages.Skip(skip).Take(query.Limit);
                }
                return new()
                {
                    Data = await queryImages.ToListAsync(),
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

        public async Task<Image?> FindById(Guid id)
        {
            try
            {
                var existImage = await dbContext.Images.FirstOrDefaultAsync(x => x.Id == id);
                if (existImage != null)
                    return existImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Image?> Update(Image data)
        {
            dbContext.Images.Update(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
    }
}