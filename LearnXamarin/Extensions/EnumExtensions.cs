using System;

namespace LearnXamarin.Extensions
{
    public static class EnumExtensions
    {
        public static T ParseEnum<T>(this string text)
            where T:struct
        {
            T result;
            if (Enum.TryParse(text, out result))
                return result;
            else
                throw new FormatException($"Unable to parse{text} into a {typeof(T).Name}");
        }
    }
}
