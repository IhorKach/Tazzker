using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazzker.Application.Interfaces
{
    public interface IUserContext
    {
        Guid UserId { get; }
        string UserName { get; }
    }
}
