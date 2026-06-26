using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Domain.Validation
{
    public class UniqueAttribute(string tableName, string columnName) : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //var propertyValue = value?.ToString()?.Trim();
            //var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext))!;

            //var table = context.GetType().GetProperty(tableName)?.GetValue(context, null);

            //if (table is IQueryable<object> query)
            //{
            //    var exists = query.Any(e => EF.Property<string>(e, columnName) == propertyValue);

            //    if (exists)
            //    {
            //        //return new ValidationResult($"Giá trị '{propertyValue}' của {columnName} đã tồn tại trong {tableName}.");
            //        return new ValidationResult($"Mã: '{propertyValue}' của đã tồn tại trong hệ thống.");

            //    }
            //}

            return ValidationResult.Success;
        }
    }
}
