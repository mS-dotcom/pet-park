using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
    public class Vaccine
    {

        [Key]
        public int VaccineId { get; set; }
        public string? VaccineName { get; set; }
        public DateTime? DateTime { get; set; }
        public DateTime? NextDateTime { get; set; }
        public int AnimalId { get; set; }
        
    }
    



}