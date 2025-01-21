using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaP3.Models;
using SQLite;


namespace PruebaP3.Services
{
    

    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Movie>().Wait();
        }

        public Task<List<Movie>> GetMoviesAsync() => _database.Table<Movie>().ToListAsync();

        public Task<int> SaveMovieAsync(Movie movie) => _database.InsertAsync(movie);
    }

}
