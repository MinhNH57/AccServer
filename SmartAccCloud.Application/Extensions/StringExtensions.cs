namespace SmartAccCloud.Application.Extensions
{
    public static class StringExtensions
    {
        public static void TrimStrings<T>(this T obj)
        {
            var stringProperties = typeof(T).GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite);

            foreach (var prop in stringProperties)
            {
                var value = (string)prop.GetValue(obj);
                if (value != null)
                {
                    // Loại bỏ dấu cách, dấu tab, dấu xuống dòng ở đầu và cuối chuỗi
                    value = value.Trim();
                    prop.SetValue(obj, value);
                }
            }
        }
    }
}