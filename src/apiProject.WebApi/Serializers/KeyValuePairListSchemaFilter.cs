using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiProject.WebApi.Serializers
{
	/// <inheritdoc/>
	public class KeyValuePairListSchemaFilter : ISchemaFilter
	{
		/// <inheritdoc/>
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			if (context.Type == typeof(List<KeyValuePair<int, string>>))
			{
				schema.Type = "array";
				schema.Items = new OpenApiSchema
				{
					Type = "object",
					Properties = new Dictionary<string, OpenApiSchema>
				{
					{ "Value", new OpenApiSchema { Type = "string", } },
				},
					Required = new HashSet<string> { "Key", "Value" },
					AdditionalPropertiesAllowed = false,
				};
			}
		}
	}
}
