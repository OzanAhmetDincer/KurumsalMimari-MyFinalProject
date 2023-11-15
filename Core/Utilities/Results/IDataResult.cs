namespace Core.Utilities.Results
{
    // Bu interface içerisinde message ya success dışında veri de döndürebilecez(Bunu IResult den miras alıyor). Liste veya bir tabloda ki bir veri.(Data)
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
