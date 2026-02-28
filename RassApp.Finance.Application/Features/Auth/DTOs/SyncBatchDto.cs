using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.Finance.Application.Features.Sync;

public class SyncBatchDto
{
    public long Version { get; set; }
    public List<object> Changes { get; set; } = new();
}
