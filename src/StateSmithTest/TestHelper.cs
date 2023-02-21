using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace StateSmithTest;

public class TestHelper
{
    public static string GetThisDir([System.Runtime.CompilerServices.CallerFilePath] string? callerFilePath = null)
    {
        return Path.GetDirectoryName(callerFilePath) + "/";
    }
}
