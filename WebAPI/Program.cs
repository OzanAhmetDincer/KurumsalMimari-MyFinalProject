using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container iþlemleri için kullanýlabilecek yapýlar. AOP denir.
// IoC Container yapýsýný kullanýrýz. Bir kutu düþün ve bu kutu içerisinde ProductManage, EfProductDal vb. yapýlar var ve biz bunlarýn implamente edildiði interface'ler üzerinden bu class'larý çaðýrdýðýmýzda otomatik, bizim yerimize new iþlemini yapar. Aþaðýda da þunu demiþ oluruz birisi bizden constructorda IProductService isterse ona ProductManager oluþtur ve referansýný ver demiþ oluruz. AddSingleton ile daha performnslý bir iþlem yapmýþ oluruz. Þuan yaptýðýmýz örnekte AddSingleton tüm bellekte bir tane ProductManager oluþturuyor ve kaç tane istek gelirse gelsin hepsine ayný instance'yi veriyor ve iþlemleri bunun üzerinden yapýyor, hepsi ayný referansý kullanýyor. Fakat AddSingleton metodunu Manager class'ýnda bir veri tutmuyorsak yaparýz. Yoksa herkese ayný veri gider ve biri bir þey yapýnca diðerlerinde de ayný iþlem gerçekleþir. Verileri veri tabanýnda tutunca her hangi bir þey olmaz. ProductManager da içerisinde IProductDal kullandýðý için burada da bir new'leme iþlemi var. O yüzden data katmaný içinde yapýlýr.(IProductDal, EfProductDal)
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();
// Yukarýda yaptýðýmýz tüm konfigürasyon iþlemlerini Autofac kullanarak aþaðýdaki gibi tanýmlarýz.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});







// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
