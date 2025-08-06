using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace WinFormsApp1.Utils
{
    public class MailSenderInfo
    {
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? Domain { get; set; }
    }

    public static class MailSenderParser
    {
        // "홍길동 <user@example.com>" 또는 "user@example.com" 등 다양한 형식 지원
        public static MailSenderInfo Parse(string rawSender)
        {
            try
            {
                var mailAddr = new MailAddress(rawSender);
                return new MailSenderInfo
                {
                    DisplayName = string.IsNullOrWhiteSpace(mailAddr.DisplayName) ? null : mailAddr.DisplayName,
                    Email = mailAddr.Address,
                    Domain = ExtractDomain(mailAddr.Address)
                };
            }
            catch
            {
                // 수동 파싱 (RFC5322 미준수 등 예외 케이스)
                var match = Regex.Match(rawSender, @"([\w\.-]+)@([\w\.-]+)");
                if (match.Success)
                {
                    return new MailSenderInfo
                    {
                        DisplayName = null,
                        Email = match.Value,
                        Domain = match.Groups[2].Value
                    };
                }
                return new MailSenderInfo { DisplayName = null, Email = null, Domain = null };
            }
        }

        public static string? ExtractDomain(string email)
        {
            var idx = email.IndexOf('@');
            if (idx >= 0 && idx < email.Length - 1)
                return email[(idx + 1)..];
            return null;
        }
    }
}
