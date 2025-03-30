using NyTimes.Domain.Interfaces;
using NyTimes.Infrastructure.DatabaseContext;

namespace NyTimes.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(MainDbContext context)
        {
            _context = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repositoryType = typeof(GenericRepository<>);
                _repositories[typeof(T)] = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            }
            return (IRepository<T>)_repositories[typeof(T)];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
