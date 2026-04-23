using System.Windows;

namespace e45y3x1f;

/// <summary>
/// Entry point for Easy EXIF application
/// Easy EXIF = Easy EXIF image metadata processor
/// IPO+S Architecture:
///   - Ingestion: File & metadata validation
///   - Processing: EXIF extraction, editing, compression
///   - Output: Result-oriented error handling
///   - State: Adapter-based persistence
/// </summary>
public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var app = new App();
        app.Run();
    }
}
