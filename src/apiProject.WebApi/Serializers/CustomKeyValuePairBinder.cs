using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiProject.WebApi.Serializers
{
    /// <inheritdoc/>
    public class CustomKeyValuePairBinder : IModelBinder
    {
        /// <inheritdoc/>
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string json;
            using (var sr = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                json = await sr.ReadToEndAsync();
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
        }
    }
}