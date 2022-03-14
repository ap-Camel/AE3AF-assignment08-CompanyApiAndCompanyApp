using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using Newtonsoft.Json;
using Xamarin.Essentials;

namespace CompanyAppTask.Services
{
    class CompanyAppServices
    {

        private const string apiUrl = "http://192.168.85.1:5012";

        public static async Task<List<T>> GetItems<T>(string controller) where T : new()
        {
            List<T> list = new List<T>();

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string fullUrl = $"{apiUrl}/{controller}";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.
                    Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<T>>(json);
                }
            }

            return list;
        }

        public static async Task<T> GetItem<T>(string controller) where T : new()
        {
            T item = new T();

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string fullUrl = $"{apiUrl}/{controller}";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.
                    Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<T>(json);
                }
            }

            return item;
        }

        public static async Task<bool> AddItem<T>(string controller, T item) where T : new()
        {
            bool success = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string fullUrl = $"{apiUrl}/{controller}";
                HttpClient client = new HttpClient();

                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(fullUrl, content);
                success = response.IsSuccessStatusCode;
            }

            return success;
        }

        public static async Task<bool> UpdateItem<T>(string controller, T item, int id) where T : new()
        {
            bool success = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string fullUrl = $"{apiUrl}/{controller}/{id}";
                HttpClient client = new HttpClient();

                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(fullUrl, content);
                success = response.IsSuccessStatusCode;
            }

            return success;
        }

        public static async Task<bool> DeleteItem(string controller, int id)
        {
            bool success = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string fullUrl = $"{apiUrl}/{controller}/{id}";
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.DeleteAsync(fullUrl);
                success = response.IsSuccessStatusCode;
            }

            return success;
        }

    }
}
