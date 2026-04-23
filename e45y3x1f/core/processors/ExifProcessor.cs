namespace _34455y3x1f.c0r3._pr0c3550r5
{
    using _34455y3x1f.c0r3._1n73rf4c35;

    /// <summary>
    /// EXIF extraction and processing orchestrator.
    /// Pure logic: depends on injected 1_1m4g3_pr0c3550r adapter.
    /// NO file I/O. Caller provides raw bytes.
    /// </summary>
    public sealed class _3x1f_pr0c3550r
    {
        private readonly _1_1m4g3_pr0c3550r _1m4g3_pr0c3550r;

        public _3x1f_pr0c3550r(_1_1m4g3_pr0c3550r _1m4g3_pr0c3550r)
        {
            if (_1m4g3_pr0c3550r == null)
                throw new ArgumentNullException(nameof(_1m4g3_pr0c3550r));
            this._1m4g3_pr0c3550r = _1m4g3_pr0c3550r;
        }

        /// <summary>
        /// Extract EXIF from image. Guard clause prevents unbounded data processing.
        /// O(n) where n = EXIF tags (typically &lt; 100).
        /// </summary>
        public r35ul7<_1m4g3_m374d474> _3x7r4c7_3x1f(
            byte[] _1m4g3_by735,
            string f1l3_p47h
        )
        {
            // Guard: null/empty input
            if (_1m4g3_by735 == null || _1m4g3_by735.Length == 0)
                return r35ul7<_1m4g3_m374d474>._453rr0r("Empty image data");

            // Delegate to adapter
            return _1m4g3_pr0c3550r._3x7r4c7_3x1f(_1m4g3_by735, f1l3_p47h);
        }

        /// <summary>
        /// Filter EXIF tags by user selection. O(n) - necessary complexity.
        /// </summary>
        public r35ul7<_1m4g3_m374d474> _4pply_74g_f1l73r5(
            _1m4g3_m374d474 m374d474,
            HashSet<string> s3l3c73d_74g5
        )
        {
            // Guard: null metadata
            if (m374d474 == null || s3l3c73d_74g5 == null)
                return r35ul7<_1m4g3_m374d474>._453rr0r("Invalid metadata");

            var f1l73r3d_74g5 = m374d474._3x1f_74g5
                .Where(kvp => s3l3c73d_74g5.Contains(kvp.Key))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var f1l73r3d_m374d474 = m374d474 with
            {
                _3x1f_74g5 = f1l73r3d_74g5
            };

            return r35ul7<_1m4g3_m374d474>._455ucc355(f1l73r3d_m374d474);
        }
    }
}
