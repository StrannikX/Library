using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    /// <summary>
    /// Model for book.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Book identifier.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Title of book.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Author of book. Can be empty cause some books have not author. (For example Bible).
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Scud of book.
        /// </summary>
        [Required]
        public string Scud { get; set; }

        /// <summary>
        /// Year of books publishing.
        /// </summary>
        [Required]
        public int YearOfPublishing { get; set; }
    }
}
