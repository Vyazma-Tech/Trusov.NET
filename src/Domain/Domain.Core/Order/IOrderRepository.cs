﻿using Domain.Common.Result;
using Domain.Kernel;

namespace Domain.Core.Order;

public interface IOrderRepository
{
    Task<Result<OrderEntity>> FindByAsync(
        Specification<OrderEntity> specification,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<OrderEntity>> FindAllByAsync(
        Specification<OrderEntity> specification,
        CancellationToken cancellationToken);

    Task InsertRangeAsync(IReadOnlyCollection<OrderEntity> orders, CancellationToken cancellationToken);

    void Update(OrderEntity order);

    Task<long> CountAsync(CancellationToken cancellationToken);
}