using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePass.Audit
{
    public interface IAuditLogger
    {
        void Log(string info);

        string GetLastLine();
    }
}
