using Microsoft.Extensions.DependencyInjection;
using PruebaP3;
using PruebaP3.Services;
using PruebaP3.ViewModel;
using PruebaP3.Views;
using System.IO;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // 🔹 Obtener la ruta de la base de datos SQLite
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "movies.db");

        // 🔹 Registrar Servicios con su Dependencia
        builder.Services.AddSingleton<MovieService>();
        builder.Services.AddSingleton<DatabaseService>(s => new DatabaseService(dbPath));

        // 🔹 Registrar ViewModels
        builder.Services.AddSingleton<SearchViewModel>();

        // 🔹 Registrar las Páginas
        builder.Services.AddSingleton<SearchPage>();

        return builder.Build();
    }
}
