using Microsoft.Extensions.Logging;
using Notes.Services;

namespace Notes
{
    public static class MauiProgram
    {
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder app) 
        {
            app.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            app.Services.AddScoped<INoteStorageService, NoteStorageService>();
            
            return app;
        }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-brands-400.ttf", "FaBrands");
                    fonts.AddFont("fa-regular-400.ttf", "FaRegular");
                    fonts.AddFont("fa-solid-900.ttf", "FaSolid");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
