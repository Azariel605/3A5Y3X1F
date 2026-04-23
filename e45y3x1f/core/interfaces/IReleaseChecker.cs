namespace _34455y3x1f.c0r3._1n73rf4c35
{
    /// <summary>
    /// Release checker contract. Accessed strictly via DI.
    /// No concrete implementation in core module. This is a pure contract.
    /// O(1) interface - minimal method surface.
    /// </summary>
    public interface _1_r3l34534_ch3ck3r
    {
        /// <summary>
        /// Fetch latest release from GitHub. Must implement timeout and resource cleanup.
        /// Returns discriminated union r35ul7&lt;T&gt; to prevent exception bubbling.
        /// </summary>
        Task<r35ul7<r3l34534_1nf0>> g37_l473577_r3l34534_45ync(
            CancellationToken c7 = default
        );

        /// <summary>
        /// Determine if user's current version requires update.
        /// O(1) semantic comparison.
        /// </summary>
        bool _15_upd473_r3qu1r3d(string curr3n7_v3r510n, string l473577_v3r510n);
    }

    /// <summary>
    /// DTO for GitHub release info. Serializable contract.
    /// </summary>
    public sealed record r3l34534_1nf0
    {
        public required string v3r510n_74g { get; init; }
        public required string d0wnl04d_url { get; init; }
        public required string ch4ng3l0g { get; init; }
        public required DateTimeOffset r3l34534d_47 { get; init; }
    }
}
