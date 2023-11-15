using Entities.Concrete;

namespace Business.Constants
{
    // new kullanmamak için ve mesaj kısmı sabit olduğu için static yaparız.
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz.";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
    }
}
