using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WinFormsApp1.Utils.Mail {
    public static class MailHeaderAuthAnalyzer {
        public static MailHeaderAuthResult AnalyzeHeaders(Dictionary<string, string> headers) {
            var result = new MailHeaderAuthResult();

            // SPF 분석
            AnalyzeSpf(headers, result);

            // DKIM 분석
            AnalyzeDkim(headers, result);

            // DMARC 분석
            AnalyzeDmarc(headers, result);

            return result;
        }

        private static void AnalyzeSpf(Dictionary<string, string> headers, MailHeaderAuthResult result) {
            var spfHeader = headers.FirstOrDefault(h =>
                h.Key.Equals("Received-SPF", StringComparison.OrdinalIgnoreCase) ||
                h.Key.Equals("X-SPF-Result", StringComparison.OrdinalIgnoreCase)).Value;

            if(string.IsNullOrEmpty(spfHeader)) {
                result.SpfResult = "SPF 헤더 없음";
                result.SpfExplanation = "SPF 인증 정보가 없습니다.";
                result.SpfPass = false;
                return;
            }

            // SPF 결과 파싱
            var spfMatch = Regex.Match(spfHeader, @"(\w+)\s+\(([^)]+)\)");
            if(spfMatch.Success) {
                result.SpfResult = spfMatch.Groups[1].Value;
                result.SpfExplanation = spfMatch.Groups[2].Value;
                result.SpfPass = result.SpfResult.Equals("pass", StringComparison.OrdinalIgnoreCase);
            } else {
                result.SpfResult = spfHeader;
                result.SpfExplanation = "SPF 결과를 파싱할 수 없습니다.";
                result.SpfPass = false;
            }
        }

        private static void AnalyzeDkim(Dictionary<string, string> headers, MailHeaderAuthResult result) {
            var dkimHeader = headers.FirstOrDefault(h =>
                h.Key.Equals("DKIM-Signature", StringComparison.OrdinalIgnoreCase) ||
                h.Key.Equals("X-DKIM-Result", StringComparison.OrdinalIgnoreCase)).Value;

            if(string.IsNullOrEmpty(dkimHeader)) {
                result.DkimResult = "DKIM 헤더 없음";
                result.DkimDomain = "";
                result.DkimPass = false;
                return;
            }

            // DKIM 결과 파싱
            var dkimMatch = Regex.Match(dkimHeader, @"(\w+)\s+\(([^)]+)\)");
            if(dkimMatch.Success) {
                result.DkimResult = dkimMatch.Groups[1].Value;
                result.DkimDomain = ExtractDkimDomain(dkimHeader);
                result.DkimPass = result.DkimResult.Equals("pass", StringComparison.OrdinalIgnoreCase);
            } else {
                result.DkimResult = dkimHeader;
                result.DkimDomain = ExtractDkimDomain(dkimHeader);
                result.DkimPass = false;
            }
        }

        private static string ExtractDkimDomain(string dkimHeader) {
            var domainMatch = Regex.Match(dkimHeader, @"d=([^;]+)");
            return domainMatch.Success ? domainMatch.Groups[1].Value : "";
        }

        private static void AnalyzeDmarc(Dictionary<string, string> headers, MailHeaderAuthResult result) {
            var dmarcHeader = headers.FirstOrDefault(h =>
                h.Key.Equals("X-DMARC-Result", StringComparison.OrdinalIgnoreCase) ||
                h.Key.Equals("DMARC-Result", StringComparison.OrdinalIgnoreCase)).Value;

            if(string.IsNullOrEmpty(dmarcHeader)) {
                result.DmarcResult = "DMARC 헤더 없음";
                result.DmarcPolicy = "";
                result.DmarcPass = false;
                return;
            }

            // DMARC 결과 파싱
            var dmarcMatch = Regex.Match(dmarcHeader, @"(\w+)\s+\(([^)]+)\)");
            if(dmarcMatch.Success) {
                result.DmarcResult = dmarcMatch.Groups[1].Value;
                result.DmarcPolicy = ExtractDmarcPolicy(dmarcHeader);
                result.DmarcPass = result.DmarcResult.Equals("pass", StringComparison.OrdinalIgnoreCase);
            } else {
                result.DmarcResult = dmarcHeader;
                result.DmarcPolicy = ExtractDmarcPolicy(dmarcHeader);
                result.DmarcPass = false;
            }
        }

        private static string ExtractDmarcPolicy(string dmarcHeader) {
            var policyMatch = Regex.Match(dmarcHeader, @"p=(\w+)");
            return policyMatch.Success ? policyMatch.Groups[1].Value : "";
        }

        public static string GetDetailedReport(MailHeaderAuthResult result) {
            var report = new System.Text.StringBuilder();
            report.AppendLine("=== 메일 인증 분석 결과 ===");
            report.AppendLine($"전체 상태: {result.OverallStatus}");
            report.AppendLine();

            // SPF 결과
            report.AppendLine("📧 SPF (Sender Policy Framework):");
            report.AppendLine($"   결과: {result.SpfResult}");
            report.AppendLine($"   설명: {result.SpfExplanation}");
            report.AppendLine($"   상태: {(result.SpfPass ? "✅ 통과" : "❌ 실패")}");
            report.AppendLine();

            // DKIM 결과
            report.AppendLine("🔐 DKIM (DomainKeys Identified Mail):");
            report.AppendLine($"   결과: {result.DkimResult}");
            report.AppendLine($"   도메인: {result.DkimDomain}");
            report.AppendLine($"   상태: {(result.DkimPass ? "✅ 통과" : "❌ 실패")}");
            report.AppendLine();

            // DMARC 결과
            report.AppendLine("🛡️ DMARC (Domain-based Message Authentication):");
            report.AppendLine($"   결과: {result.DmarcResult}");
            report.AppendLine($"   정책: {result.DmarcPolicy}");
            report.AppendLine($"   상태: {(result.DmarcPass ? "✅ 통과" : "❌ 실패")}");

            return report.ToString();
        }

        public static string GetSecurityRecommendation(MailHeaderAuthResult result) {
            var recommendations = new List<string>();

            if(!result.SpfPass) {
                recommendations.Add("• SPF 인증이 실패했습니다. 발신자 도메인의 신뢰성을 확인하세요.");
            }

            if(!result.DkimPass) {
                recommendations.Add("• DKIM 인증이 실패했습니다. 메일이 변조되었을 가능성이 있습니다.");
            }

            if(!result.DmarcPass) {
                recommendations.Add("• DMARC 정책 위반이 감지되었습니다. 추가 주의가 필요합니다.");
            }

            if(result.OverallPass) {
                return "✅ 모든 인증이 통과되었습니다. 이 메일은 안전합니다.";
            } else {
                return "⚠️ 보안 주의사항:\n" + string.Join("\n", recommendations);
            }
        }
    }
}