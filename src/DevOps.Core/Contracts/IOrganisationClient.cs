using DevOps.Core.Models;

namespace DevOps.Core.Contracts;

public interface IOrganisationClient
{
  Task<IEnumerable<Organisation>> GetAccounts(UserProfile profile);
}
