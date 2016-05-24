using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePass.Audit
{
    public class AuditLogger : IAuditLogger
    {
        private List<string> logs = new List<string>();

        public void Log(string info)
        {
            if (string.IsNullOrEmpty(info))
            {
                return;
            }

            logs.Add(info);
        }

        public string GetLastLine()
        {
            if (logs.Count > 0)
            {
                return logs.Last();
            }

            return string.Empty;
        }
    }
}
