using DataAccessLayer.Concrete;
using Traversal.CQRS.Commands.DestinationCommands;

namespace Traversal.CQRS.Handlers.DestinationHandlers
{
    public class UpdateDestinationCommandHandler
    {
        private readonly Context _context;

        public UpdateDestinationCommandHandler(Context context)
        {
            _context = context;
        }
        public void Handle(UpdateDestinationCommand command)
        {
            var values = _context.Destinations.Find(command.Destinationid);
            values.city = command.City;
            values.DayNight = command.DayNight;
            values.Price = command.Price;
            values.Capacity = command.Capacity;
            _context.SaveChanges();
        }
    }
}
