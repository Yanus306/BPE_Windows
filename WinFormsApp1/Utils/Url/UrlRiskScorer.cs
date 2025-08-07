namespace WinFormsApp1.Utils.Url {
    public static class UrlRiskScorer {
        // 종합 위험도 평가
        public static UrlRiskScoreResult Score(
            RedirectChainResult? chain,
            DynamicAnalysisResult? dyn,
            UrlStaticAnalysisResult? stat) {
            int score = 0;
            string reason = "";

            // 리다이렉션 분석
            if(chain != null) {
                if(chain.Steps.Count > 5) {
                    score += 20;
                    reason += "리다이렉션 5회 초과; ";
                }
                if(chain.Steps.Exists(s => s.IsDomainChanged)) {
                    score += 30;
                    reason += "도메인 변경 발생; ";
                }
                if(chain.Suspicious) {
                    score += 30;
                    reason += $"의심 리다이렉션: {chain.SuspiciousReason}; ";
                }
            }

            // 동적 분석
            if(dyn != null) {
                if(dyn.HasLoginForm) {
                    score += 20;
                    reason += "로그인 폼 존재; ";
                }
                if(dyn.HasSuspiciousKeyword) {
                    score += 20;
                    reason += "의심 키워드 포함; ";
                }
            }

            // 정적 분석
            if(stat != null) {
                if(stat.Length > 100) {
                    score += 10;
                    reason += "URL 길이 100자 초과; ";
                }
                if(stat.IsIpAddress) {
                    score += 20;
                    reason += "IP 주소 사용; ";
                }
                if(stat.HasAtSymbol) {
                    score += 10;
                    reason += "@ 기호 포함; ";
                }
                if(stat.HasSuspiciousKeywords) {
                    score += 10;
                    reason += "의심 키워드 포함; ";
                }
            }

            // 점수 → 등급
            string grade = score >= 70   ? "위험"
                           : score >= 40 ? "주의"
                                           : "안전";

            return new UrlRiskScoreResult {
                Score = Math.Min(score, 100),
                Grade = grade,
                Reason = reason.Trim()
            };
        }
    }
}