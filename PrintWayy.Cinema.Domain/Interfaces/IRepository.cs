namespace PrintWayy.Cinema.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity data);
        IEnumerable<TEntity> All();
        TEntity FindById(Guid id);
        void Update(TEntity entity);
        bool Delete(Guid id);
    }
}
