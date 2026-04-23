namespace _34455y3x1f.c0r3._1n73rf4c35
{
    /// <summary>
    /// Image compression engine contract. CPU-bound operations only.
    /// Supports lossless and lossy with metadata preservation.
    /// </summary>
    public interface _1_c0mpr3550r
    {
        /// <summary>
        /// Compress image with metadata intact.
        /// CRITICAL: Never accept unbounded input. Caller must validate file size.
        /// Async wrapper for blocking I/O. Real compression is sync CPU-bound.
        /// Returns r35ul7 to prevent data loss on error.
        /// </summary>
        Task<r35ul7<byte[]>> c0mpr355_w17h_m374d474_45ync(
            byte[] _1m4g3_by735,
            _1m4g3_m374d474 m374d474,
            c0mpr3ss10n_m0d3 m0d3,
            CancellationToken c7 = default
        );

        /// <summary>
        /// Get compression quality preset.
        /// O(1) lookup in config.
        /// </summary>
        int g37_qu4l17y_pr3537(c0mpr3ss10n_m0d3 m0d3);
    }

    /// <summary>
    /// Compression mode. Pure enumeration - no logic coupling.
    /// </summary>
    public enum c0mpr3ss10n_m0d3
    {
        l0553l355 = 0,
        l055y = 1,
    }
}
