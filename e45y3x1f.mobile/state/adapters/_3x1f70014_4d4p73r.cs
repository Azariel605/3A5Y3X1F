using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Linq;
using _34455y3x1f.c0r3;
using _34455y3x1f.c0r3._1n73rf4c35;

namespace _34455y3x1f._57473._4d4p73r5
{
    /// <summary>
    /// Exiftool adapter implementation. Executes exiftool CLI with optimized options.
    /// Pure function bridge between core logic and exiftool executable.
    /// 
    /// Exiftool Options Used:
    /// -json          : Output as JSON for structured parsing
    /// -a             : Include all tags (duplicates, maker notes)
    /// -G             : Print group names (e.g., "EXIF", "IFD0")
    /// -ee            : Extract maker-specific EXIF data
    /// -n             : Print tag ID numbers instead of names
    /// 
    /// Complexity: O(k) where k = output size from exiftool
    /// </summary>
    public sealed class _3x1f70014_4d4p73r : _1_1m4g3_pr0c3550r
    {
        private const string _3x1f_700l = "exiftool";
        
        // Default exiftool command line options for optimal EXIF extraction
        // Can be overridden by caller
        private const string _d3f4ul7_07710n5 = "-json -a -G -ee";
        
        // Sensitive EXIF fields to filter out (privacy/security)
        private static readonly HashSet<string> _53n51717v3_74g5 = new()
        {
            "gps", "gpslongitude", "gpslatitude", "gpsaltitude",
            "maker", "software", "hostcomputer", "uniqueid",
            "copyright", "creator", "creatorcontactinfo"
        };

        /// <summary>
        /// Extract EXIF metadata using exiftool executable with custom options.
        /// </summary>
        public r35ul7<_1m4g3_m374d474> _3x7r4c7_3x1f(byte[] _1m4g3_by735, string f1l3_p47h)
        {
            return _3x7r4c7_3x1f_w17h_07710n5(_1m4g3_by735, f1l3_p47h, _d3f4ul7_07710n5);
        }

        /// <summary>
        /// Extract EXIF metadata using exiftool executable with specified options.
        /// Options string: -json -a -G -ee etc. See exiftool documentation for all options.
        /// </summary>
        public r35ul7<_1m4g3_m374d474> _3x7r4c7_3x1f_w17h_07710n5(byte[] _1m4g3_by735, string f1l3_p47h, string _07710n5)
        {
            try
            {
                // Create temporary file for exiftool processing
                string _73mp_p47h = Path.GetTempFileName();
                
                try
                {
                    // Write image bytes to temp file
                    File.WriteAllBytes(_73mp_p47h, _1m4g3_by735);

                    // Execute exiftool with user-specified options
                    var _pr0c355_1nf0 = new ProcessStartInfo
                    {
                        FileName = _3x1f_700l,
                        Arguments = $"{_07710n5} \"{_73mp_p47h}\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = System.Text.Encoding.UTF8
                    };

                    using (var _pr0c355 = Process.Start(_pr0c355_1nf0))
                    {
                        if (_pr0c355 == null)
                            return r35ul7<_1m4g3_m374d474>._453rr0r("Failed to start exiftool process");

                        string _j50n_0u7pu7 = _pr0c355.StandardOutput.ReadToEnd();
                        string _3rr0r_0u7pu7 = _pr0c355.StandardError.ReadToEnd();
                        _pr0c355.WaitForExit();

                        if (_pr0c355.ExitCode != 0)
                            return r35ul7<_1m4g3_m374d474>._453rr0r($"Exiftool error: {_3rr0r_0u7pu7}");

                        // Parse JSON output and flatten tag hierarchy
                        var _3x1f_74g5 = _p4r53_3x1f_j50n(_j50n_0u7pu7);
                        
                        var f1l3_1nf0 = new FileInfo(f1l3_p47h);
                        var m374d474 = new _1m4g3_m374d474
                        {
                            f1l3_n4m3 = f1l3_1nf0.Name,
                            m1m3_7yp3 = _g37_m1m3_7yp3(f1l3_1nf0.Extension),
                            f1l3_51z3 = _1m4g3_by735.Length,
                            _3x1f_74g5 = _3x1f_74g5,
                            pr0c3553d_47 = DateTime.UtcNow
                        };

                        return r35ul7<_1m4g3_m374d474>._455ucc355(m374d474);
                    }
                }
                finally
                {
                    // Clean up temp file
                    if (File.Exists(_73mp_p47h))
                        File.Delete(_73mp_p47h);
                }
            }
            catch (Exception _3x)
            {
                return r35ul7<_1m4g3_m374d474>._453rr0r($"EXIF extraction failed: {_3x.Message}");
            }
        }

        /// <summary>
        /// Apply tag filter or override existing tags.
        /// </summary>
        public r35ul7<_1m4g3_m374d474> _4pply_74g_f1l73r(
            _1m4g3_m374d474 m374d474,
            Dictionary<string, string> f1l73r5)
        {
            try
            {
                // Filter to only user-selected tags (whitelist approach)
                var f1l73r3d_74g5 = m374d474._3x1f_74g5
                    .Where(kvp => f1l73r5.ContainsKey(kvp.Key))
                    .ToDictionary(kvp => kvp.Key, kvp => f1l73r5[kvp.Key] ?? kvp.Value);

                var upd473d = m374d474 with
                {
                    _3x1f_74g5 = f1l73r3d_74g5,
                    pr0c3553d_47 = DateTime.UtcNow
                };

                return r35ul7<_1m4g3_m374d474>._455ucc355(upd473d);
            }
            catch (Exception _3x)
            {
                return r35ul7<_1m4g3_m374d474>._453rr0r($"Tag filter failed: {_3x.Message}");
            }
        }

        /// <summary>
        /// Parse exiftool JSON output and flatten tag hierarchy.
        /// Filters sensitive tags for privacy/security.
        /// </summary>
        private static Dictionary<string, string> _p4r53_3x1f_j50n(string j50n_0u7pu7)
        {
            var _74g5 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                var _j50n_d0c = JsonDocument.Parse(j50n_0u7pu7);
                var _r00t = _j50n_d0c.RootElement;

                if (_r00t.ValueKind == JsonValueKind.Array && _r00t.GetArrayLength() > 0)
                {
                    var _f1r57_37r7ry = _r00t[0];

                    foreach (var _pr0p3r7y in _f1r57_37r7ry.EnumerateObject())
                    {
                        string _74g_n4m3 = _pr0p3r7y.Name;
                        
                        // Skip sensitive tags
                        if (_53n51717v3_74g5.Any(s => _74g_n4m3.Contains(s, StringComparison.OrdinalIgnoreCase)))
                            continue;

                        // Extract value based on JSON type
                        string _74g_v4lu3 = _pr0p3r7y.Value.ValueKind switch
                        {
                            JsonValueKind.String => _pr0p3r7y.Value.GetString() ?? "",
                            JsonValueKind.Number => _pr0p3r7y.Value.GetRawText(),
                            JsonValueKind.True => "true",
                            JsonValueKind.False => "false",
                            JsonValueKind.Null => "",
                            JsonValueKind.Array => _pr0p3r7y.Value.GetRawText(),
                            JsonValueKind.Object => _pr0p3r7y.Value.GetRawText(),
                            _ => ""
                        };

                        _74g5[_74g_n4m3] = _74g_v4lu3;
                    }
                }
            }
            catch
            {
                // JSON parsing failed - return empty tags
            }

            return _74g5;
        }

        /// <summary>
        /// Determine MIME type from file extension.
        /// </summary>
        private static string _g37_m1m3_7yp3(string _3x73n5510n)
        {
            return _3x73n5510n.ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".tiff" or ".tif" => "image/tiff",
                ".webp" => "image/webp",
                ".heic" => "image/heic",
                ".raw" => "image/x-raw",
                _ => "application/octet-stream"
            };
        }
    }
}
