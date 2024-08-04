using Newtonsoft.Json;
using System.Net;
using Tawuniya.Core.Domain.Employees;

namespace Tawuniya.Services.Common
{
    public partial class CommonAPIService : ICommonAPIService
    {
        /// <summary>
        /// api post method
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="jsonBody">json body</param>
        /// <returns></returns>
        public async Task<string> ApiPostAsync(string url, string jsonBody)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("accept", "*/*");
            var content = new StringContent(jsonBody, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var registrationResponse = await response.Content.ReadAsStringAsync();

            return registrationResponse;
        }

        /// <summary>
        /// api get object by id
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<string> GetByIdAsync(string url, int id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}?id={id}");
            request.Headers.Add("accept", "*/*");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var detailResponse = await response.Content.ReadAsStringAsync();

            return detailResponse;
        }

        /// <summary>
        /// Delete entity async
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteEntityAsync(string url, int id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{url}?id={id}");
            request.Headers.Add("accept", "*/*");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();            

            return response;
        }

        /// <summary>
        /// get list of entity
        /// </summary>
        /// <param name="url">api url</param>
        /// <returns></returns>
        public async Task<string> EntityListAsync(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "*/*");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
