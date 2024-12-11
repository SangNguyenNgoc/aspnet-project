using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Ganss.Xss;

namespace MovieApp.Application;

public static class AppUtil
{
    public static string GenerateSlug(string phrase)
    {
        var slug = phrase.ToLowerInvariant();
        slug = RemoveDiacritics(slug);
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        slug = Regex.Replace(slug, @"\s+", "-").Trim();
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
    
    private static readonly List<string> AllowedTags =
        ["b", "i", "u", "p", "a", "div", "span", "h1", "h2", "h3", "h4", "strong", "ul", "li", "ol"];
    
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