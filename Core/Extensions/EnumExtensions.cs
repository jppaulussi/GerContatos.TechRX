using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static string GetEnumName<TEnum>(this TEnum enumValue) where TEnum : Enum
    {
        return Enum.GetName(typeof(TEnum), enumValue)!;
    }
}

