using System;
using System.Collections.Generic;
using System.Text;

namespace RassApp.Finance.Application.Features.Sync;

public interface ISyncService
{
    Task ApplyChanges(SyncBatchDto batch);
    Task<object> GetChanges(long lastVersion);
}
