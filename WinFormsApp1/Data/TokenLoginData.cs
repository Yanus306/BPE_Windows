namespace WinFormsApp1.Data;

public class TokenLoginData : AccessableLoginData {
    public Platform Platform;
    public string Token;
    
    public TokenLoginData(Platform platform, string token) {
        Platform = platform;
        Token = token;
    }
}