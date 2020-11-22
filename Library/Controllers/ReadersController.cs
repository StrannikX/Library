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
    /// Controllers for readers resource.
    /// </summary>
    public class ReadersController : Controller
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
        public ReadersController(DatabaseContext db)
        {
            _db = db;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Returns reader creation page.
        /// </summary>
        [Route("reader/create", Name = "CreateReaderPage")]
        [HttpGet]
        public IActionResult Create()
        {
            Reader reader = new Reader();
            return View(reader);
        }

        /// <summary>
        /// Creates reader and redirects to readers list page.
        /// If model is not valid returns reader creation page for continue editing.
        /// </summary>
        /// <param name="reader">New reader.</param>
        [Route("reader/create", Name = "CreateReader")]
        [HttpPost]
        public async Task<IActionResult> Create(Reader reader)
        {
            if (ModelState.IsValid)
            {
                _db.Readers.Add(reader);
                await _db.SaveChangesAsync();

                return RedirectToRoute("ReadersList");
            }

            return View(reader);
        }

        /// <summary>
        /// Removes reader with id <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Id of reader.</param>
        [Route("reader/{id}/delete", Name = "DeleteReader")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Reader reader = await _db.Readers.FindAsync(id);
            if (reader is null)
            {
                return NotFound();
            }

            _db.Readers.Remove(reader);
            await _db.SaveChangesAsync();

            return RedirectToRoute("ReadersList");
        }

        /// <summary>
        /// Returns editing page for reader with id <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Id of reader.</param>
        [Route("reader/{id}/edit", Name = "EditReaderPage")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Reader reader = await _db.Readers.FindAsync(id);
            if (reader is null)
            {
                return NotFound();
            }
            return View(reader);
        }

        /// <summary>
        /// Updates reader and redirects to readers list page.
        /// If model is not valid returns reader editing page for continue editing.
        /// </summary>
        /// <param name="id">Id of reader.</param>
        /// <param name="reader">Reader with new data.</param>
        [Route("reader/{id}/edit", Name = "UpdateReader")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Reader reader)
        {
            bool readerExists = await _db.Readers
                .AnyAsync(r => r.Id == id);

            if (!readerExists)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Readers.Update(reader);
                await _db.SaveChangesAsync();

                return RedirectToRoute("ReadersList");
            }

            return View(reader);
        }

        /// <summary>
        /// Filter readers by name and returns page with list of readers.
        /// </summary>
        /// <param name="name">Desired reader name.</param>
        [Route("readers", Name = "ReadersList")]
        [HttpGet]
        public async Task<IActionResult> Index(string name = null)
        {
            IQueryable<Reader> query = _db.Readers;

            ViewBag.ReaderName = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(reader => reader.FullName.Contains(name));
                ViewBag.ReaderName = name;
            }

            IEnumerable<Reader> readers = await query.ToListAsync();
            return View(readers);
        }

        #endregion Public Methods
    }
}
