using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Traversal.CQRS.Queries.DestinationQueries;
using Traversal.CQRS.Results.DestinationResult;

namespace Traversal.CQRS.Handlers.DestinationHandlers
{
    public class GetAllDestinationQueryHandler
    {
        private readonly Context _context;

        public GetAllDestinationQueryHandler(Context context)
        {
            _context = context;
        }

        public List<GetAllDestinationQueryResult> Handle(/*GetAllDestinationQuery result*/)
        {
            var values = _context.Destinations.Select(x => new GetAllDestinationQueryResult
            {
                id = x.DestinationID,
                capacity = x.Capacity,
                city = x.city,
                daynight=x.DayNight,
                price=x.Price
            }).AsNoTracking().ToList();
            return values;
        }
    }
}
