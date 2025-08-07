using System.Diagnostics;
using System.Net;

namespace WinFormsApp1.Utils.Url {
    public static class HttpRedirectChainAnalyzer {
        public static async Task<RedirectChainResult> AnalyzeAsync(string url, int maxRedirects = 10) {
            var result = new RedirectChainResult();
            string? prevDomain = null;
            string currentUrl = url;
            int redirectCount = 0;
            var handler = new HttpClientHandler { AllowAutoRedirect = false };
            using var client = new HttpClient(handler);
            while(redirectCount < maxRedirects) {
                var step = new RedirectStep { Url = currentUrl };
                var sw = Stopwatch.StartNew();
                HttpResponseMessage? resp = null;
                try {
                    resp = await client.GetAsync(currentUrl);
                    step.StatusCode = (int) resp.StatusCode;
                } catch (Exception ex) {
                    step.StatusCode = -1;
                    result.Steps.Add(step);
                    result.Suspicious = true;
                    result.SuspiciousReason = $"요청 실패: {ex.Message}";
                    break;
                }
                sw.Stop();
                step.ResponseTimeMs = sw.ElapsedMilliseconds;
                // 도메인 변경 여부
                string domain = GetDomain(currentUrl);
                step.IsDomainChanged = prevDomain != null && !string.Equals(domain, prevDomain, StringComparison.OrdinalIgnoreCase);
                prevDomain = domain;
                // SSL 유효성 (https 여부만 간단 체크)
                step.IsSslValid = currentUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
                result.Steps.Add(step);
                // 리다이렉션 여부
                if(resp.StatusCode is HttpStatusCode.Moved or HttpStatusCode.Redirect or HttpStatusCode.RedirectMethod or HttpStatusCode.TemporaryRedirect
                   or (HttpStatusCode) 308) {
                    if(resp.Headers.Location == null) {
                        result.Suspicious = true;
                        result.SuspiciousReason = "리다이렉션 Location 헤더 없음";
                        break;
                    }
                    string nextUrl = resp.Headers.Location.IsAbsoluteUri ? resp.Headers.Location.ToString() : new Uri(new Uri(currentUrl), resp.Headers.Location).ToString();
                    // 도메인 변경 감지
                    if(step.IsDomainChanged) {
                        result.Suspicious = true;
                        result.SuspiciousReason = "리다이렉션 중 도메인 변경 발생";
                    }
                    currentUrl = nextUrl;
                    redirectCount++;
                } else {
                    break;
                }
            }
            if(redirectCount >= maxRedirects) {
                result.Suspicious = true;
                result.SuspiciousReason = "과도한 리다이렉션";
            }
            return result;
        }

        private static string GetDomain(string url) {
            try {
                var uri = new Uri(url);
                return uri.Host;
            } catch {
                return url;
            }
        }
    }
}