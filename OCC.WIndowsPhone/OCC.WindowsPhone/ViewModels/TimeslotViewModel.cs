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
    public class TimeslotViewModel : ViewModelBase
    {
        public TimeslotViewModel()
        {
            Sessions = new ObservableCollection<Session>();
        }
        public Timeslot Timeslot
        {
            get { return Get(() => Timeslot); }
            private set
            {
                Set(() => Timeslot, value);

                // look up sessions for this timeslot then get speaker/track for each (Kludge?)
                var sessions = App.MainViewModel.Sessions.Where(s => s.Timeslot_ID == Timeslot.ID).ToList();
                sessions.ForEach(s =>
                {
                    s.Speaker = App.MainViewModel.Speakers.FirstOrDefault(sp => sp.ID == s.Speaker_ID);
                    s.Track = App.MainViewModel.Tracks.FirstOrDefault(t => t.ID == s.Track_ID);
                });

                // sort and load sessions
                Sessions.Clear();
                sessions.OrderBy(s => s.Track.Name).ForEach(s => Sessions.Add(s)); 

                // retrieve lunch sponsor information
                if (Timeslot.IsLunchPeriod)
                {
                    LunchSponsor = App.MainViewModel.Sponsors.FirstOrDefault(s => s.SponsorshipLevel.ToLower().Contains("lunch"));
                }

            }
        }
        public ObservableCollection<Session> Sessions
        {
            get;
            private set;
        }

        public int Id { get; private set; }


        public void Load(int id)
        {
            var timeslot = App.MainViewModel.Timeslots.FirstOrDefault(t => t.ID == id);
            if (timeslot == null)
            {
                MessageBox.Show("Invalid Timeslot Id");
                return;
            }
            Timeslot = timeslot;
            Id = id;

        }


        public bool IsLunchPeriod
        {
            get { return Get(() => IsLunchPeriod); }
            set { Set(() => IsLunchPeriod, value); }
        }
        public Sponsor LunchSponsor
        {
            get { return Get(() => LunchSponsor); }
            set { Set(() => LunchSponsor, value); }
        }
    }
}
