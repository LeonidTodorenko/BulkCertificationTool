using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkCertificationTool.Contracts
{
    internal interface IResult<T>
    {
        string Error { get; }
        bool HasError { get; }
        T Value { get; }
    }
}
