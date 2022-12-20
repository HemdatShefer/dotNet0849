using System.Collections;
using System.Reflection;

namespace OtherFunctions
{
    internal static class ToStringGenric
    {
        internal static string toString<Item>(this Item item, string str = "")
        {
            IEnumerable<PropertyInfo> propertyInfos = item!.GetType().GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                var value = propertyInfo.GetValue(item, null);
                if (value is IEnumerable and not string)
                {
                    var items = value as IEnumerable;

                    str += '\n' + "Items:" + '\n';

                    str += string.Join('\n', items!.Cast<object>());
                }
                else
                {
                    str += propertyInfo.Name + ": " + propertyInfo.GetValue(item) + '\n';
                }

            }

            return str;
        }
    }
}
