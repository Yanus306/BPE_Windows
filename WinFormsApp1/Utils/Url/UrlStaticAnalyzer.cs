using System.Net;
using System.Text.RegularExpressions;

namespace WinFormsApp1.Utils.Url {
    public static class UrlStaticAnalyzer {
        // 의심 키워드 예시
        private static readonly string[] SuspiciousKeywords = new[] {
            "login", "secure", "update", "verify", "account", "bank", "confirm", "webscr", "signin"
        };

        // URL 정적 분석
        public static UrlStaticAnalysisResult Analyze(string url) {
            var result = new UrlStaticAnalysisResult { Url = url, Length = url.Length };
            result.Domain = ExtractDomain(url);
            result.IsIpAddress = result.Domain != null && IsIp(result.Domain);
            result.HasAtSymbol = url.Contains("@");
            result.HasSuspiciousKeywords = false;
            foreach(var keyword in SuspiciousKeywords) {
                if(url.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) {
                    result.HasSuspiciousKeywords = true;
                    break;
                }
            }
            return result;
        }

        // 도메인 추출 (http/https:// 제외, 포트/경로 제외)
        public static string? ExtractDomain(string url) {
            try {
                var match = Regex.Match(url, @"^(?:https?://)?([^/:?#]+)", RegexOptions.IgnoreCase);
                if(match.Success)
                    return match.Groups[1].Value;
            } catch {
            }
            return null;
        }

        // 도메인이 IP인지 판별
        public static bool IsIp(string domain) {
            return IPAddress.TryParse(domain, out _);
        }
    }
}