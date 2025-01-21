using PruebaP3;
using PruebaP3.Services;
using PruebaP3.ViewModel;
using PruebaP3.Views;
using Microsoft.Extensions.DependencyInjection;

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

        // Ruta de la base de datos
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "movies.db");

        // Inyectar HttpClient para MovieService
        builder.Services.AddHttpClient<MovieService>(client =>
        {
            client.BaseAddress = new Uri("https://freetestapi.com/api/v1/");
        });

        // Inyectar DatabaseService con la ruta de la BD
        builder.Services.AddSingleton<DatabaseService>(s => new DatabaseService(dbPath));

        // Inyectar ViewModels con sus dependencias
        builder.Services.AddSingleton<SearchViewModel>(s =>
            new SearchViewModel(
                s.GetRequiredService<MovieService>(),
                s.GetRequiredService<DatabaseService>()
            ));

        builder.Services.AddSingleton<MovieListViewModel>(s =>
            new MovieListViewModel(
                s.GetRequiredService<DatabaseService>()
            ));

        // Inyectar Vistas
        builder.Services.AddSingleton<SearchPage>();
        builder.Services.AddSingleton<MovieListPage>();

        // Inyectar AppShell
        builder.Services.AddSingleton<AppShell>();

        return builder.Build();
    }
}
