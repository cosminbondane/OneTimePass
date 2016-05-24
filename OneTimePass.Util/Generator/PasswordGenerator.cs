using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePass.Util.Generator
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string Generate()
        {
            Guid randomGuid = Guid.NewGuid();
            long ticks = DateTime.UtcNow.Ticks;

            string newPasswordKey = string.Concat(randomGuid.ToString("N"), ticks);
            byte[] bytes = Encoding.UTF8.GetBytes(newPasswordKey);

            string base64Password = Convert.ToBase64String(bytes);
            base64Password = base64Password.TrimEnd('=');

            return base64Password;
        }
    }
}
