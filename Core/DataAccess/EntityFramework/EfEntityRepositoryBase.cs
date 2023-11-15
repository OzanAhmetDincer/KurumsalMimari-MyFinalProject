using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            // Bir classı new'lediğimiz de bellekten "garbage collector" belirli aralıklarla bellekten bunu atar. "using" kullanınca scoop arasında ki kodlar çalıştıktan sonra hemen bellekten atma işlemini yapar. contex nesnesi pahalıdır. Bu şekilde yazarsak daha persormanslı bir yapı kurmuş oluruz. Yoksa direkt "NorthwindContext context = new NorthwindContext()" şeklide de yazabiliriz.
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);// Referansı yakalarız yani işlem yapılacak yeri, adresi alırız
                addedEntity.State = EntityState.Added;// Eklemeyi yaparız
                context.SaveChanges();// Kaydederiz
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);// Referansı yakalarız yani işlem yapılacak yeri, adresi alırız.
                deletedEntity.State = EntityState.Deleted;// Silmeyi yaparız
                context.SaveChanges();// Kaydederiz
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)// bir filtre verebiliriz vermezsekde null olur
        {
            using (TContext context = new TContext())
            {
                // Bir filtre var mı yoksa direkt Product listesini getir varsa filtreyi uygula(Where(filter)) ve getir. "Set" ile işlemlerin uyguanmasını istediğimiz entity'e konumlanırız. "Expression<Func<Product, bool>>" bu yapı lambda ile yaptığımız filtreleme işlemini yapmamızı sağlar.
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updateEntity = context.Entry(entity);// Referansı yakalarız yani işlem yapılacak yeri, adresi alırız
                updateEntity.State = EntityState.Modified;// Güncelleme yaparız
                context.SaveChanges();// Kaydederiz
            }
        }
    }
}
