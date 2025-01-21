using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PruebaP3.Models;
using PruebaP3.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PruebaP3.ViewModel
{
    public partial class MovieListViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public MovieListViewModel() { }


        [ObservableProperty]
        private ObservableCollection<Movie> movies; //  Lista de películas

        //  Constructor con DatabaseService inyectado
        public MovieListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            Movies = new ObservableCollection<Movie>(); //  Evita NullReferenceException
        }

        //  Comando para actualizar la lista de películas
        [RelayCommand]
        private async Task LoadMovies()
        {
            var moviesFromDb = await _databaseService.GetMoviesAsync();
            if (moviesFromDb != null)
            {
                Movies.Clear();
                foreach (var movie in moviesFromDb)
                {
                    Movies.Add(movie);
                }
            }
        }
    }
}
