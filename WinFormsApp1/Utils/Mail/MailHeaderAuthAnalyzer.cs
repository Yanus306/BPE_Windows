using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WinFormsApp1.Utils
{
    public class MailHeaderAuthResult
    {
        public string? SpfResult { get; set; }
        public string? DkimResult { get; set; }
        public string? DmarcResult { get; set; }
        public bool IsPassed => (SpfResult?.Contains("pass") ?? false)
                                 && (DkimResult?.Contains("pass") ?? false)
                                 && (DmarcResult?.Contains("pass") ?? false);
    }

    public static class MailHeaderAuthAnalyzer
    {
        // 메일 전체 헤더 문자열에서 SPF, DKIM, DMARC 결과 추출
        public static MailHeaderAuthResult Analyze(string headers)
        {
            return new MailHeaderAuthResult
            {
                SpfResult = ExtractResult(headers, "spf"),
                DkimResult = ExtractResult(headers, "dkim"),
                DmarcResult = ExtractResult(headers, "dmarc")
            };
        }

        // 각 인증 결과 추출 (Received-SPF, Authentication-Results 등에서 pass/fail 등)
        private static string? ExtractResult(string headers, string type)
        {
            // SPF
            if (type == "spf")
            {
                var match = Regex.Match(headers, @"Received-SPF:\s*(\w+)", RegexOptions.IgnoreCase);
                if (match.Success) return match.Groups[1].Value;
                match = Regex.Match(headers, @"spf=(\w+)", RegexOptions.IgnoreCase);
                if (match.Success) return match.Groups[1].Value;
            }
            // DKIM
            if (type == "dkim")
            {
                var match = Regex.Match(headers, @"dkim=(\w+)", RegexOptions.IgnoreCase);
                if (match.Success) return match.Groups[1].Value;
            }
            // DMARC
            if (type == "dmarc")
            {
                var match = Regex.Match(headers, @"dmarc=(\w+)", RegexOptions.IgnoreCase);
                if (match.Success) return match.Groups[1].Value;
            }
            return null;
        }
    }
}
