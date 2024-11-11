using System.ComponentModel.DataAnnotations;

namespace EventManager.Data.Models
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }
    }
}
