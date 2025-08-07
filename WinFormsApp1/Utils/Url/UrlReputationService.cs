namespace WinFormsApp1.Utils.Url {
    public static class UrlReputationService {
        // 단축 URL 서비스 도메인
        private static readonly HashSet<string> ShortenerDomains = new(StringComparer.OrdinalIgnoreCase) {
            "bit.ly", "t.co", "goo.gl", "tinyurl.com", "ow.ly", "is.gd", "buff.ly", "cutt.ly", "rebrand.ly"
        };

        // 블랙리스트/화이트리스트 (실제 서비스에서는 DB/파일/네트워크 연동 가능)
        private static readonly HashSet<string> Blacklist = new(StringComparer.OrdinalIgnoreCase) {
            "phishing.com", "evil.com", "bad.com"
        };

        private static readonly HashSet<string> Whitelist = new(StringComparer.OrdinalIgnoreCase) {
            "google.com", "naver.com", "kakao.com", "daum.net", "microsoft.com", "apple.com"
        };

        // 단축 URL 여부
        public static bool IsShortener(string url) {
            var domain = ExtractDomain(url);
            return domain != null && ShortenerDomains.Contains(domain);
        }

        // 블랙리스트 여부
        public static bool IsBlacklisted(string url) {
            var domain = ExtractDomain(url);
            return domain != null && Blacklist.Contains(domain);
        }

        // 화이트리스트 여부
        public static bool IsWhitelisted(string url) {
            var domain = ExtractDomain(url);
            return domain != null && Whitelist.Contains(domain);
        }

        // 평판 점수 산정 (100: 매우 위험, 0: 매우 안전)
        public static int GetReputationScore(string url) {
            var domain = ExtractDomain(url);
            if(domain == null) return 50;
            if(Blacklist.Contains(domain)) return 100;
            if(Whitelist.Contains(domain)) return 0;
            if(ShortenerDomains.Contains(domain)) return 70;
            // 기타: 도메인 길이, 특이문자 등 추가 규칙 가능
            if(domain.Length > 20) return 60;
            return 50;
        }

        // 도메인 추출 (http/https:// 제외, 포트/경로 제외)
        public static string? ExtractDomain(string url) {
            try {
                var uri = new Uri(url);
                return uri.Host;
            } catch {
                return null;
            }
        }

        // 블랙리스트/화이트리스트 동적 관리 (추가/삭제)
        public static void AddToBlacklist(string domain) => Blacklist.Add(domain);
        public static void RemoveFromBlacklist(string domain) => Blacklist.Remove(domain);
        public static void AddToWhitelist(string domain) => Whitelist.Add(domain);
        public static void RemoveFromWhitelist(string domain) => Whitelist.Remove(domain);
    }
}