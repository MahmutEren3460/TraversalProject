using MediatR;

namespace Traversal.CQRS.Commands.GuideCommands
{
    public class CreateGuideCommand:IRequest<Unit>
    {
        public string? Name { get; set; }
        public string? Descrition { get; set; }
        public string? Image { get; set; }
        public bool? Status { get; set; }
    }
}
