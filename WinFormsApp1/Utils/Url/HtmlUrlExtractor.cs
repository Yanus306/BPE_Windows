using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using HtmlAgilityPack;

namespace WinFormsApp1.Utils
{
    public static class HtmlUrlExtractor
    {
        // 단축 URL 서비스 예시
        private static readonly string[] ShortenerDomains = new[]
        {
            "bit.ly", "t.co", "goo.gl", "tinyurl.com", "ow.ly", "is.gd", "buff.ly", "cutt.ly", "rebrand.ly"
        };

        // HTML 및 텍스트에서 모든 URL 추출
        public static List<string> ExtractAll(string htmlOrText)
        {
            var urls = new HashSet<string>();

            // 1. HTML 태그 속성 추출 (a, img, iframe 등)
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlOrText);
                foreach (var node in doc.DocumentNode.SelectNodes("//a[@href] | //img[@src] | //iframe[@src]") ?? Enumerable.Empty<HtmlNode>())
                {
                    var url = node.GetAttributeValue("href", null) ?? node.GetAttributeValue("src", null);
                    if (!string.IsNullOrWhiteSpace(url))
                        urls.Add(url);
                }
            }
            catch { }

            // 2. CSS url() 함수 추출
            foreach (Match m in Regex.Matches(htmlOrText, @"url\(['\"]?(.*?)['\"]?\)", RegexOptions.IgnoreCase))
            {
                if (m.Groups.Count > 1 && !string.IsNullOrWhiteSpace(m.Groups[1].Value))
                    urls.Add(m.Groups[1].Value);
            }

            // 3. 텍스트 내 일반 URL 추출 (http/https/ftp)
            foreach (Match m in Regex.Matches(htmlOrText, @"https?://[\w\-\.:/?#@!$&'()*+,;=%]+", RegexOptions.IgnoreCase))
            {
                urls.Add(m.Value);
            }

            // 4. Base64 인코딩된 URL 추출 (간단 패턴)
            foreach (Match m in Regex.Matches(htmlOrText, @"([A-Za-z0-9+/=]{20,})", RegexOptions.None))
            {
                try
                {
                    var decoded = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(m.Value));
                    if (Regex.IsMatch(decoded, @"https?://"))
                        urls.Add(decoded);
                }
                catch { }
            }

            // 5. 단축 URL 탐지
            foreach (var url in urls.ToList())
            {
                foreach (var shortener in ShortenerDomains)
                {
                    if (url.Contains(shortener, StringComparison.OrdinalIgnoreCase))
                        urls.Add(url); // 이미 포함되어 있으면 그대로 둠 (확장 가능)
                }
            }

            return urls.ToList();
        }
    }
}
