using Chat.Application.Features.User.Commands.DeleteUser;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.API.BackgroundServices
{
    public class Worker : BackgroundService
    {
        private Timer _timer;
        private readonly ConcurrentDictionary<string, CancellationTokenSource> _cancelTokens;
        private readonly IMediator _mediator;

        public Worker(IMediator mediator)
        {
            _cancelTokens = new ConcurrentDictionary<string, CancellationTokenSource>();
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

                foreach (var userId in _cancelTokens.Keys)
                {
                    stoppingToken.ThrowIfCancellationRequested();

                    // xóa user khỏi db
                    await _mediator.Send(new DeleteUserCommand { UserId = userId });

                    _cancelTokens.TryRemove(userId, out _);
                }
            }

            //_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            await base.StopAsync(stoppingToken);
        }

        public void CancelTask(string userId)
        {
            if (_cancelTokens.TryGetValue(userId, out var tokenSource))
            {
                tokenSource.Cancel();
                _cancelTokens.TryRemove(userId, out _);
            }
        }

        public void TriggerTask(string userId)
        {
            var tokenSource = new CancellationTokenSource();
            _cancelTokens.TryAdd(userId, tokenSource);
        }

        //private void DoWork(object state)
        //{

        //}
    }
}
