﻿using Application.Core.Common;
using Application.Core.Contracts;

namespace Application.Handlers.Queue.Queries;

public class QueueQuery : IQuery<QueueResponse>
{
    public QueueQuery(Guid? orderId, Guid? queueId, DateTime? assignmentDate)
    {
        OrderId = orderId;
        QueueId = queueId;
        AssignmentDate = assignmentDate;
    }

    public static QueryBuilder Builder => new QueryBuilder();
    public Guid? OrderId { get; set; }
    public Guid? QueueId { get; set; }
    public DateTime? AssignmentDate { get; set; }

    public sealed class QueryBuilder
    {
        private Guid? _orderId;
        private Guid? _queueId;
        private DateTime? _assignmentDate;
        
        public QueryBuilder WithOrderId(Guid orderId)
        {
            _orderId = orderId;
            return this;
        }

        public QueryBuilder WithQueueId(Guid queueId)
        {
            _queueId = queueId;
            return this;
        }

        public QueryBuilder WithAssignmentDate(DateTime assignmentDate)
        {
            _assignmentDate = assignmentDate;
            return this;
        }

        public QueueQuery Build()
        {
            return new QueueQuery(_orderId, _queueId, _assignmentDate);
        }
    }
}