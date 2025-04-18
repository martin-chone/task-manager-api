using Microsoft.AspNetCore.Mvc;
using TaskManager.Shared;

namespace TaskManager.API.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            return result.IsSuccess
                ? new OkObjectResult(result.Data)
                : new BadRequestObjectResult(new { error = result.Error });
        }

        public static IActionResult ToActionResult(this Result result)
        {
            return result.IsSuccess
                ? new OkResult()
                : new BadRequestObjectResult(new { error = result.Error });
        }
    }
}
