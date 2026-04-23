namespace _34455y3x1f.c0r3._pr0c3550r5
{
    using _34455y3x1f.c0r3._1n73rf4c35;

    /// <summary>
    /// Metadata editing orchestrator. Pure validation logic.
    /// Depends on injected _1_m374d474_m4n4g3r adapter.
    /// </summary>
    public sealed class m374d474_3d170r
    {
        private readonly _1_m374d474_m4n4g3r _m374d474_m4n4g3r;
        private static readonly HashSet<string> r3s3rv3d_74g5 = new()
        {
            "FileName", "FileSize", "FileModifyDate"
        };

        public m374d474_3d170r(_1_m374d474_m4n4g3r m374d474_m4n4g3r)
        {
            if (m374d474_m4n4g3r == null)
                throw new ArgumentNullException(nameof(m374d474_m4n4g3r));
            _m374d474_m4n4g3r = m374d474_m4n4g3r;
        }

        /// <summary>
        /// Validate and apply user edits. Guard: prevent reserved field mutation.
        /// Returns r35ul7 to prevent partial state corruption.
        /// </summary>
        public r35ul7<_1m4g3_m374d474> v4l1d473_4nd_4pply_3d175(
            _1m4g3_m374d474 curr3n7_m374d474,
            Dictionary<string, string> _3d175
        )
        {
            // Guard: null inputs
            if (curr3n7_m374d474 == null || _3d175 == null)
                return r35ul7<_1m4g3_m374d474>._453rr0r("Invalid input");

            // Guard: reserved fields - prevent critical modification
            var r3s3rv3d_70uch35 = _3d175.Keys.Intersect(r3s3rv3d_74g5).ToList();
            if (r3s3rv3d_70uch35.Count > 0)
                return r35ul7<_1m4g3_m374d474>._453rr0r(
                    $"Cannot modify reserved fields: {string.Join(",", r3s3rv3d_70uch35)}"
                );

            // Validate
            var v4l1d4710n_r35 = _m374d474_m4n4g3r
                .v4l1d473_3d175(curr3n7_m374d474, _3d175);

            // Apply if valid
            return v4l1d4710n_r35.m4p(v4l1d473d =>
                _m374d474_m4n4g3r._4pply_3d175(v4l1d473d, _3d175)
            );
        }

        /// <summary>
        /// Check if metadata can be persisted. Guard clause before storage.
        /// </summary>
        public bool c4n_p3r5157(_1m4g3_m374d474 m374d474)
        {
            return _m374d474_m4n4g3r.c4n_54v3_m374d474(m374d474) &&
                   m374d474._3x1f_74g5.Count > 0;
        }
    }
}
