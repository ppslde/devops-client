using DevOps.Core.Models;

namespace DevOps.Core.Contracts;

public interface IOragnisationClient
{
  Task<IEnumerable<Oragnisation>> GetAccounts(UserProfile profile);
}
