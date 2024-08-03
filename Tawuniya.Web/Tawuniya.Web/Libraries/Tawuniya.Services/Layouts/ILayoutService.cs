using Tawuniya.Core.Domain.Layout;

namespace Tawuniya.Services.Layouts
{
    public partial interface ILayoutService
    {
        Task<IList<Layout>> GetLayoutsAsync();

        Task InsertLayoutAsync(Layout layout);

        Task<Layout> GetLayoutByIdAsync(int id);
    }
}
