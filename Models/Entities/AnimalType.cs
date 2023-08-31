using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
    public class AnimalType
    {
        [Key]
        public int AnimalTypeId { get; set; }
        public string TypeName { get; set; }
    }
}