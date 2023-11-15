namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        // Aşağıda tanımladığımız propert'ler set edilemez fakat constructor kullanarak bunları sadece constructor içersinde set edebiliriz. Farklı bir yerde set edilemez. Bu özelliği kapatıp kodları daha standart hale getirmemizin sebebi kodlar farklı yerlerde değiştirilmesin. Biz "ProductManager" class'ında "Add" metodunda return olarak "Result" class'ını döndürüyoruz. Bu class içerisinde Success ve Message olarak tanımladığımız property'leri döndürmek için constructor kullanarak nesnelerimizi oluştururuz ki "ProductManager" class'ında (return new Result(true,"Ürün eklendi.")) dediğimiz zaman true ve "ürün eklendi" bilgilerini geriye döndürsün.
        // İlla geriye hem message hem de success döndürmek zorunda değiliz. Farklı bir şekilde de tasarlayabiliriz. Örneğin sadece success olup olmadığını da döndürebiliriz. Bu durum da sadece success parametresi alan bir "Result" constructor daha yazarak overload ederiz. İki parametre alan constructor içerisinde sadece message parametresini tanımlarız. Olursa success parametreside gönderilirse "this(success)" ile diğer constructor a gidip bize parametre olarak verilen success'i oraya veririz. Böylelikle iki sonucu yada tek bir sonucu döndürürüz.
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
