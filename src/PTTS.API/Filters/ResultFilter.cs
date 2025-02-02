using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PTTS.API.Filters.Model;
using PTTS.Core.Shared;

namespace ShopAllocationPortal.API.Filters;

public class ResultFilter : ResultFilterAttribute
{
	private readonly ILogger<ResultFilter> _logger;

	public ResultFilter(ILogger<ResultFilter> logger)
	{
		_logger = logger;
	}

	public override void OnResultExecuting(ResultExecutingContext context)
	{
		switch (context.Result)
		{
			case OkObjectResult okObjectResult:
				context.Result = GetSuccessResponse(HttpStatusCode.OK, okObjectResult);
				break;
			case CreatedResult createdResult:
				context.Result = GetSuccessResponse(HttpStatusCode.Created, createdResult);
				break;
			case ObjectResult objectResult:
				context.Result = GetErrorResponse(objectResult);
				break;
			default:
				break;
		}
		base.OnResultExecuting(context);
	}

	private static JsonResult GetSuccessResponse(HttpStatusCode statusCode, ObjectResult objectResult)
	{
		if (objectResult.Value is not SuccessResult result)
		{
			return new JsonResult(new SuccessResponse<object> { Data = objectResult.Value })
			{
				StatusCode = (int)statusCode
			};
		}
		if (Equals(result.Data, null))
		{
			return new JsonResult(new SuccessResponse { Status = (int)statusCode, Message = result.Message }) { StatusCode = (int)statusCode };
		}
		var response = new SuccessResponse<object>()
		{
			Status = (int)statusCode,
			Message = result.Message ?? "",
			Data = result.Data
		};
		return new JsonResult(response) { StatusCode = (int)statusCode };
	}

	private JsonResult GetErrorResponse(ObjectResult objectResult)
	{
		var statusCode = (HttpStatusCode)(objectResult.StatusCode ?? 500);

		if ((int)statusCode < 400)
		{
			return new JsonResult(objectResult) { StatusCode = (int)statusCode };
		}

		var errors = new List<string>();
		switch (objectResult.Value)
		{
			case ValidationProblemDetails:
				_logger.LogInformation("ValidationProblemDetails: {@data}", objectResult.Value);
				errors = new List<string> { "The data you provided is invalid or not properly formatted. Please check for errors and try again." };
				break;
			case List<string> errorsList:
				errors = errorsList;
				break;
		}

		var errorResponse = new ErrorResponse
		{
			Status = (int)statusCode,
			Message = errors?.FirstOrDefault() ?? statusCode.ToString(),
			Type = Enum.GetName(statusCode)!,
			Errors = errors
		};

		return new JsonResult(errorResponse) { StatusCode = (int)statusCode };
	}
}
