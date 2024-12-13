using DataAccessLayer.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Traversal.CQRS.Queries.GuideQueries;
using Traversal.CQRS.Results.GuideResult;

namespace Traversal.CQRS.Handlers.GuideHandlers
{
    public class GetAllGuideQueryHandler: IRequestHandler<GetAllGuideQuery, List<GetAllGuideQueryResult>>
    {
        private readonly Context _context;
        public GetAllGuideQueryHandler(Context context)
        {
            _context = context;
        }
        public async Task<List<GetAllGuideQueryResult>> Handle(GetAllGuideQuery request, CancellationToken cancellationToken)
        {
            return await _context.Guides.Select(x => new GetAllGuideQueryResult
            {
                GuideID = x.GuideID,
                Descrition = x.Descrition,
                Image = x.Image,
                Name = x.Name
            }).AsNoTracking().ToListAsync();
        }
    }
}
