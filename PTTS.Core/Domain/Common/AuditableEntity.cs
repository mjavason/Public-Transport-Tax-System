namespace PTTS.Core.Domain.Common;

public interface IAuditableEntity
{
	public DateTimeOffset DateCreated { get; set; }
	public DateTimeOffset DateModified { get; set; }
}
