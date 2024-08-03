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
        Task<object?> GetByIdAsync(string url, int id);
    }
}
