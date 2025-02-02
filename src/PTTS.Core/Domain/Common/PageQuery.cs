namespace PTTS.Core.Domain.Common;

public class PageQuery
{
	public string? Search { get; set; }

	public int Page { get; set; } = 1;

	public int PageSize { get; set; } = 10;

	public DateOnly? StartDate { get; set; }

	public DateOnly? EndDate { get; set; }
}
