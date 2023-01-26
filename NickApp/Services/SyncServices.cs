﻿using Dotmim.Sync;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NickApp.Services
{
    public class SyncServices : ISyncServices
    {

        private SqliteSyncProvider sqliteSyncProvider;
        private SyncAgent syncAgent;
        private WebRemoteOrchestrator webProxyProvider;
        private HttpClient httpClient;

        private ISettingServices settings;

        public SyncServices()
        {
            SettingServices stObj = new SettingServices();

            this.settings = stObj; // DependencyService.Get<ISettingServices>();
            var syncApiUri = new Uri(this.settings.SyncApiUrl);
            
            this.httpClient = new HttpClient();

//#if DEBUG
//            var handler = DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler();
//#else
//            var handler = new HttpClientHandler();
//#endif
            var handler = new HttpClientHandler();

            handler.AutomaticDecompression = DecompressionMethods.GZip;

            this.httpClient = new HttpClient(handler);

            // Check if we are trying to reach a IIS Express.
            // IIS Express does not allow any request other than localhost
            // So far,hacking the Host-Content header to mimic localhost call
            //if (Device.RuntimePlatform == Device.Android && syncApiUri.Host == "10.0.2.2")
            //    this.httpClient.DefaultRequestHeaders.Host = $"localhost:{syncApiUri.Port}";

            this.httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            this.webProxyProvider = new WebRemoteOrchestrator(this.settings.SyncApiUrl, client:this.httpClient);

            this.sqliteSyncProvider = new SqliteSyncProvider(this.settings.DataSource);
            
            var clientOptions = new SyncOptions { BatchSize = settings.BatchSize, BatchDirectory = settings.BatchDirectoryPath };
            
            this.syncAgent = new SyncAgent(sqliteSyncProvider, webProxyProvider, clientOptions);

          
        }

        public SyncAgent GetSyncAgent() => this.syncAgent;

        public HttpClient GetHttpClient() => this.httpClient;

    }
}
