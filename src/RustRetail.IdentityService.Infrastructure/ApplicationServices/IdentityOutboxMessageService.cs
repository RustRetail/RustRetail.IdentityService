using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RustRetail.IdentityService.Persistence.Database;
using RustRetail.SharedApplication.Behaviors.Outbox;
using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.IdentityService.Infrastructure.ApplicationServices
{
    internal class IdentityOutboxMessageService : IOutboxMessageService
    {
        const int MAX_TAKE_SIZE = 100;

        readonly IdentityDbContext _dbContext;
        readonly ILogger<IdentityOutboxMessageService> _logger;
        readonly DbSet<OutboxMessage> _dbSet;

        public IdentityOutboxMessageService(
            IdentityDbContext dbContext,
            ILogger<IdentityOutboxMessageService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbSet = dbContext.Set<OutboxMessage>();
        }

        public async Task AddOutboxMessageAsync(OutboxMessage message, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(message, cancellationToken);
        }

        public async Task AddOutboxMessagesAsync(IEnumerable<OutboxMessage> messages, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(messages, cancellationToken);
        }

        public async Task<List<OutboxMessage>> GetErrorMessagesAsync(int takeSize = 20, bool asTracking = true, CancellationToken cancellationToken = default)
        {
            if (takeSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(takeSize), "Take size must be greater than zero.");
            }
            if (takeSize > MAX_TAKE_SIZE)
            {
                _logger.LogWarning("GetErrorMessagesAsync - Take size is larger than maximum take size ({@TakeSize} > {@MaxTakeSize}). Set the take size value to maximum.",
                    takeSize,
                    MAX_TAKE_SIZE);
                takeSize = MAX_TAKE_SIZE;
            }
            var trackingBehavior = asTracking ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return await _dbSet
                .AsTracking(trackingBehavior)
                .Where(m => m.Error != null)
                .OrderBy(m => m.OccurredOn)
                .Take(takeSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<OutboxMessage>> GetUnpublishedMessagesAsync(
            int takeSize = 20,
            bool asTracking = true,
            CancellationToken cancellationToken = default)
        {
            if (takeSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(takeSize), "Take size must be greater than zero.");
            }
            if (takeSize > MAX_TAKE_SIZE)
            {
                _logger.LogWarning("GetUnpublishedMessagesAsync - Take size is larger than maximum take size ({@TakeSize} > {@MaxTakeSize}). Set the take size value to maximum.",
                    takeSize,
                    MAX_TAKE_SIZE);
                takeSize = MAX_TAKE_SIZE;
            }
            var trackingBehavior = asTracking ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return await _dbSet
                .AsTracking(trackingBehavior)
                .Where(m => m.ProcessedOn == null && m.Error == null)
                .OrderBy(m => m.OccurredOn)
                .Take(takeSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void UpdateOutboxMessage(OutboxMessage message)
        {
            _dbSet.Update(message);
        }

        public void UpdateOutboxMessages(IEnumerable<OutboxMessage> messages)
        {
            _dbSet.UpdateRange(messages);
        }
    }
}
