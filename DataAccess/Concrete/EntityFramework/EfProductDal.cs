using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // Products'lara p dedik Categories'lere c dedik ve Products ile Categories join yaptık. p'deki CategoryId ile c'deki CategoryId eşitse(equals) yap. select ile de gelmesini istediğimiz kolonları yazarız. Sonucu "ProductDetailDto" daki kolonlara göre ver demiş olduk.
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName=p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock=p.UnitsInStock
                             };
                return result.ToList();
            }
        }
    }
}
