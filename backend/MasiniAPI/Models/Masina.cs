using System.ComponentModel.DataAnnotations;

namespace MasiniAPI.Models
{
    public class Masina
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Marca { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;

        [Required]
        [Range(1900, 2025)]
        public int An { get; set; }

        [Required]
        [StringLength(20)]
        public string Motor { get; set; } = string.Empty;
    }
}
