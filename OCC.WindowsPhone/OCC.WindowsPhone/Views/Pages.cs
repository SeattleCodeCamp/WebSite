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

namespace OCC.WindowsPhone.Views
{
    /// <summary>
    /// Single place to define the magic strings used for navigating between pages
    /// </summary>
    public class PageUri
    {
        public static Uri TimeslotPage(int timeslotId)
        {
            return new Uri(string.Format("/Views/TimeslotPage.xaml?timeslotid={0}", timeslotId), UriKind.Relative);
        }
        public static Uri TimeslotsPage()
        {
            return new Uri("/Views/TimeslotsPage.xaml", UriKind.Relative);
        }

        public static Uri TracksPage()
        {
            return new Uri("/Views/TracksPage.xaml", UriKind.Relative);
        }

        public static Uri TrackPage(int trackId)
        {
            return new Uri(string.Format("/Views/TrackPage.xaml?trackid={0}",trackId), UriKind.Relative);
        }

        public static Uri SessionsPage()
        {
            return new Uri("/Views/SessionsPage.xaml", UriKind.Relative);
        }

        public static Uri SessionPage(int sessionId)
        {
            return new Uri(string.Format("/Views/SessionPage.xaml?sessionid={0}",sessionId), UriKind.Relative);
        }

        public static Uri SpeakersPage()
        {
            return new Uri("/Views/SpeakersPage.xaml", UriKind.Relative);
        }

        public static Uri SpeakerPage(int speakerId)
        {
            return new Uri(string.Format("/Views/SpeakerPage.xaml?speakerid={0}",speakerId), UriKind.Relative);
        }

        public static Uri SponsorsPage()
        {
            return new Uri("/Views/SponsorsPage.xaml", UriKind.Relative);
        }

        public static Uri SponsorPage(int sponsorId)
        {
            return new Uri(string.Format("/Views/SponsorPage.xaml?sponsorid={0}",sponsorId), UriKind.Relative);
        }

        public static Uri AnnouncementsPage()
        {
            return new Uri("/Views/AnnouncementsPage.xaml", UriKind.Relative);
        }

        public static Uri AnnouncementPage(int announcementId)
        {
            return new Uri(string.Format("/Views/AnnouncementPage.xaml?announcementid={0}", announcementId), UriKind.Relative);
        }

        public static Uri AboutPage()
        {
            return new Uri("/Views/AboutPage.xaml", UriKind.Relative);
        }
    }
}
