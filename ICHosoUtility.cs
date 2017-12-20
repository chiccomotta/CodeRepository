using System.Collections.Generic;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace Repower.LeMA.CHoso.Infrastructure
{
    public interface ICHosoUtility
    {
        List<KeyValuePair<string, string>> BuildKeyValuePair<T>(T instance) where T : class, new();
    }
}