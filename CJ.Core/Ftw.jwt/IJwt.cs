using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Core.Ftw.jwt
{
    public interface IJwt
    {
        string GetToken(Dictionary<string, string> Clims);
        bool ValidateToken(string Token, out Dictionary<string, string> Clims);
    }
}
