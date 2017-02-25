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

using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using OCC.WindowsPhone.Common;
using OCC.WindowsPhone.OrlandoCodeCampService;

namespace OCC.WindowsPhone.Views
{
    public partial class MainPage
    {
        bool isNewInstance;

        public MainPage()
        {
            InitializeComponent();
            isNewInstance = true;
        }


        /// <summary>
        /// Restore Control State and perform Main View Model data load if not already restored from State or Isolated Storage
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (isNewInstance)
            {
                if (State.ContainsKey("pivotindex"))
                {
                    PivotControl.SelectedIndex = (int)State["pivotindex"];
                }

                DataContext = App.MainViewModel;
                if (!App.MainViewModel.IsDataLoaded)
                {
                    App.MainViewModel.LoadData();
                }

            }
            isNewInstance = false;

        }


        /// <summary>
        /// Save current control state in case application gets Tombstoned
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode != NavigationMode.Back)
            {
                State["pivotindex"] = PivotControl.SelectedIndex;

            }
        }


        /// <summary>
        /// Refresh just the currently displayed view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRefreshClick(object sender, System.EventArgs e)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("Cannot refresh right now. The network is not available.");
                return;
            }

            var pivotPageContent = new[] { DataType.Announcements, DataType.Tracks, DataType.Agenda, DataType.Speakers, DataType.Sponsors, DataType.About };
            App.MainViewModel.Refresh(pivotPageContent[PivotControl.SelectedIndex]);
        }


        /// <summary>
        /// Show the TimeSlot (Agenda Details) page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeslotSelected(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null || listbox.SelectedIndex == -1) return;

            var timeslot = e.AddedItems[0] as Timeslot;
            if (timeslot == null) return;

            // navigate
            NavigationService.Navigate(PageUri.TimeslotPage(timeslot.ID));

            // de-select
            listbox.SelectedIndex = -1;
        }


        /// <summary>
        /// Show the Track Details page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackSelected(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null || listbox.SelectedIndex == -1) return;

            var track = e.AddedItems[0] as Track;
            if (track == null) return;

            // navigate
            NavigationService.Navigate(PageUri.TrackPage(track.ID));

            // de-select
            listbox.SelectedIndex = -1;
        }


        /// <summary>
        /// Show the Announcements List page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAnnouncementsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageUri.AnnouncementsPage());
        }


        /// <summary>
        /// Show the Speaker Details page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeakerSelected(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null || listbox.SelectedIndex == -1) return;

            var speaker = e.AddedItems[0] as Person;
            if (speaker == null) return;

            // navigate
            NavigationService.Navigate(PageUri.SpeakerPage(speaker.ID));

            // de-select
            listbox.SelectedIndex = -1;
        }


        /// <summary>
        /// Show the Sponsor Details page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SponsorSelected(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null || listbox.SelectedIndex == -1) return;

            var sponsor = e.AddedItems[0] as Sponsor;
            if (sponsor == null) return;

            // navigate
            NavigationService.Navigate(PageUri.SponsorPage(sponsor.ID));

            // de-select
            listbox.SelectedIndex = -1;
        }


    }
}