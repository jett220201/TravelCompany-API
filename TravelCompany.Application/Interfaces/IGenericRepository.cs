namespace TravelCompany.Application.Interface
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<int> Create(TEntity entity);
        Task<TEntity> AddReturn(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entity);
        Task<int> Update(TEntity entity);
        Task<TEntity> UpdateReturn(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
    }
}
