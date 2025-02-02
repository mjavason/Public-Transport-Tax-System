namespace PTTS.Core.Shared;

public enum ErrorType
{
	BadRequest = 400,
	Unauthorized = 401,
	Forbidden = 403,
	NotFound = 404,
	PreconditionFailed = 412,
	UnprocessableEntity = 422,
	Conflict = 409,
	InternalServerError = 500,
}
