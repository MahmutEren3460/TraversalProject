namespace Traversal.CQRS.Results.DestinationResult
{
    public class GetDestinationByIDQueryResult
    {
        public int Destinationid { get; set; }
        public string? City { get; set; }
        public string? DayNight { get; set; }
        public double? Price { get; set; }
        public string? Capacity { get; set; }
    }
}
