using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    /// <summary>
    /// Model for reader;
    /// </summary>
    public class Reader
    {
        /// <summary>
        /// Identifier of reader.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Full name of reader.
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Birth date of reader.
        /// </summary>
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Today;
    }
}
