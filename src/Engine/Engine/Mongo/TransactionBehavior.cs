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
            var result = await next();
            var domainEvents = _dbContext.GetDomainEvents();
            //next dispatch events here
            return result;
        }
    }
}
