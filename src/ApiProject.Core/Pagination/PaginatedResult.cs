using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Core.Pagination
{
    /// <summary>
    /// Represents collection of items.
    /// </summary>
    /// <typeparam name="T">Type of collection items.</typeparam>
    public class PaginatedResult<T>
    {
        /// <summary>
        /// Initializes an instance of <see cref="PaginatedResult{T}"/>.
        /// </summary>
        /// <param name="items">Collection values.</param>
        /// <param name="total">Total elements count.</param>
        public PaginatedResult([NotNull] List<T> items, long total)
        {
            Items = items;
            Total = total;
        }

        /// <summary>
        /// Collection values.
        /// </summary>
        public List<T> Items { get; }

        /// <summary>
        /// Total elements count.
        /// </summary>
        public long Total { get; set; }
    }
}
