using MediatR;
using Traversal.CQRS.Results.GuideResult;

namespace Traversal.CQRS.Queries.GuideQueries
{
    public class GetAllGuideQuery: IRequest<List<GetAllGuideQueryResult>>
    {
    }
}
