using Dotmim.Sync;
using System.Net.Http;

namespace NickApp.Services
{
    public interface ISyncServices
    {
        SyncAgent GetSyncAgent();
        HttpClient GetHttpClient();
    }
}