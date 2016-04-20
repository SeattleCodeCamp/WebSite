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
using System.Windows.Controls;
using System.Windows.Navigation;
using OCC.WindowsPhone.Extensions;
using OCC.WindowsPhone.OrlandoCodeCampService;
using OCC.WindowsPhone.ViewModels;

namespace OCC.WindowsPhone.Views
{
    public partial class TrackPage
    {
        bool isNewInstance;
        TrackViewModel viewModel;

        public TrackPage()
        {
            InitializeComponent();
            isNewInstance = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (isNewInstance)
            {
                if (viewModel == null)
                {
                    if (State.Count > 0)
                    {
                        viewModel = (TrackViewModel)State["viewModel"];
                    }
                    else
                    {
                        viewModel = new TrackViewModel();
                        viewModel.Load(this.QueryStringInt("trackid"));
                    }
                    DataContext = viewModel;
                }
            }
            isNewInstance = false;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode != NavigationMode.Back)
            {
                //State["viewModel"] = viewModel;

            }
        }

        private void SessionSelected(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null || listbox.SelectedIndex == -1) return;

            var session = e.AddedItems[0] as Session;
            if (session == null) return;

            // navigate
            NavigationService.Navigate(PageUri.SessionPage(session.ID));

            // de-select
            listbox.SelectedIndex = -1;
        }
    }
}