using MediatR;

namespace Engine.Mongo
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        private readonly IMongoDbContext _dbContext;

        public TransactionBehavior(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await _dbContext.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await next();
                await _dbContext.CommitTransactionAsync(cancellationToken);
                return result;
            }
            catch
            {
                await _dbContext.RollbackTransaction(cancellationToken);
                throw;
            }
        }
    }
}
