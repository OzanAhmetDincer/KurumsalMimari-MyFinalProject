using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        // Bu class içerisinde bağımlılık konfigürasyonlarımızı yaparız. C#'ın kendi IoC container yapısının yerine Autofac gibi eklentileri kurarak, bu eklenti üzerinden IoC container ve başka özellikleride kullanabiliriz. Program çalıştığı zaman aşağıdaki metod da çalışacak ve Program.cs içerisinde "AddSingleton" metodu ile tanımladığımız yapıyı "AutofacBusinessModule" içerisine tanımlarız. AutofacBusinessModule class'ını business katmanı içerisine yazmamızın sebebi, bizim tek API projemiz olmayabilir fakat kullandığımız container yapısı tek. Bunu her projenin program.cs classına yazmak durumunda kalırız. Bu yapıyı iş katmanında kurarsak tüm süreci tek yerden yönetiriz. Oluşturduğumuz DependencyResolvers(bağımlılık çözümü) klasörü içerisine hangi IoC eklentisini kuracaksak ona ait klasör oluştururuz. Sonradan değişiklik yaparsak Program.cs de tanımlama yaptığımız yerde "AutofacServiceProviderFactory" ve "AutofacBusinessModule" kısımlarını değiştirmemiz yeterli olacaktır. Autofac yapısını kullanmak için belirli nugetleri eklememiz gerek.
        protected override void Load(ContainerBuilder builder)
        {
            // As = sana "IProductService" verirlerse, RegisterType = döneceğin tür "ProductManager" tipinde olacak ve bu türden SingleInstance = sadece bir tane instance oluştur.
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
