using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Services.Security
{
    public interface IPasswordHasher
    {
        string Hash(string password, int iterations = 1000);

        (bool Verified, bool NeedsUpgrade) Check(string hash, string password);

        bool IsValid(string testPassword, string origDelimHash);
    }
}
