using Microsoft.EntityFrameworkCore;
using Tawuniya.Core.Domain.Layout;
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

        #endregion

        #region Methods

        public async Task<IList<Layout>> GetLayoutsAsync()
        {
            return await _context.Layouts.ToListAsync();

        }
        public async Task InsertLayoutAsync(Layout layout)
        {
            if (layout == null)
                throw new ArgumentNullException(nameof(layout));

            await _context.Layouts.AddAsync(layout);
            await _context.SaveChangesAsync();
        }

        public async Task<Layout> GetLayoutByIdAsync(int id)
        {
            return await _context.Layouts.FindAsync(id);
        }

        #endregion
    }
}
