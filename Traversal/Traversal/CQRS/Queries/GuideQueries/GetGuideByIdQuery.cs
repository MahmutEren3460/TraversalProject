﻿using MediatR;
using Traversal.CQRS.Results.GuideResult;

namespace Traversal.CQRS.Queries.GuideQueries
{
    public class GetGuideByIdQuery:IRequest<GetGuideByIdQueryResult>
    {
        public GetGuideByIdQuery(int ıd)
        {
            Id = ıd;
        }

        public int Id { get; set; }
    }
}