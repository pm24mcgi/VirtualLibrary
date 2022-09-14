using System.ComponentModel.DataAnnotations;

namespace VirtualLibrary.Models
{
    public class Book
    {

        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public bool CheckedOut { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}
