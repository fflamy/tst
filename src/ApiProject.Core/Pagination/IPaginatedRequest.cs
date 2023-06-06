namespace ApiProject.Core.Pagination
{
    /// <summary>
    /// Постраничный запрос.
    /// </summary>
    public interface IPaginatedRequest
    {
        /// <summary>
        /// Размер возвращаемого массива.
        /// </summary>
        public int? Take { get; set; }

        /// <summary>
        /// Сколько элемет=нтов пропустить.
        /// </summary>
        public int? Skip { get; set; }
    }
}
