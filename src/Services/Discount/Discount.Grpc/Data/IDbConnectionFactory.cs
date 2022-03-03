using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
