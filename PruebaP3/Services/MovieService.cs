using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using PruebaP3.Models;

/*Creacion del servicio para el consumo de la Api */
namespace PruebaP3.Services
{
   
    public class MovieService
    {
        private readonly HttpClient _httpClient = new();

        public async Task<Movie> SearchMovieAsync(string query)
        {
            string url = $"https://freetestapi.com/api/v1/movies?search={query}";
            var response = await _httpClient.GetStringAsync(url);

            var movies = JsonSerializer.Deserialize<List<MovieResponse>>(response);

            if (movies?.Count > 0)
            {
                var movie = movies[0];
                return new Movie
                {
                    Title = movie.Title,
                    Genre = movie.Genres?[0] ?? "N/A",
                    Actor = movie.Actors?[0] ?? "N/A",
                    Awards = movie.Awards,
                    Website = movie.Website
                };
            }
            return null;
        }
    }

    public class MovieResponse
    {
        public string Title { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Actors { get; set; }
        public string Awards { get; set; }
        public string Website { get; set; }
    }

}
