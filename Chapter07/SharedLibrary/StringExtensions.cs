using System.Text.RegularExpressions;
namespace Packt.Shared
{
    public static class StringExtensions
    {
        public static bool IsValidXmlTag(this string input)
        {
            return Regex.IsMatch(input,
            @"^<([a-z]+)([^<]+)*(?:>(.*)<\/\1>|\s+\/>)$");
        }
        public static bool IsValidPassword(this string input)
        {
            // не менее восьми допустимых символов
            return Regex.IsMatch(input, "^[a-zA-Z0-9_-]{8,}$");
        }
        public static bool IsValidHex(this string input)
        {
            // три или шесть допустимых шестнадцатеричных символов
            return Regex.IsMatch(input,
            "^#?([a-fA-F0-9]{3}|[a-fA-F0-9]{6})$");
        }
    }
}