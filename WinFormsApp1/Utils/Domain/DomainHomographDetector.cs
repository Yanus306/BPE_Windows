using System.Text.RegularExpressions;

namespace WinFormsApp1.Utils.Domain {
    public static class DomainHomographDetector {
        // 주요 브랜드 도메인 예시 (실제 서비스에 맞게 확장 가능)
        private static readonly string[] KnownDomains = new[] {
            "google.com", "naver.com", "kakao.com", "daum.net", "microsoft.com", "apple.com"
        };

        // 유사문자 치환 테이블 (키: 의심문자, 값: 실제문자)
        private static readonly Dictionary<char, char> HomographMap = new() {
            { '0', 'o' }, { '1', 'l' }, { '3', 'e' }, { '5', 's' }, { '7', 't' },
            { 'а', 'a' }, // 키릴문자 a
            { 'е', 'e' }, // 키릴문자 e
            { 'і', 'i' }, // 키릴문자 i
            { 'ο', 'o' }, // 그리스문자 o
            { 'с', 'c' }, // 키릴문자 c
            { 'р', 'p' }, // 키릴문자 p
            { 'ԁ', 'd' }, // 키릴문자 d
            { 'ԛ', 'q' }, // 키릴문자 q
            // 필요시 추가
        };

        // 도메인에서 유사문자 치환
        public static string NormalizeHomograph(string domain) {
            var arr = domain.ToCharArray();
            for(int i = 0; i < arr.Length; i++) {
                if(HomographMap.TryGetValue(arr[i], out var realChar))
                    arr[i] = realChar;
            }
            return new string(arr);
        }

        // Levenshtein 거리 계산
        public static int Levenshtein(string s, string t) {
            if(s == t) return 0;
            if(s.Length == 0) return t.Length;
            if(t.Length == 0) return s.Length;
            int[,] d = new int[s.Length + 1, t.Length + 1];
            for(int i = 0; i <= s.Length; i++) d[i, 0] = i;
            for(int j = 0; j <= t.Length; j++) d[0, j] = j;
            for(int i = 1; i <= s.Length; i++)
            for(int j = 1; j <= t.Length; j++) {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
            return d[s.Length, t.Length];
        }

        // 도메인 유사도 및 호모그래피 공격 탐지
        public static (bool isSuspicious, string? matchedBrand, int distance) Analyze(string domain) {
            string normalized = NormalizeHomograph(domain.ToLower());
            foreach(var brand in KnownDomains) {
                int dist = Levenshtein(normalized, brand);
                if(dist <= 2) // 임계값: 2 이하이면 의심
                    return (true, brand, dist);
            }
            return (false, null, int.MaxValue);
        }

        // URL에서 도메인 추출
        public static string? ExtractDomain(string url) {
            try {
                var match = Regex.Match(url, @"^(?:https?://)?([^/]+)", RegexOptions.IgnoreCase);
                if(match.Success)
                    return match.Groups[1].Value;
            } catch {
            }
            return null;
        }
    }
}