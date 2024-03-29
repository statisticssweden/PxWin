﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PX.Security
{
    public interface IAuthorization
    {
        bool IsAuthorized(System.Security.Principal.IIdentity id, string dbid, string menu, string selection);
        bool IsAuthorized(string dbid);
    }
}
