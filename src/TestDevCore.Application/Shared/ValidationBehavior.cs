using MediatR;
using TestDevCore.Core.Results;

namespace TestDevCore.Application.Shared
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        public ValidationBehavior()
        {
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (DomainException ex)
            {
                return CreateFailureResult(ex.Error);
            }
            catch (Exception ex)
            {
                var genericError = Error.Failure(
                    "Server.InternalError",
                    "Error Server.");

                //Result.Failure(genericError);
                return CreateFailureResult(genericError);
            }
        }

        private static TResponse CreateFailureResult(Error error)
        {
            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                Type valueType = typeof(TResponse).GetGenericArguments()[0];

                var method = typeof(Result)
                    .GetMethods()
                    .First(m => m.Name == nameof(Result.Failure) && m.IsGenericMethod)
                    .MakeGenericMethod(valueType);

                return (TResponse)method.Invoke(null, new object[] { error })!;
            }

            return (TResponse)(object)Result.Failure(error);
        }
    }
}
