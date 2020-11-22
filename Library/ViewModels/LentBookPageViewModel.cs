using Library.Models;
using System.Collections.Generic;

namespace Library.ViewModels
{
    /// <summary>
    /// View model for lent book page.
    /// </summary>
    public class LentBookPageViewModel
    {
        #region Public Constructors

        /// <summary>
        /// Creates view model.
        /// </summary>
        /// <param name="lentBook">Lent book model.</param>
        /// <param name="books">Available for lent books.</param>
        /// <param name="readers">List of readers.</param>
        public LentBookPageViewModel(LentBook lentBook, IEnumerable<Book> books, IEnumerable<Reader> readers)
        {
            LentBook = lentBook;
            Books = books;
            Readers = readers;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Available for lent books.
        /// </summary>
        public IEnumerable<Book> Books { get; }

        /// <summary>
        /// Lent book model.
        /// </summary>
        public LentBook LentBook { get; }

        /// <summary>
        /// List of readers.
        /// </summary>
        public IEnumerable<Reader> Readers { get; }

        #endregion Public Properties
    }
}
