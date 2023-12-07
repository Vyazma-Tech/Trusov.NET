﻿using Domain.Common.Errors;
using Domain.Common.Result;
using Domain.Core.Order;
using Domain.Core.Subscription;
using Domain.Core.User;
using Domain.Core.ValueObjects;
using FluentAssertions;
using Test.Core.Domain.Entities.ClassData;
using Xunit;

namespace Test.Core.Domain.Entities;

public class SubscriptionTests
{
    [Fact]
    public void CreateSubscription_Should_ReturnNotNullSubscriber()
    {
        var creationDate = DateOnly.FromDateTime(DateTime.UtcNow);
        UserEntity user = UserClassData.Create();

        var subscription = new SubscriptionEntity(
            user,
            creationDate);

        subscription.Should().NotBeNull();
        subscription.Orders.Should().BeEmpty();
        subscription.CreationDate.Should().Be(creationDate);
        subscription.ModifiedOn.Should().BeNull();
        subscription.Queue.Should().BeNull();
    }

    [Theory]
    [ClassData(typeof(SubscriptionClassData))]
    public void SubscribeOrder_ShouldReturnSuccessResult_WhenOrderIsNotInSubscription(
        SubscriptionEntity subscription,
        OrderEntity order)
    {
        Result<OrderEntity> entranceResult = subscription.Subscribe(order);

        entranceResult.IsSuccess.Should().BeTrue();
        subscription.Orders.Should().Contain(order);
    }

    [Theory]
    [ClassData(typeof(SubscriptionClassData))]
    public void UnsubscribeOrder_ShouldReturnFailureResult_WhenUserOrderIsNotInSubscription(
        SubscriptionEntity subscription,
        OrderEntity order)
    {
        Result<OrderEntity> quitResult = subscription.Unsubscribe(order);

        quitResult.IsFaulted.Should().BeTrue();
        quitResult.Error.Message.Should().Be(DomainErrors.Subscription.OrderIsNotInSubscription(order.Id).Message);
    }
}