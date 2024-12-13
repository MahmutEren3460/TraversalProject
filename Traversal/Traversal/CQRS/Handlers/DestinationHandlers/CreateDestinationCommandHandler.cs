using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Traversal.CQRS.Commands.DestinationCommands;

namespace Traversal.CQRS.Handlers.DestinationHandlers
{
    public class CreateDestinationCommandHandler
    {
        private readonly Context _context;

        public CreateDestinationCommandHandler(Context context)
        {
            _context = context;
        }
        public void Handle(CreateDestinationCommand command)
        {
            _context.Destinations.Add(new Destination
            {
                city = command.city,
                Price=command.Price,
                DayNight=command.DayNight,
                Capacity=command.Capacity,
                status=true
            });
            _context.SaveChanges();
        }
    }
}
