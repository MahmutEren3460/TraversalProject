using DataAccessLayer.Concrete;
using Traversal.CQRS.Queries.DestinationQueries;
using Traversal.CQRS.Results.DestinationResult;

namespace Traversal.CQRS.Handlers.DestinationHandlers
{
    public class GetDestinationByIDQueryHandler
    {
        private readonly Context _context;

        public GetDestinationByIDQueryHandler(Context context)
        {
            _context = context;
        }

        public GetDestinationByIDQueryResult Handle(GetDestinationByIDQuery query)
        {
            var values = _context.Destinations.Find(query.id);
            return new GetDestinationByIDQueryResult
            {
                City = values.city,
                Destinationid = values.DestinationID,
                DayNight = values.DayNight,
                Price=values.Price,
                Capacity=values.Capacity
            };
        }
    }
}
