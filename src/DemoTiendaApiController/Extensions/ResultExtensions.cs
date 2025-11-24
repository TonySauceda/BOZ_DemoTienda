using DemoTienda.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DemoTienda.Api.Extensions;

public static class ResultExtensions
{
    extension(Result result)
    {
        public ObjectResult ToObjectResult()
        {
            var problemDetails = new ProblemDetails
            {
                Title = result.Title,
                Status = (int)result.StatusCode
            };
            problemDetails.Extensions.Add("errors", result.Error?.ToDictionary());

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status.Value
            };
        }
    }
}