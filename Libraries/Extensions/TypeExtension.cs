using System;

namespace HouseofCat.Library.Extensions
{
    /// <summary>
    /// C# extensions for the Type object.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Extention for Type T to see if it has a default constructor or not.
        /// </summary>
        /// <param name="t"></param>
        /// <returns>A bool indicating a default constructor exists.</returns>
        public static bool HasDefaultConstructor(this Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
