using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Extensions
{
    /// <summary>
    /// Some extensions for db.
    /// </summary>
    public static class DBExtensions
    {
        /// <summary>
        /// Returns query with books not lent now.
        /// </summary>
        /// <param name="books">Books query.</param>
        /// <param name="lentBooks">Lent books model collection.</param>
        /// <returns>Query with books not lent now.</returns>
        public static IQueryable<Book> NotLentNow(this IQueryable<Book> books, IQueryable<LentBook> lentBooks)
        {
            var lentBooksId = lentBooks
                .Where(issue => EF.Functions.DateDiffDay(DateTime.Today, issue.LentDate) < issue.LentDaysCount)
                .Select(issue => issue.BookId)
                .Distinct();

            return books
                .Where(book => !lentBooksId.Contains(book.Id));
        }
    }
}
