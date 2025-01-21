using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PruebaP3.Services;

namespace PruebaP3.ViewModel
{
    [ObservableObject]
    public partial class SearchViewModel
    {
        private readonly MovieService _movieService;
        private readonly DatabaseService _databaseService;

        //  Constructor sin parámetros requerido por XAML
        public SearchViewModel() { }

        // Constructor con dependencias (usado en inyección de dependencias)
        public SearchViewModel(MovieService movieService, DatabaseService databaseService)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private string message;

        [RelayCommand]
        private async Task SearchMovie()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Message = "Ingrese un título de película.";
                return;
            }

            var movie = await _movieService.SearchMovieAsync(SearchText);
            if (movie != null)
            {
                await _databaseService.SaveMovieAsync(movie);
                Message = "Película guardada en la base de datos.";
            }
            else
            {
                Message = "No se encontraron resultados.";
            }
        }

        [RelayCommand]
        private void ClearSearch()
        {
            SearchText = string.Empty;
            Message = string.Empty;
        }
    }
}
