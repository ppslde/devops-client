using DevOps.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Core.Contracts;

public interface IOragnisationClient
{
    Task<IEnumerable<Oragnisation>> GetAccounts(UserProfile profile);
}
