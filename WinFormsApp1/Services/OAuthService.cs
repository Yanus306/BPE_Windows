using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;
using WinFormsApp1.Models;
using WinFormsApp1.Utils;
using System.Net.Http;
using System.Threading.Tasks;

namespace WinFormsApp1.Services
{
    public class OAuthService
    {

        public static void HandleGoogleLogin()
        {
            try
            {
                // Google OAuth URL 생성
                var googleAuthUrl = $"{OAuthConfig.Google.AuthUrl}?" +
                    $"client_id={OAuthConfig.Google.ClientId}&" +
                    $"redirect_uri={Uri.EscapeDataString(OAuthConfig.Google.RedirectUri)}&" +
                    $"response_type=code&" +
                    $"scope={Uri.EscapeDataString(OAuthConfig.Google.Scope)}&" +
                    $"access_type=offline";

                // 브라우저에서 Google 로그인 페이지 열기
                Process.Start(new ProcessStartInfo
                {
                    FileName = googleAuthUrl,
                    UseShellExecute = true
                });

                MessageBox.Show("브라우저에서 Google 로그인 페이지가 열렸습니다.\n" +
                    "로그인 후 인증 코드를 받아서 처리합니다.", 
                    "Google 로그인", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Google 로그인 처리 중 오류가 발생했습니다: {ex.Message}", 
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void HandleNaverLogin()
        {
            try
            {
                // Naver OAuth URL 생성
                var naverAuthUrl = $"{OAuthConfig.Naver.AuthUrl}?" +
                    $"client_id={OAuthConfig.Naver.ClientId}&" +
                    $"redirect_uri={Uri.EscapeDataString(OAuthConfig.Naver.RedirectUri)}&" +
                    $"response_type=code&" +
                    $"state={GenerateRandomState()}";

                // 브라우저에서 Naver 로그인 페이지 열기
                Process.Start(new ProcessStartInfo
                {
                    FileName = naverAuthUrl,
                    UseShellExecute = true
                });

                MessageBox.Show("브라우저에서 Naver 로그인 페이지가 열렸습니다.\n" +
                    "로그인 후 인증 코드를 받아서 처리합니다.", 
                    "Naver 로그인", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Naver 로그인 처리 중 오류가 발생했습니다: {ex.Message}", 
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GenerateRandomState()
        {
            // CSRF 공격 방지를 위한 랜덤 state 생성
            var random = new Random();
            var bytes = new byte[32];
            random.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public static async Task<bool> ProcessOAuthCallback(string code, string provider)
        {
            try
            {
                string accessToken = "";
                string userEmail = "";

                if (provider.ToLower() == "google")
                {
                    // Google 토큰 교환
                    accessToken = await ExchangeGoogleCodeForToken(code);
                    userEmail = await GetGoogleUserEmail(accessToken);
                }
                else if (provider.ToLower() == "naver")
                {
                    // Naver 토큰 교환
                    accessToken = await ExchangeNaverCodeForToken(code);
                    userEmail = await GetNaverUserEmail(accessToken);
                }

                if (!string.IsNullOrEmpty(userEmail))
                {
                    // 사용자 세션에 정보 저장
                    UserSession.Email = userEmail;
                    UserSession.Password = "oauth_token"; // 실제로는 토큰을 안전하게 저장해야 함
                    
                    MessageBox.Show($"{provider} 로그인이 성공했습니다!\n이메일: {userEmail}", 
                        "로그인 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"OAuth 콜백 처리 중 오류가 발생했습니다: {ex.Message}", 
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static async Task<string> ExchangeGoogleCodeForToken(string code)
        {
            // Google 인증 코드를 액세스 토큰으로 교환
            var tokenUrl = OAuthConfig.Google.TokenUrl;
            var postData = $"client_id={OAuthConfig.Google.ClientId}&" +
                          $"client_secret={OAuthConfig.Google.ClientSecret}&" +
                          $"code={code}&" +
                          $"grant_type=authorization_code&" +
                          $"redirect_uri={Uri.EscapeDataString(OAuthConfig.Google.RedirectUri)}";

            using var client = new HttpClient();
            var content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync(tokenUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // JSON 응답에서 access_token 추출 (실제로는 JSON 파싱 필요)
            if (responseContent.Contains("access_token"))
            {
                // 간단한 예시 - 실제로는 JSON 파싱 라이브러리 사용
                var startIndex = responseContent.IndexOf("\"access_token\":\"") + 16;
                var endIndex = responseContent.IndexOf("\"", startIndex);
                return responseContent.Substring(startIndex, endIndex - startIndex);
            }

            throw new Exception("액세스 토큰을 받지 못했습니다.");
        }

        private static async Task<string> ExchangeNaverCodeForToken(string code)
        {
            // Naver 인증 코드를 액세스 토큰으로 교환
            var tokenUrl = OAuthConfig.Naver.TokenUrl;
            var postData = $"client_id={OAuthConfig.Naver.ClientId}&" +
                          $"client_secret={OAuthConfig.Naver.ClientSecret}&" +
                          $"code={code}&" +
                          $"grant_type=authorization_code&" +
                          $"state={GenerateRandomState()}";

            using var client = new HttpClient();
            var content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync(tokenUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // JSON 응답에서 access_token 추출
            if (responseContent.Contains("access_token"))
            {
                var startIndex = responseContent.IndexOf("\"access_token\":\"") + 16;
                var endIndex = responseContent.IndexOf("\"", startIndex);
                return responseContent.Substring(startIndex, endIndex - startIndex);
            }

            throw new Exception("액세스 토큰을 받지 못했습니다.");
        }

        private static async Task<string> GetGoogleUserEmail(string accessToken)
        {
            // Google API를 사용하여 사용자 이메일 가져오기
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            
            var response = await client.GetAsync(OAuthConfig.Google.UserInfoUrl);
            var content = await response.Content.ReadAsStringAsync();

            // JSON 응답에서 email 추출
            if (content.Contains("\"email\":"))
            {
                var startIndex = content.IndexOf("\"email\":\"") + 9;
                var endIndex = content.IndexOf("\"", startIndex);
                return content.Substring(startIndex, endIndex - startIndex);
            }

            throw new Exception("사용자 이메일을 가져오지 못했습니다.");
        }

        private static async Task<string> GetNaverUserEmail(string accessToken)
        {
            // Naver API를 사용하여 사용자 정보 가져오기
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            
            var response = await client.GetAsync(OAuthConfig.Naver.UserInfoUrl);
            var content = await response.Content.ReadAsStringAsync();

            // JSON 응답에서 email 추출
            if (content.Contains("\"email\":"))
            {
                var startIndex = content.IndexOf("\"email\":\"") + 9;
                var endIndex = content.IndexOf("\"", startIndex);
                return content.Substring(startIndex, endIndex - startIndex);
            }

            throw new Exception("사용자 이메일을 가져오지 못했습니다.");
        }
    }
} 