#if PLAYWRIGHT
using Microsoft.Playwright;
#endif

namespace WinFormsApp1.Utils.Url {
    public static class PlaywrightDynamicAnalyzer {
#if PLAYWRIGHT
        private static readonly string[] SuspiciousKeywords = new[]
        {
            "login", "password", "verify", "update", "account", "bank", "signin", "otp"
        };

        public static async Task<DynamicAnalysisResult> AnalyzeAsync(string url)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            await page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
            var finalUrl = page.Url;
            var title = await page.TitleAsync();
            // 로그인 폼 존재 여부
            bool hasLogin = (await page.QuerySelectorAsync("input[type='password']")) != null;
            // 의심 키워드 포함 여부 (타이틀, 본문)
            string body = await page.ContentAsync();
            bool hasKeyword = SuspiciousKeywords.Any(k =>
                (title?.IndexOf(k, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0 ||
                (body?.IndexOf(k, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0);
            return new DynamicAnalysisResult
            {
                FinalUrl = finalUrl,
                PageTitle = title,
                HasLoginForm = hasLogin,
                HasSuspiciousKeyword = hasKeyword
            };
        }
#else
        public static Task<DynamicAnalysisResult> AnalyzeAsync(string url) {
            throw new NotImplementedException("Playwright 라이브러리가 필요합니다. NuGet에서 Microsoft.Playwright 설치 후 #define PLAYWRIGHT 추가");
        }
#endif
    }
}