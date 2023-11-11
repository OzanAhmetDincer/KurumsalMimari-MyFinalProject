using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDisposable pattern implementation of c#
            // Bir classı new'lediğimiz de bellekten "garbage collector" belirli aralıklarla bellekten bunu atar. "using" kullanınca scoop arasında ki kodlar çalıştıktan sonra hemen bellekten atma işlemini yapar. contex nesnesi pahalıdır. Bu şekilde yazarsak daha persormanslı bir yapı kurmuş oluruz. Yoksa direkt "NorthwindContext context = new NorthwindContext()" şeklide de yazabiliriz.
            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(entity);// Referansı yakalarız yani işlem yapılacak yeri, adresi alırız
                addedEntity.State = EntityState.Added;// Eklemeyi yaparız
                context.SaveChanges();// Kaydederiz
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);// Referansı yakalarız yani işlem yapılacak yeri, adresi alırız.
                deletedEntity.State = EntityState.Deleted;// Silmeyi yaparız
                context.SaveChanges();// Kaydederiz
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)// bir filtre verebiliriz vermezsekde null olur
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // Bir filtre var mı yoksa direkt Product listesini getir varsa filtreyi uygula(Where(filter)) ve getir. "Set" ile işlemlerin uyguanmasını istediğimiz entity'e konumlanırız. "Expression<Func<Product, bool>>" bu yapı lambda ile yaptığımız filtreleme işlemini yapmamızı sağlar.
                return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updateEntity = context.Entry(entity);// Referansı yakalarız yani işlem yapılacak yeri, adresi alırız
                updateEntity.State = EntityState.Modified;// Güncelleme yaparız
                context.SaveChanges();// Kaydederiz
            }
        }
    }
}
