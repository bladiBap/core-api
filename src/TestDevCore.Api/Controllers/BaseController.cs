using Microsoft.AspNetCore.Mvc;
using TestDevCore.Core.Results;

namespace TestDevCore.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandlerResult(Result result)
        {
            return result.IsSuccess
                ? Ok(result)
                : HandlerResponse(result);
        }

        protected IActionResult HandlerResult<T>(Result<T> result)
        {
            return result.IsSuccess
                ? Ok(result)
                : HandlerResponse(result);
        }

        private IActionResult HandlerResponse(Result result)
        {   
            var errorType = result.Error.Type;

            return errorType switch
            {
                ErrorType.Validation => BadRequest(result),
                ErrorType.NotFound => NotFound(result),
                ErrorType.Unauthorized => Unauthorized(result),
                ErrorType.Forbidden => StatusCode(403, result),
                ErrorType.Conflict => Conflict(result),
                ErrorType.Failure => BadRequest(result),
                _ => StatusCode(500, result)
            };
        }
    }
}
