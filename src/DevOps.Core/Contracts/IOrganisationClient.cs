using DevOps.Core.Models;

namespace DevOps.Core.Contracts;

public interface IOragnisationClient
{
  Task<IEnumerable<Organisation>> GetAccounts(UserProfile profile);
}
