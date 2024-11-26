using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Ganss.Xss;

namespace MovieApp.Application.Feature.Movie.Services;

public class AppUtil
{
    public static string GenerateSlug(string phrase)
    {
        // Chuyển đổi sang chữ thường
        string slug = phrase.ToLowerInvariant();

        // Chuẩn hóa các ký tự có dấu thành không dấu
        slug = RemoveDiacritics(slug);

        // Loại bỏ các ký tự đặc biệt
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

        // Thay khoảng trắng bằng dấu gạch ngang
        slug = Regex.Replace(slug, @"\s+", "-").Trim();

        // Loại bỏ các dấu gạch ngang thừa
        slug = Regex.Replace(slug, @"-+", "-");

        return slug;
    }

    private static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
    
    private static readonly List<string> AllowedTags = new() { "b", "i", "u", "p", "a", "div", "span", "h1", "h2", "h3", "h4", "strong", "ul", "li", "ol" };
    
    public static string SanitizeHtml(string inputHtml)
    {
        var sanitizer = new HtmlSanitizer();
        sanitizer.AllowedTags.Clear();

        // Thêm các thẻ được phép nếu cần
        foreach (var item in AllowedTags)
        {
            sanitizer.AllowedTags.Add(item);
        }

        // Sanitize HTML
        return sanitizer.Sanitize(inputHtml);
    }
    
}