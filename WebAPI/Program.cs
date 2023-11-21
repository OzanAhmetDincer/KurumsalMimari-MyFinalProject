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

// Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container i�lemleri i�in kullan�labilecek yap�lar. AOP denir.
// IoC Container yap�s�n� kullan�r�z. Bir kutu d���n ve bu kutu i�erisinde ProductManage, EfProductDal vb. yap�lar var ve biz bunlar�n implamente edildi�i interface'ler �zerinden bu class'lar� �a��rd���m�zda otomatik, bizim yerimize new i�lemini yapar. A�a��da da �unu demi� oluruz birisi bizden constructorda IProductService isterse ona ProductManager olu�tur ve referans�n� ver demi� oluruz. AddSingleton ile daha performnsl� bir i�lem yapm�� oluruz. �uan yapt���m�z �rnekte AddSingleton t�m bellekte bir tane ProductManager olu�turuyor ve ka� tane istek gelirse gelsin hepsine ayn� instance'yi veriyor ve i�lemleri bunun �zerinden yap�yor, hepsi ayn� referans� kullan�yor. Fakat AddSingleton metodunu Manager class'�nda bir veri tutmuyorsak yapar�z. Yoksa herkese ayn� veri gider ve biri bir �ey yap�nca di�erlerinde de ayn� i�lem ger�ekle�ir. Verileri veri taban�nda tutunca her hangi bir �ey olmaz. ProductManager da i�erisinde IProductDal kulland��� i�in burada da bir new'leme i�lemi var. O y�zden data katman� i�inde yap�l�r.(IProductDal, EfProductDal)
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();
// Yukar�da yapt���m�z t�m konfig�rasyon i�lemlerini Autofac kullanarak a�a��daki gibi tan�mlar�z.
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
