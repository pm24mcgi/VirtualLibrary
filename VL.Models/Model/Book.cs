using System.ComponentModel.DataAnnotations;

namespace VL.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Author { get; set; } = string.Empty;
        [Required]

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public bool CheckedOut { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}
