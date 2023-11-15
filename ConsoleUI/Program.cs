using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;


//ProductTest();
//ProductTest1();
ProductTest2();
//CategoryTest();


static void ProductTest()
{
    ProductManager productManager = new ProductManager(new InMemoryProductDal());

    foreach (var product in productManager.GetAll().Data)
    {
        Console.WriteLine(product.ProductName);
    }

    foreach (var product in productManager.GetAllByCategoryId(2).Data)
    {
        Console.WriteLine(product.ProductName);
    }
}
static void ProductTest1()
{
    ProductManager productManager = new ProductManager(new EfProductDal());

    foreach (var product in productManager.GetByUnitPrice(40, 100).Data)
    {
        Console.WriteLine(product.ProductName);
    }
}
static void ProductTest2()
{
    ProductManager productManager = new ProductManager(new EfProductDal());
    var result = productManager.GetProductDetails();

    if (result.Success == true)
    {
        foreach (var product in result.Data)
        {
            Console.WriteLine(product.ProductName + " / " + product.CategoryName);
        }
    }
    else
    {
        Console.WriteLine(result.Message);
    }
}

static void CategoryTest()
{
    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
    foreach (var category in categoryManager.GetAll())
    {
        Console.WriteLine(category.CategoryName);
    }
}