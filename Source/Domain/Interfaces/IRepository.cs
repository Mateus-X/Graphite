namespace Graphite.Source.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByIdAsync(string id);
        Task<T?> GetByIdAsync(int id);
    }

}
