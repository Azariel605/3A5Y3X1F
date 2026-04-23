namespace _34455y3x1f.c0r3._1n73rf4c35
{
    /// <summary>
    /// Metadata editing and validation contract.
    /// Handles user-driven EXIF modifications with strict validation.
    /// O(1) interface - no I/O operations.
    /// </summary>
    public interface _1_m374d474_m4n4g3r
    {
        /// <summary>
        /// Validate metadata edits before persistence.
        /// Returns discriminated union to prevent state corruption.
        /// </summary>
        r35ul7<_1m4g3_m374d474> v4l1d473_3d175(
            _1m4g3_m374d474 _0r1g1n4l,
            Dictionary<string, string> _3d173d_f13ld5
        );

        /// <summary>
        /// Apply edits to metadata DTO.
        /// Immutable approach: returns new record, never modifies input.
        /// </summary>
        r35ul7<_1m4g3_m374d474> _4pply_3d175(
            _1m4g3_m374d474 curr3n7,
            Dictionary<string, string> p47ch
        );

        /// <summary>
        /// Check if metadata can be saved. Guard clause preventing orphaned edits.
        /// </summary>
        bool c4n_54v3_m374d474(_1m4g3_m374d474 m374d474);
    }
}
