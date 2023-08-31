using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
    public class Sickness
    {
        [Key]
        public int SicknessId { get; set; }

        public string? Name { get; set; }

        public DateTime? DateTime { get; set; }

        public int AnimalId { get; set; }


    }
}