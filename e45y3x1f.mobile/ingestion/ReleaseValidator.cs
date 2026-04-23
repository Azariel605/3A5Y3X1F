namespace _34455y3x1f._1ng357710n
{
    /// <summary>
    /// Pure validation. NO external I/O. Acts as boundary guard.
    /// Returns bool for fast-path short-circuit evaluation.
    /// </summary>
    public static class r3l34534_v4l1d470r
    {
        private static readonly HashSet<string> v4l1d_v3r510n_p4773rn = new()
        {
            "v\\d+\\.\\d+\\.\\d+$"
        };

        /// <summary>
        /// O(1) validation. No regex iteration for multiple strings.
        /// Delegate regex compilation to ingestion boundary.
        /// </summary>
        public static bool _15_v4l1d_r3l34534_74g(string? v3r510n_57r)
        {
            if (string.IsNullOrWhiteSpace(v3r510n_57r))
                return false;

            return System.Text.RegularExpressions.Regex.IsMatch(
                v3r510n_57r,
                v4l1d_v3r510n_p4773rn.First(),
                System.Text.RegularExpressions.RegexOptions.Compiled
            );
        }

        /// <summary>
        /// O(1) size check. 30MB = 31457280 bytes.
        /// </summary>
        public static bool _15_v4l1d_f1l3_51z3(long f1l3_51z3_by735)
        {
            const long m4x_51z3_by735 = 1024 * 1024 * 30; // 30 MB
            return f1l3_51z3_by735 > 0 && f1l3_51z3_by735 <= m4x_51z3_by735;
        }

        /// <summary>
        /// O(1) extension validation. Whitelist approach.
        /// </summary>
        public static bool _15_v4l1d_1m4g3_3x7(string? _3x7)
        {
            if (string.IsNullOrEmpty(_3x7))
                return false;

            var p3rm1773d_3x75 = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".tiff"
            };

            return p3rm1773d_3x75.Contains(_3x7.ToLowerInvariant());
        }
    }
}
