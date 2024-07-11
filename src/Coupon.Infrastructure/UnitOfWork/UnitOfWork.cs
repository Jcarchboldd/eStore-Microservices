namespace Coupon.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CouponContext _context;
        private readonly IMediator _mediator;
        private IDbContextTransaction? _transaction;

        public IPromoCodeRepository PromoCodeRepository { get; }

        public UnitOfWork(CouponContext context, IPromoCodeRepository promoCodeRepository, IMediator mediator)
        {
            _context = context;
            PromoCodeRepository = promoCodeRepository;
            _mediator = mediator;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction started. Call BeginTransactionAsync before CommitAsync.");
            }

            try
            {
                await _mediator.DispatchDomainEventsAsync(_context);
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await RollbackTransactionAsync();
                throw new Exception("An error occurred while committing the transaction. Transaction has been rolled back.", ex);
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction started. Call BeginTransactionAsync before RollbackAsync.");
            }

            try
            {
                await RollbackTransactionAsync();
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        private async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<bool> SaveEntitiesAsync()
        {
            await _mediator.DispatchDomainEventsAsync(_context);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

