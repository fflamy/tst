using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiProject.WebApi.Serializers
{
	/// <inheritdoc/>
	public class CustomKeyValuePairBinder : IModelBinder
	{
		/// <inheritdoc/>
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			var modelName = bindingContext.ModelName;
			var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

			if (valueProviderResult == ValueProviderResult.None)
			{
				return Task.CompletedTask;
			}

			var json = valueProviderResult.FirstValue;
			if (string.IsNullOrEmpty(json))
			{
				return Task.CompletedTask;
			}

			try
			{
				var list = new List<KeyValuePair<int, string>>();
				JArray array = JArray.Parse(json);

				foreach (JObject item in array.Children<JObject>())
				{
					var kvp = item.Properties()
						.Select(p => new KeyValuePair<int, string>(
							int.Parse(p.Name),
							p.Value.ToString()))
						.FirstOrDefault();

					if (!Equals(kvp, default(KeyValuePair<int, string>)))
					{
						list.Add(kvp);
					}
				}

				bindingContext.Result = ModelBindingResult.Success(list);
			}
			catch (JsonReaderException)
			{
				bindingContext.Result = ModelBindingResult.Failed();
			}

			return Task.CompletedTask;
		}
	}
}