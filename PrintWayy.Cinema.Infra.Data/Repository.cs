using LiteDB;
using Microsoft.Extensions.Configuration;
using PrintWayy.Cinema.Domain.Interfaces;

namespace PrintWayy.Cinema.Infra.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public ILiteDatabase DB { get; }
        public ILiteCollection<TEntity> Collection { get; }
        protected Repository(IConfiguration? configuration)
        {
            var pass = configuration?["LiteDB:RepositoryKey"];
            if(pass == null)
                DB = new LiteDatabase(":memory:");
            else
            {
                var connectionString = new ConnectionString
                {
                    Filename = "cinema.db",
                    Connection = ConnectionType.Shared,
                    Password = pass,
                };
                DB = new LiteDatabase(connectionString);
            }

            Collection = DB.GetCollection<TEntity>();
        }

        public virtual TEntity Create(TEntity entity)
        {
            var newId = Collection.Insert(entity);
            return Collection.FindById(newId.AsGuid);
        }

        public virtual IEnumerable<TEntity> All()
        {
            var list =  Collection.FindAll().ToList();
            return list;
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
