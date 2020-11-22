using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    /// <summary>
    /// Model for lent book.
    /// </summary>
    public class LentBook
    {
        /// <summary>
        /// Id of book.
        /// </summary>
        [Required]
        public int BookId { get; set; }
        
        /// <summary>
        /// Book.
        /// </summary>
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }

        /// <summary>
        /// Id of reader.
        /// </summary>
        [Required]
        public int ReaderId { get; set; }

        /// <summary>
        /// Reader.
        /// </summary>
        [ForeignKey(nameof(ReaderId))]
        public Reader Reader { get; set; }

        /// <summary>
        /// Date of lent.
        /// </summary>
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime LentDate { get; set; } = DateTime.Today;

        /// <summary>
        /// Lent days count.
        /// </summary>
        [Required]
        [Range(1, 365)]
        public int LentDaysCount { get; set; } = 1;
    }
}
