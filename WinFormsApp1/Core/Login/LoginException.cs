namespace WinFormsApp1.Core.Login;

public class LoginException(string message, Exception innerException) : Exception(message, innerException);