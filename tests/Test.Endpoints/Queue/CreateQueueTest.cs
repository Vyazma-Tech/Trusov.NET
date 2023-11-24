﻿using System.Net;
using Application.Handlers.Queue.Commands.CreateQueue;
using Domain.Kernel;
using FastEndpoints;
using FluentAssertions;
using Infrastructure.Tools;
using Mediator;
using Moq;
using Presentation.Endpoints.Queue.CreateQueues;
using Test.Endpoints.Fixtures.WebFactory;
using Xunit;

namespace Test.Endpoints.Queue;

[Collection(nameof(WebAppFactoryCollectionFixture))]
public class CreateQueueTest
{
    private readonly HttpClient _client;
    private readonly CreateQueuesEndpoint _endpoint;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateQueueTest(WebAppFactory factory)
    {
        IMediator mediator = new Mock<IMediator>().Object;
        _endpoint = new CreateQueuesEndpoint(mediator);
        _client = factory.CreateClient();
        _dateTimeProvider = new DateTimeProvider();
    }

    [Fact]
    public async Task SendCreateQueuesRequest_ShouldReturn200_WhenEverythingFine()
    {
        var command = new CreateQueuesCommand(new QueueModel[]
        {
            new (
                10,
                _dateTimeProvider.UtcNow.AddDays(1),
                TimeOnly.FromDateTime(_dateTimeProvider.UtcNow.AddHours(2)),
                TimeOnly.FromDateTime(_dateTimeProvider.UtcNow.AddHours(4)))
        });

        TestResult<CreateQueuesResponse> response = await _client
            .POSTAsync<CreateQueuesEndpoint, CreateQueuesCommand, CreateQueuesResponse>(command);

        response.Result.Should().NotBeNull();
        response.Result.Queues.Should().NotBeEmpty();
        response.Response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}