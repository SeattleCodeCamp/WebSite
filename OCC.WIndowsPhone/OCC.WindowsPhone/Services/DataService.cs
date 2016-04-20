#region License, Terms and Author(s)
//
// Orlando Code Camp for Windows Phone 7
// Copyright (c) 2012 Orlando .Net User Group. All rights reserved.
//
//  Author(s):
//
//      Brian Mishler, http://www.qualitydata.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Data.Services.Client;
using OCC.WindowsPhone.OrlandoCodeCampService;

namespace OCC.WindowsPhone.Services
{
    /// <summary>
    /// Not currently being used... This is my normal pattern for calling async however it does feel compatible with oData
    /// </summary>
    public class DataService
    {

        public void GetSessions(Action<DataServiceCollection<Session>> onSuccess, Action<bool> onBusyStateChanged, Action<Exception> onError)
        {
            var sessions = new DataServiceCollection<Session>(null, TrackingMode.None);

            sessions.LoadCompleted += (s, e) =>
                                         {
                                             DispatchBusyStateChanged(onBusyStateChanged, false);
                                             if (e.Cancelled) return;
                                             if (e.Error != null)
                                                 DispatchError(e.Error, onError);
                                             else
                                                 onSuccess(sessions);
                                         };

            DispatchBusyStateChanged(onBusyStateChanged, true);
            sessions.LoadAsync(Context.Sessions);

        }
        void DispatchBusyStateChanged(Action<bool> onBusyStateChanged, bool isBusy)
        {
            if (onBusyStateChanged != null)
                onBusyStateChanged(isBusy);
        }
        void DispatchError(Exception ex, Action<Exception> onError)
        {
            if (onError != null)
                onError(ex);
        }
        public bool IsDataLoaded { get; set; }

        OrlandoCodeCampEntities _context;
        public OrlandoCodeCampEntities Context
        {
            get
            {
                return _context ?? (_context = new OrlandoCodeCampEntities(Uri));
            }
        }
        static Uri Uri
        {
            get
            {
                // todo: move this to....
                const string url = "http://odata.orlandocodecamp.com/DataService.svc";
                return new Uri(url, UriKind.Absolute);
            }
        }

    }
}
