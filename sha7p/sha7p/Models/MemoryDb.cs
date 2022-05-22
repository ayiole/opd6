using System.Collections.Generic;

namespace sha7p.Models
{
    public static class MemoryDb
    {
        public static List<City> Cities { get; set; } = new List<City>()
        {
            new City { Name = "AYYY", FoundingDate = 2000, Population = 999999, Square = 1960},
            new City { Name = "AYYY", FoundingDate = 2000, Population = 999999, Square = 1960},
            new City { Name = "AYYY", FoundingDate = 2000, Population = 999999, Square = 1960},
            new City { Name = "AYYY", FoundingDate = 2000, Population = 999999, Square = 1960}
        };
    }

}
