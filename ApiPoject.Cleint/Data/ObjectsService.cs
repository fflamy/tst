using ApiProject.Core.Entities;
using Newtonsoft.Json;

namespace ApiPoject.Cleint.Data
{
    public class ObjectsService
    {
        const string serverUri = "https://localhost:8346/api/Objects";
        public async Task<List<ApiObject>> FetchDataAsync(int take = 10, int skip = 0)
        {

            using (HttpClient client = new())
            {
                try
                {

                    HttpResponseMessage response = await client.GetAsync(serverUri+$"?take={take}&skip={skip}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<ApiObject>>(responseContent);
                        return result ?? new List<ApiObject>();
                    }
                    else
                    {
                        return new List<ApiObject>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                return new List<ApiObject>();
            }
        }

        public async Task<bool> PostObjects(List<KeyValuePair<int, string>> objects)
        {
            string json = JsonConvert.SerializeObject(objects, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new KeyValuePairConverter() }
            });

            using (HttpClient client = new())
            {
                try
                {
                    HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(serverUri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                return false;
            }
        }
    }
}