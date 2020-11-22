using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    /// <summary>
    /// Context of database.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        #region Public Constructors

        /// <summary>
        /// Creates database.
        /// </summary>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Database collection of books.
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Database collection of lent books.
        /// </summary>
        public DbSet<LentBook> LentBooks { get; set; }

        /// <summary>
        /// Database collection of readers.
        /// </summary>
        public DbSet<Reader> Readers { get; set; }

        #endregion Public Properties

        #region Protected Methods

        /// <summary>
        /// Set up models.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LentBook>(builder =>
            {
                builder.HasKey(issuedBook => new { issuedBook.BookId, issuedBook.ReaderId });
            });

            modelBuilder.Entity<Book>(builder =>
            {
                builder.HasIndex(book => book.Title);
            });

            modelBuilder.Entity<Reader>(reader =>
            {
                reader.HasIndex(reader => reader.FullName);
            });
        }

        #endregion Protected Methods
    }
}
