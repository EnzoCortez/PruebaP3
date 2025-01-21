using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using PruebaP3.Services;


namespace PruebaP3.ViewModel
{
    

    public partial class SearchViewModel : ObservableObject
    {
        private readonly MovieService _movieService;
        private readonly DatabaseService _databaseService;

        public SearchViewModel()
        {

        }

        public SearchViewModel(MovieService movieService, DatabaseService databaseService)
        {
            _movieService = movieService;
            _databaseService = databaseService;
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
