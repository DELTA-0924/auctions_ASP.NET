using Auctuons_core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.Abstractions
{
    public interface ITokenProvider
    {
        string GenerateToken(User user);
    }
}
