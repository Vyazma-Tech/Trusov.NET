﻿namespace Application.Handlers.Order.Queries;

public record struct OrderResponseModel(
    Guid Id,
    bool Paid,
    bool Ready,
    DateTime? ModifiedOn,
    DateTime CreationDate);

public sealed record OrderResponse(OrderResponseModel Queue);