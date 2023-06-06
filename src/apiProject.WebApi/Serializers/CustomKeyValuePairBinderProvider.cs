using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiProject.WebApi.Serializers
{
	/// <inheritdoc/>
	public class CustomKeyValuePairBinderProvider : IModelBinderProvider
	{
		/// <inheritdoc/>
		public IModelBinder? GetBinder(ModelBinderProviderContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (context.Metadata.ModelType.IsGenericType &&
				context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(List<KeyValuePair<int, string>>))
			{
				return new CustomKeyValuePairBinder();
			}

			return null;
		}
	}
}
