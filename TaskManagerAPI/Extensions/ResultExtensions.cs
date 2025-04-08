using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Common;

namespace TaskManagerAPI.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            return result.IsSuccess
                ? new OkObjectResult(result.Data)
                : new BadRequestObjectResult(new { error = result.Error });
        }
    }
}
