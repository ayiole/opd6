using System.Collections.Generic;

namespace sha7p.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Population { get; set; }

        public int Square { get; set; }

        public List<City> Cities { get; set; } = new List<City>();
    }
}
