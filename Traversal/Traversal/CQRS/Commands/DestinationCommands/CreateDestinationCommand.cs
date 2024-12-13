namespace Traversal.CQRS.Commands.DestinationCommands
{
    public class CreateDestinationCommand
    {
        public string? city { get; set; }
        public string? DayNight { get; set; }
        public double? Price { get; set; }       
        public string? Capacity { get; set; }
    }
}
