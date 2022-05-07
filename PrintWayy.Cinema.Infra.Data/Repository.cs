using LiteDB;
using PrintWayy.Cinema.Domain.Interfaces;

namespace PrintWayy.Cinema.Infra.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public ILiteDatabase DB { get; }
        public ILiteCollection<TEntity> Collection { get; }

        protected Repository(ILiteDatabase db)
        {
            DB = db;
            Collection = db.GetCollection<TEntity>();
        }

        public virtual TEntity Create(TEntity entity)
        {
            var newId = Collection.Insert(entity);
            return Collection.FindById(newId.AsGuid);
        }

        public virtual IEnumerable<TEntity> All()
        {
            return Collection.FindAll();
        }

        public virtual TEntity FindById(Guid id)
        {
            return Collection.FindById(id);
        }

        public virtual void Update(TEntity entity)
        {
            Collection.Upsert(entity);
        }

        public virtual bool Delete(Guid id)
        {
            return Collection.Delete(id);
        }
    }
}
