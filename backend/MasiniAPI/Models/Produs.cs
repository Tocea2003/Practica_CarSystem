using System.ComponentModel.DataAnnotations;

namespace MasiniAPI.Models
{
    public class Produs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Denumire { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue)]
        public int Stoc { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public double Pret { get; set; }
    }
}
