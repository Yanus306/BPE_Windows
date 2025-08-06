namespace WinFormsApp1.Utils
{
    public static class OAuthConfig
    {
        // Google OAuth 설정
        public static class Google
        {
            public const string ClientId = "YOUR_GOOGLE_CLIENT_ID";
            public const string ClientSecret = "YOUR_GOOGLE_CLIENT_SECRET";
            public const string RedirectUri = "http://localhost:8080/callback";
            public const string AuthUrl = "https://accounts.google.com/o/oauth2/v2/auth";
            public const string TokenUrl = "https://oauth2.googleapis.com/token";
            public const string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
            public const string Scope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
        }

        // Naver OAuth 설정
        public static class Naver
        {
            public const string ClientId = "YOUR_NAVER_CLIENT_ID";
            public const string ClientSecret = "YOUR_NAVER_CLIENT_SECRET";
            public const string RedirectUri = "http://localhost:8080/callback";
            public const string AuthUrl = "https://nid.naver.com/oauth2.0/authorize";
            public const string TokenUrl = "https://nid.naver.com/oauth2.0/token";
            public const string UserInfoUrl = "https://openapi.naver.com/v1/nid/me";
        }

        // OAuth 설정 가이드
        public static class SetupGuide
        {
            public static string GetGoogleSetupGuide()
            {
                return @"Google OAuth 설정 방법:

1. Google Cloud Console (https://console.cloud.google.com/) 접속
2. 새 프로젝트 생성 또는 기존 프로젝트 선택
3. 'API 및 서비스' > '사용자 인증 정보' 메뉴로 이동
4. '사용자 인증 정보 만들기' > 'OAuth 2.0 클라이언트 ID' 선택
5. 애플리케이션 유형을 '데스크톱 앱'으로 설정
6. 클라이언트 ID와 클라이언트 보안 비밀번호를 복사
7. OAuthConfig.cs 파일의 Google 섹션에 설정값 입력

주의: 실제 배포 시에는 보안을 위해 환경 변수나 설정 파일을 사용하세요.";
            }

            public static string GetNaverSetupGuide()
            {
                return @"Naver OAuth 설정 방법:

1. Naver Developers (https://developers.naver.com/) 접속
2. '애플리케이션 등록' 메뉴로 이동
3. 새 애플리케이션 등록
4. 서비스 환경에서 '웹 서비스 URL' 설정
5. 'Callback URL'을 http://localhost:8080/callback로 설정
6. 클라이언트 ID와 클라이언트 보안 비밀번호를 복사
7. OAuthConfig.cs 파일의 Naver 섹션에 설정값 입력

주의: 실제 배포 시에는 보안을 위해 환경 변수나 설정 파일을 사용하세요.";
            }
        }
    }
} 