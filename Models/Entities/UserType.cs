using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models
{
    public class UserType
    {
        [Key]
        public int? UserTypeId { get; set; }
        public string TypeName { get; set; }
    }
}