#if PLAYWRIGHT
using Microsoft.Playwright;
#endif

namespace WinFormsApp1.Utils.Url {
    public static class PlaywrightRedirectTracer {
#if PLAYWRIGHT
        public static async Task<string> GetFinalUrlAsync(string url)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            var response = await page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
            return page.Url;
        }
#else
        // PlaywrightSharp가 설치되어 있지 않으면 컴파일 에러 방지용 더미
        public static Task<string> GetFinalUrlAsync(string url) {
            throw new NotImplementedException("PlaywrightSharp 라이브러리가 필요합니다. NuGet에서 Microsoft.Playwright 설치 후 #define PLAYWRIGHT 추가");
        }
#endif
    }
}