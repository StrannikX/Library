using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    /// <summary>
    /// Controller for books.
    /// </summary>
    public class BooksController : Controller
    {
        #region Private Fields

        /// <summary>
        /// Database context.
        /// </summary>
        private readonly DatabaseContext _db;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Creates books controller.
        /// </summary>
        /// <param name="db">Database context.</param>
        public BooksController(DatabaseContext db)
        {
            _db = db;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Returns book creation page.
        /// </summary>
        [Route("book/create", Name = "CreateBookPage")]
        [HttpGet]
        public IActionResult Create()
        {
            Book book = new Book();
            return View(book);
        }

        /// <summary>
        /// Creates book and redirects to books list page.
        /// If model is not valid returns book creation page for continue editing.
        /// </summary>
        /// <param name="book">New book.</param>
        [Route("book/create", Name = "CreateBook")]
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Add(book);
                await _db.SaveChangesAsync();

                return RedirectToRoute("BooksList");
            }

            return View(book);
        }

        /// <summary>
        /// Deletes book.
        /// </summary>
        /// <param name="id">Id of book to delete.</param>
        [Route("book/{id}/delete", Name = "DeleteBook")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Book book = await _db.Books.FindAsync(id);
            if (book is null)
            {
                return NotFound();
            }

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToRoute("BooksList");
        }

        /// <summary>
        /// Returns book editing page.
        /// </summary>
        /// <param name="id">Id of book to edit.</param>
        [Route("book/{id}/edit", Name = "EditBookPage")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = await _db.Books.FindAsync(id);
            if (book is null)
            {
                return NotFound();
            }
            return View(book);
        }

        /// <summary>
        /// Updates book and redirects to books list page.
        /// If model is not valid returns book editing page for continue editing.
        /// </summary>
        /// <param name="book">Book with new data.</param>
        /// <param name="id">Id of book.</param>
        [Route("book/{id}/edit", Name = "UpdateBook")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            bool bookExists = await _db.Books
                .AnyAsync(b => b.Id == id);

            if (!bookExists)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Books.Update(book);
                await _db.SaveChangesAsync();

                return RedirectToRoute("BooksList");
            }

            return View(book);
        }

        /// <summary>
        /// Filter books by title and returns page with list of books.
        /// </summary>
        /// <param name="title">Desired title of book.</param>
        [Route("", Name = "BooksList")]
        [HttpGet]
        public async Task<IActionResult> Index(string title = null)
        {
            IQueryable<Book> query = _db.Books;

            ViewBag.BookTitle = string.Empty;
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(book => book.Title.Contains(title));
                ViewBag.BookTitle = title;
            }

            IEnumerable<Book> books = await query.ToListAsync();
            return View(books);
        }

        #endregion Public Methods
    }
}
