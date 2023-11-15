namespace Core.Utilities.Results
{
    // Temel voidler için başlangıç. Add ,Delete gibi void metotlar için
    public interface IResult
    {
        // Burada ki propertileri işlemlerimizin sonuçlarına göre getiricez yani sadece okuma işlemi yapıcaz(get). İşlem başarılı mesajı ve true-false gibi
        bool Success { get; }
        string Message { get; }
    }
}
