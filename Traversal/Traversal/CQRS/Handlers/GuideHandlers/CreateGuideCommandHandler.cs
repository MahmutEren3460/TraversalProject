using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using MediatR;
using Traversal.CQRS.Commands.GuideCommands;

namespace Traversal.CQRS.Handlers.GuideHandlers
{
    public class CreateGuideCommandHandler : IRequestHandler<CreateGuideCommand,Unit>
    {
        private readonly Context _context;

        public CreateGuideCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateGuideCommand request, CancellationToken cancellationToken)
        {
            _context.Guides.Add(new Guide
            {
                Name = request.Name,
                Descrition = request.Descrition,
                Status=true,
                Image=request.Image

            });
            await _context.SaveChangesAsync();

            return Unit.Value;
        }

    }
}
