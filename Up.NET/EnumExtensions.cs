using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Up.NET
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue<T>(this T value)
            where T : struct, IConvertible
            => typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
    }
}