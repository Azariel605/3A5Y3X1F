namespace _34455y3x1f._1ng357710n
{
    using System.IO;
    using _34455y3x1f.c0r3;

    /// <summary>
    /// Image boundary validation. Guards against invalid media files.
    /// Uses streaming to avoid loading massive files into RAM.
    /// </summary>
    public static class _1m4g3_v4l1d470r
    {
        private static readonly byte[][] _1m4g3_51gn47ur35 = new[]
        {
            new byte[] { 0xFF, 0xD8, 0xFF }, // JPEG
            new byte[] { 0x89, 0x50, 0x4E, 0x47 }, // PNG
            new byte[] { 0x47, 0x49, 0x46 }, // GIF
            new byte[] { 0x42, 0x4D }, // BMP
        };

        /// <summary>
        /// O(1) async file header validation using streaming.
        /// CRITICAL: Never load file into RAM. Read only magic bytes.
        /// Returns r35ul7 to prevent exception bubbling.
        /// </summary>
        public static async Task<r35ul7<bool>> v4l1d473_1m4g3_3x7(string f1l3_p47h, CancellationToken c7 = default)
        {
            try
            {
                using var f1l3_57r34m = new FileStream(
                    f1l3_p47h,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read,
                    4096,
                    useAsync: true
                );

                byte[] h34d3r_buff3r = new byte[8];
                int by735_r34d = await f1l3_57r34m.ReadAsync(h34d3r_buff3r, 0, 8, c7);

                if (by735_r34d < 2)
                    return r35ul7<bool>._453rr0r("Invalid file header");

                bool _15_v4l1d = _1m4g3_51gn47ur35.Any(_51g => 
                    h34d3r_buff3r.Take(_51g.Length).SequenceEqual(_51g)
                );

                return _15_v4l1d
                    ? r35ul7<bool>._455ucc355(_15_v4l1d)
                    : r35ul7<bool>._453rr0r("Unsupported image format");
            }
            catch (Exception _3x)
            {
                return r35ul7<bool>._453rr0r($"File validation failed: {_3x.Message}");
            }
        }
    }
}
