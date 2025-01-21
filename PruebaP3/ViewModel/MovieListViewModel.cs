using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using PruebaP3.Services;
using PruebaP3.Models;

namespace PruebaP3.ViewModel
{
   
    public partial class MovieListViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<Movie> Movies { get; } = new();

        public MovieListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            LoadMovies();
        }

        [RelayCommand]
        private async Task LoadMovies()
        {
            var movies = await _databaseService.GetMoviesAsync();
            Movies.Clear();
            foreach (var movie in movies)
            {
                Movies.Add(movie);
            }
        }
    }

}
