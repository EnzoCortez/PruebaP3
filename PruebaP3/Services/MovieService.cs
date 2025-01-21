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
                string url = $"https://freetestapi.com/api/v1/movies?search={query}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error en la API: {response.StatusCode}");
                    return null;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Respuesta JSON: {jsonResponse}");

                var movies = JsonSerializer.Deserialize<List<MovieResponse>>(jsonResponse);

                if (movies == null || movies.Count == 0)
                {
                    Console.WriteLine("No se encontraron películas.");
                    return null;
                }

                var movie = movies[0];

                return new Movie
                {
                    Title = movie.title,
                    Genre = movie.genre?.FirstOrDefault() ?? "N/A",  // <-- Aquí se cambia a "Genre"
                    Actor = movie.actors?.FirstOrDefault() ?? "N/A",
                    Awards = movie.awards,
                    Website = movie.website
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SearchMovieAsync: {ex.Message}");
                return null;
            }
        }


    }

    public class MovieResponse
    {
        public string title { get; set; }
        public List<string> genre { get; set; }
        public List<string> actors { get; set; }
        public string awards { get; set; }
        public string website { get; set; }
    }
}
