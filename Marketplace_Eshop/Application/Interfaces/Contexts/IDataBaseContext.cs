using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken());

    }
}
