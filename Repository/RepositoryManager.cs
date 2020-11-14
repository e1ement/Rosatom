using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        private IValueRepository _valueRepository;
        private IWorkRepository _workRepository;

        public IValueRepository ValueRepository =>
            _valueRepository ??= new ValueRepository(_repositoryContext);

        public IWorkRepository WorkRepository =>
            _workRepository ??= new WorkRepository(_repositoryContext);
    }
}
