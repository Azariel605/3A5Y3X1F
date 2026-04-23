using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace e45y3x1f.mobile;

public static class Program
{
    public static void Main(string[] args)
    {
#if WINDOWS
        CreateMauiApp().Run();
#endif
    }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
