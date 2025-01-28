namespace PTTS.Core.Domain.Common;

public interface IUnitOfWork
{
	Task SaveChangesAsync(CancellationToken cancellationToken);
}
