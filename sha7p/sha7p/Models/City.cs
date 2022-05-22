using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sha7p.Models
{
    public class City
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoundingDate { get; set; }
        public int Population { get; set; }
        public int Square { get; set; }


        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
