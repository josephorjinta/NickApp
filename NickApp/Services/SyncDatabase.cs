using Dotmim.Sync;
using Dotmim.Sync.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NickApp.Services
{
    public class SyncDatabase
    {
        private SyncAgent syncAgent;
       // private readonly ISyncServices __syncServices;


        public SyncDatabase()
        {
            SyncServices obj = new SyncServices();
           
            this.syncAgent = obj.GetSyncAgent();

        }
        public async Task SynchronizeDatabase()
        {
            try
            {
              
                var r = await this.syncAgent.SynchronizeAsync(SyncType.Normal);
            }
            catch (Exception ex)
            {
                string exs = ex.Message;
            }

        }
      
    }
}
