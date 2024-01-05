namespace Imkery.Application.Common.Interfaces;
public interface IUnitOfWork
{
    Task CommitChangesAsync();
}
