﻿using Domain.Common.Result;
using Domain.Kernel;

namespace Domain.Core.Queue;

public interface IQueueRepository
{
    Task<Result<QueueEntity>> FindByAsync(
        Specification<QueueEntity> specification,
        CancellationToken cancellationToken);

    Task InsertRangeAsync(IReadOnlyCollection<QueueEntity> queues, CancellationToken cancellationToken);

    void Update(QueueEntity queue);

    Task<long> CountAsync(CancellationToken cancellationToken);
}