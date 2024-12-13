using DataAccessLayer.Concrete;
using MediatR;
using Traversal.CQRS.Queries.GuideQueries;
using Traversal.CQRS.Results.GuideResult;

namespace Traversal.CQRS.Handlers.GuideHandlers
{
    public class GetGuideByIDQueryHandler : IRequestHandler<GetGuideByIdQuery, GetGuideByIdQueryResult>
    {
        private readonly Context _context;

        public GetGuideByIDQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<GetGuideByIdQueryResult> Handle(GetGuideByIdQuery request, CancellationToken cancellationToken)
        {
            var values= await _context.Guides.FindAsync(request.Id);
            return new GetGuideByIdQueryResult
            {
                GuideID = values.GuideID,
                Descrition = values.Descrition,
                Name = values.Name,
                Image = values.Image
            };
        }
    }
}
