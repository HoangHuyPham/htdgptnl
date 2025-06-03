using be.Helpers;

namespace be.Repos.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> FindById(Guid id);
        Task<ApiPaginationResponse<List<T>>> FindAll(PaginationQuery query);
        Task<T?> Create(T target);
        Task<T?> Update(T data);
        Task<bool> Delete(Guid id);
    }
}
