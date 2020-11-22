using Library.Extensions;
using Library.Models;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Controllers
{
    /// <summary>
    /// Controller for lent books resource.
    /// </summary>
    public class LentBooksController : Controller
    {

        #region Private Fields

        /// <summary>
        /// Database context.
        /// </summary>
        private readonly DatabaseContext _db;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Creates controller.
        /// </summary>
        /// <param name="db">Database context.</param>
        public LentBooksController(DatabaseContext db)
        {
            _db = db;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Returns lent books page.
        /// </summary>
        [Route("lent/add", Name = "LentBookPage")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            IEnumerable<Book> books = await _db.Books
                .NotLentNow(_db.LentBooks)
                .ToListAsync();
            IEnumerable<Reader> readers = await _db.Readers.ToListAsync();
            LentBook lentBook = new LentBook();
            LentBookPageViewModel viewModel = new LentBookPageViewModel(lentBook, books, readers);
            return View(viewModel);
        }

        /// <summary>
        /// Lent out book and redirects to page with lent books list.
        /// If model is not valid return lent page to continue edit.
        /// </summary>
        /// <param name="lentBook">Lent book model.</param>
        [Route("lent/add", Name = "LentBook")]
        [HttpPost]
        public async Task<IActionResult> Add(LentBook lentBook)
        {
            if (ModelState.IsValid && await ValidateLentBook(lentBook))
            {
                _db.LentBooks.Add(lentBook);
                await _db.SaveChangesAsync();

                return RedirectToRoute("LentBooksList");
            }

            IEnumerable<Book> books = await _db.Books
                .NotLentNow(_db.LentBooks)
                .ToListAsync();
            IEnumerable<Reader> readers = await _db.Readers.ToListAsync();
            LentBookPageViewModel viewModel = new LentBookPageViewModel(lentBook, books, readers);
            return View(viewModel);
        }

        /// <summary>
        /// Returns page with list of lent books.
        /// </summary>
        [Route("lent", Name = "LentBooksList")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<LentBook> books = await _db.LentBooks
                .Include(ib => ib.Book)
                .Include(ib => ib.Reader)
                .ToListAsync();

            return View(books);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Validates book and user of lent book model.
        /// </summary>
        /// <param name="lentBook">Lent book model.</param>
        /// <returns>Is lent model valid.</returns>
        private async Task<bool> ValidateLentBook(LentBook lentBook)
        {
            bool isValid = true;

            Book book = await _db.Books.FindAsync(lentBook.BookId);
            if (book is null)
            {
                ModelState.AddModelError("BookId", "Book with that id doesn't exists!");
                isValid = false;
            }

            Reader reader = await _db.Readers.FindAsync(lentBook.ReaderId);
            if (reader is null)
            {
                ModelState.AddModelError("ReaderId", "Reader with that id doesn't exists!");
                isValid = false;
            }

            return isValid;
        }

        #endregion Private Methods

    }
}
