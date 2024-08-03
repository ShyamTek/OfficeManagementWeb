using Tawuniya.Core.Domain.Layouts;
using Tawuniya.Data;

namespace Tawuniya.Services.Layouts
{
    public partial class LayoutService : ILayoutService
    {
        #region Fields

        private readonly TawuniyaDBContext _context;

        #endregion

        #region Ctor

        public LayoutService(TawuniyaDBContext context)
        {
            _context = context;
        }

        public Task<Layout> GetLayoutByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Layout>> GetLayoutsAsync()
        {
            throw new NotImplementedException();
        }

        public Task InsertLayoutAsync(Layout layout)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods


        #endregion
    }
}
