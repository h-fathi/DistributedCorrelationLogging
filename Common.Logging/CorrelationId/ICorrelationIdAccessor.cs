using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Logging
{

    public interface ICorrelationIdAccessor
    {
        string GetCorrelationId();
    }


}
