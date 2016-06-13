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
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using OCC.WindowsPhone.OrlandoCodeCampService;

namespace OCC.WindowsPhone.ViewModels
{
    public class SpeakerViewModel:ViewModelBase
    {
        public Person Speaker
        {
            get { return Get(() => Speaker); }
            private set { Set(() => Speaker, value); LoadSessions(); }
        }
        public IEnumerable<Session> Sessions
        {
            get { return Get(() => Sessions); }
            private set { Set(() => Sessions, value); }
        }

        public int Id { get; private set; }


        public void Load(int id)
        {
            var speaker = App.MainViewModel.Speakers.FirstOrDefault(t => t.ID == id);
            if (speaker == null)
            {
                MessageBox.Show("Invalid Speaker Id");
                return;
            }
            Speaker = speaker;
            Id = id;

        }
        void LoadSessions()
        {
            if (Speaker == null)
                return;

            Sessions = App.MainViewModel.Sessions.Where(s => s.Speaker_ID == Speaker.ID).AsEnumerable();
        }
    }
}
