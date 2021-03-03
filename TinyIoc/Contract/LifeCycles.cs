using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyIoc.Contract
{
    internal enum LifeCycles
    {
        Singleton,
        Transient,
        Scope,
    }
}
