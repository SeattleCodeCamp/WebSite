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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using OCC.WindowsPhone.OrlandoCodeCampService;
using QualityData.Shared.Extensions;

namespace OCC.WindowsPhone.ViewModels
{
    public class TrackViewModel : ViewModelBase
    {
        public TrackViewModel()
        {
            Sessions = new ObservableCollection<Session>();
        }
        public Track Track
        {
            get { return Get(() => Track); }
            private set
            {
                Set(() => Track, value);

                // look up time slots and sessions for this track (Kludge?)
                var sessions = App.MainViewModel.Sessions.Where(s => s.Track_ID == Track.ID).ToList();
                sessions.ForEach(s =>
                                           {
                                               s.Speaker = App.MainViewModel.Speakers.FirstOrDefault(sp => sp.ID == s.Speaker_ID);
                                               s.Timeslot = App.MainViewModel.Timeslots.FirstOrDefault(t => t.ID == s.Timeslot_ID);
                                           });

                // sort and load sessions
                Sessions.Clear();
                sessions.OrderBy(s => s.StartTime).ForEach(s => Sessions.Add(s));
            }
        }
        public ObservableCollection<Session> Sessions
        {
            get;
            private set;
        }

        public void Load(int id)
        {
            var track = App.MainViewModel.Tracks.FirstOrDefault(t => t.ID == id);
            if (track == null)
            {
                MessageBox.Show("Invalid Track Id");
                return;
            }
            Track = track;
        }
    }
}
