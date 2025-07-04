using System;
public static class StringExtensions
{
    public static string CapitalizeFirst(this string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}