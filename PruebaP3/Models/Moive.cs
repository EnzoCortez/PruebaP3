using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PruebaP3.Models
{
    

    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Actor { get; set; }
        public string Awards { get; set; }
        public string Website { get; set; }
        public string Ecortez { get; set; } = "Ecortez";
    }

}
