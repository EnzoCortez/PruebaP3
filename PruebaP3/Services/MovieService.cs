using System.Net.Http;
using System.Text.Json;
using PruebaP3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaP3.Services
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;

        // Inyección de dependencia
        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Movie> SearchMovieAsync(string query)
        {
            try
            {
                string url = $"movies?search={query}"; // Se usa la BaseAddress de HttpClient
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null; // No se encontraron resultados o error en la API
                }

                var json = await response.Content.ReadAsStringAsync();
                var movies = JsonSerializer.Deserialize<List<MovieResponse>>(json);

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
            catch (Exception ex)
            {
                Console.WriteLine($"Error en MovieService: {ex.Message}");
                return null;
            }
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
