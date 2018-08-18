using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wcindicator.api.test.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }

        public static async Task<T> ReadAsJsonStrictAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error });
            if (EqualityComparer<T>.Default.Equals(value, default(T))) 
                throw new NullReferenceException();
            return value;
        }
    }
}
