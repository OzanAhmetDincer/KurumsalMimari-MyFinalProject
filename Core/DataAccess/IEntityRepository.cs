using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    // "IEntityRepository" interface'i DataAccess katmanındaydı. Daha düzgün bir sistem kurmak için Core katmanına taşıdık. Her entity için yaptığımız ortak metotları ve içerisine yazdığımız database ve parametreleri bu katmana taşıdık. Başka bir zaman farklı projelerde veritabanı işlemlerimizi yapacağımız zaman data katmanın da tek tek her class için bu metotları yazıp zaman kaybetmiyecez, uğraşmayacaz. Bu metotları da generic hale getirdik
    //generic constrait: "where T :  class" ile T'nin her veri tipinde değer almasını engelleriz. İstenilen tipi yazarız
    // class: referans tip anlamında
    // IEntity: IEntity olabilir yada IEntity implamente eden bir nesne olabilir. Yani her referans tipli değişkeni yazmamızı engeler. Ne tür de referans vereceğimizi belirttik
    // new() : new'lenebilir olmalı. IEntity interface olduğuna göre bunu değer olarak artık veremeyiz fakat IEntity implemente eden class ları verebiliriz. Böylelile sistemimiz veritabanı ile çalışan bir repository oldu
    public interface IEntityRepository<T> where T : class, IEntity, new()  
    {
        // İnterface içerisinde ki metotlar default yani varsayılan olarak public'tir. O yüzden başına public yazmaya gerek yok. İnterface'in kendisi default olarak public olmadığı için onun başına yazarız.
        // İnterface kullanmadan class içerisinde yapacağımız işlemleri yazabiliriz. Başka bir alternetif kullanmayacam diyip her şeyi concrete içerisindeki class da yapabiliriz. Fakat zaman ileledikçe ihtiyaçlr farklılaşınca her hangi bir sıkıntı ile karşılaşmamak için interface tanımlayıp bunun üzerinden işlerimizi yapmamız dha güvenli. Hem de interface'ler inherit edilen class'ında referansını tuttuğu için işimizi kolaylaştırır.
        // Aşağıda ki metotlar tüm entityler için uygulanak olan ortak metotlar. Bunları her bir entity için oluşturduğumuz Dal class'larında yazmak yerine generic bir tip alan ana interface üzerinden verebiliriz. "IEntityRepository" interface'sini generic tip alacak şekilde tanımlarız(<T>). Sonrasında bu interface'yi implamente edeceğimiz diğer interface'lere "T" yerine hangi entity için geçerli olacaksa onu yazarız.(ICategoryDal :IEntityRepository<Category>)
        List<T> GetAll(Expression<Func<T,bool>> filter = null);// Tüm verilerin listelenmesini sağlayacak kod. "GetAll" içerisinde ki kod satırı "p=>p.CategoryId == 2" gibi filtreler yazmamızı sağlar.        
        T Get(Expression<Func<T, bool>> filter);// id ye göre yada başka arama türüne göre istenileni getirecek kod.
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
