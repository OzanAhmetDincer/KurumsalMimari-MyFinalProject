using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product>
            {
                new Product{ProductId = 1, CategoryId = 1, ProductName="Bardak",UnitsInStock=15,UnitPrice=15},
                new Product{ProductId = 2, CategoryId = 1, ProductName="Kamere",UnitsInStock=3,UnitPrice=500},
                new Product{ProductId = 3, CategoryId = 2, ProductName="Telefon",UnitsInStock=2,UnitPrice=1500},
                new Product{ProductId = 4, CategoryId = 2, ProductName="Klavye",UnitsInStock=65,UnitPrice=150},
                new Product{ProductId = 5, CategoryId = 2, ProductName="Fare",UnitsInStock=1,UnitPrice=85}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            // Silme işlemi yapılacağı zaman sileceğimiz değerin id'sini bilsek dahi silemeyiz. Çünkü elimizde bir liste var ve listeyi referans numarası ile tuttuğumuz için bu referans numarası üzerinden silmemiz gerekir. Bizim yapacağımız ilk iş sileceğimiz öğenin id'sini liste içerisinde gezerek bulmamız olacak. Sonrasında bu öğeyi sileriz.
            /*Product productToDelete = null; // bu nesneyi new lemeye gerek yok.(Product productToDelete = new Product();) Böyle yapınca heap memory de alan oluşturacağımız için belleği boşuna yorarız. Bizi buradaki amacımız silinecek öğenin referans numarasını bulmak. O yüzden içi boş bir değer olarak tanımladık.
            foreach (var p in _products)
            {
                if (product.ProductId == p.ProductId)
                {
                    productToDelete = p;//Burada silinecek öğenin referans numarasını "productToDelete" 'e atadık.
                }
            }*/

            //Yukarıdaki kod bloğu ile aşağıdaki kod satırı aynı işi yapar.
            // LINQ -> Language Integrated Query
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);// "SingleOrDefault" tek bir eleman bulmaya yarar.
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            // Gönderdiğim ürün id'sine sahip olan listedeki ürünü bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
