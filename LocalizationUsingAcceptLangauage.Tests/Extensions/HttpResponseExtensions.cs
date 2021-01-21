using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationUsingAcceptLangauage.Tests.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> ReadBody<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
