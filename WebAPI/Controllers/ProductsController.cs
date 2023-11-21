using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    // Route yönlentirme yapar yapi url kısmında domainden sonra "api/Products" yazarsa gelip bu controller'i çalıştıracak
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Loosely coupled(Gevşek bağlılık)
        // naming convention
        // IoC Container -- Inversion of Control
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            // Biz burada IProductService parametresi verdik ve ProductManager class'ından nesne türetip işlemlerimizi ProductManager üzerinden yapmamız lazım. Fakat IProductService bunu bilemediği için yapamaz ve hata verir. O yüzden Program.cs de AddSingleton metodunu kullanarak bu istediğimizi yapabiliriz.
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        //[HttpGet("{id}")] bu şekilde id vererek de yazdığımız metotda yönlendirme yapabiliriz. Yada her metodumuza ayrı isim verip ve httpget gibi isteklerine de isim vererek bu isimler üzerinden çağırırız.
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
