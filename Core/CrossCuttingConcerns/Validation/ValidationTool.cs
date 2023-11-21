using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Core.CrossCuttingConcerns.Validation
{
    // static ile tek bir instance oluşturulur ve sadece bu kullanılır, tekrar tekrar new'lenmesin diye. Bu tarz tool yapıları static tanımlanır.
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
