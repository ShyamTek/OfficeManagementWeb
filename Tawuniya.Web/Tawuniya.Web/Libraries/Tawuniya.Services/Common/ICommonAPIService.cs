using System.Net;

namespace Tawuniya.Services.Common
{
    public partial interface ICommonAPIService
    {
        /// <summary>
        /// api post method
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="jsonBody">json body</param>
        /// <returns></returns>
        Task<string> ApiPostAsync(string url, string jsonBody);

        /// <summary>
        /// api get object by id
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<string> GetByIdAsync(string url, int id);

        /// <summary>
        /// Delete entity async
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteEntityAsync(string url, int id);

        /// <summary>
        /// get list of entity
        /// </summary>
        /// <param name="url">api url</param>
        /// <returns></returns>
        Task<string> EntityListAsync(string url);
    }
}
