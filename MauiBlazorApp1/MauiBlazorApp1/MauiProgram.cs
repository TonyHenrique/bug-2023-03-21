using MauiBlazorApp1.Data;
using Microsoft.Extensions.Logging;
using static BusinessClassLibrary1.Class1;

namespace MauiBlazorApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
#if MAUI || ANDROID || WINDOWS || IOS || MACCATALYST || MACOS || TVOS
            BusinessClassLibrary1.Class1.AmbienteAtual = Ambiente.Hibrido;
#else
            BusinessClassLibrary1.Class1.AmbienteAtual = Ambiente.Normal;
#endif

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                //.ConfigureFonts(fonts =>
                //{
                //    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                //})
                ;

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}