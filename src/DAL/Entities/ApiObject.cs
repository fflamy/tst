namespace ApiProject.DAL.Entities
{
	/// <summary>
	/// Объект, передаваемый через апи.
	/// </summary>
	public class ApiObject
	{
		/// <summary>
		/// Идентификатор объекта в базе.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Код объекта.
		/// </summary>
		public int Code { get; set; }

		/// <summary>
		/// Значение объекта.
		/// </summary>
		public string Value { get; set; } = string.Empty;
	}
}
