namespace Contracts
{
    public interface IRepositoryManager
    {
        IValueRepository ValueRepository { get; }
        IWorkRepository WorkRepository { get; }
    }
}
