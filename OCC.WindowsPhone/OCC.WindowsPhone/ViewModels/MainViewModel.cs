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
using System.Collections.Generic;
using System.Data.Services.Client;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using OCC.WindowsPhone.Common;
using OCC.WindowsPhone.Models;
using OCC.WindowsPhone.OrlandoCodeCampService;
namespace OCC.WindowsPhone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        OrlandoCodeCampEntities context;
        public bool IsDataLoaded { get; private set; }



        DataServiceCollection<Announcement> announcements;
        public DataServiceCollection<Announcement> Announcements
        {
            get { return announcements; }
            private set
            {
                announcements = value;
                RaisePropertyChanged(() => Announcements);
                LatestAnnouncement = Announcements.FirstOrDefault();

                announcements.LoadCompleted += (s, e) =>
                {
                    if (announcements.Continuation != null)
                    {
                        Announcements.LoadNextPartialSetAsync();
                    }
                    IsDataLoaded = true;
                    LatestAnnouncement = Announcements.FirstOrDefault();

                };
            }
        }

        DataServiceCollection<Track> tracks;
        public DataServiceCollection<Track> Tracks
        {
            get { return tracks; }
            private set
            {
                tracks = value;
                RaisePropertyChanged(() => Tracks);

                tracks.LoadCompleted += (s, e) =>
                {
                    if (tracks.Continuation != null)
                    {
                        Tracks.LoadNextPartialSetAsync();
                    }
                    IsDataLoaded = true;
                    IsBusy = false;
                };
            }
        }
        DataServiceCollection<Session> sessions;
        public DataServiceCollection<Session> Sessions
        {
            get { return sessions; }
            private set
            {
                sessions = value;
                RaisePropertyChanged(() => Sessions);

                sessions.LoadCompleted += (s, e) =>
                {
                    if (sessions.Continuation != null)
                    {
                        Sessions.LoadNextPartialSetAsync();
                    }
                    IsDataLoaded = true;
                };
            }
        }


        DataServiceCollection<Person> speakers;
        public DataServiceCollection<Person> Speakers
        {
            get { return speakers; }
            private set
            {
                speakers = value;
                RaisePropertyChanged(() => Speakers);

                speakers.LoadCompleted += (s, e) =>
                {
                    if (speakers.Continuation != null)
                    {
                        Speakers.LoadNextPartialSetAsync();
                    }
                    IsDataLoaded = true;

                };
            }
        }
        DataServiceCollection<Timeslot> timeslots;
        public DataServiceCollection<Timeslot> Timeslots
        {
            get { return timeslots; }
            private set
            {
                timeslots = value;
                RaisePropertyChanged(() => Timeslots);

                timeslots.LoadCompleted += (s, e) =>
                {
                    if (timeslots.Continuation != null)
                    {
                        timeslots.LoadNextPartialSetAsync();
                    }
                    IsDataLoaded = true;
                };
            }
        }
        DataServiceCollection<Sponsor> sponsors;
        public DataServiceCollection<Sponsor> Sponsors
        {
            get { return sponsors; }
            private set
            {
                sponsors = value;
                RaisePropertyChanged(() => Sponsors);
                sponsors.LoadCompleted += (s, e) =>
                {
                    if (sponsors.Continuation != null)
                    {
                        Sponsors.LoadNextPartialSetAsync();
                    }
                    IsDataLoaded = true;
                };
            }
        }
        public Announcement LatestAnnouncement
        {
            get { return Get(() => LatestAnnouncement); }
            set { Set(() => LatestAnnouncement, value); }
        }
        public List<BoardMember> BoardMembers { get { return Board.GetBoardMembers(); } }


        public void Refresh(DataType dataType)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return;

            // Cache the current merge option and change it to MergeOption.OverwriteChanges.
            var cachedOption = context.MergeOption;
            context.MergeOption = MergeOption.OverwriteChanges;
            switch (dataType)
            {
                case DataType.Announcements:
                    LoadAnnouncements();
                    break;

                case DataType.Agenda:
                    LoadTimeslots();
                    LoadSessions();
                    break;

                case DataType.Speakers:
                    LoadSpeakers();
                    break;

                case DataType.Sponsors:
                    LoadSponsors();
                    break;

                case DataType.Tracks:
                    LoadTracks();
                    LoadSessions();
                    break;

            }
            // Reset the merge option.
            context.MergeOption = cachedOption;
        }

        public void LoadData()
        {
            LoadTwitter();
            context = new OrlandoCodeCampEntities(new Uri(Constants.DataServiceUrl));
            IsBusy = true;
            LoadAnnouncements();
            LoadTracks();
            LoadSessions();
            LoadSpeakers();
            LoadTimeslots();
            LoadSponsors();
        }
        public void LoadAnnouncements()
        {
            Announcements = new DataServiceCollection<Announcement>(context);
            var query = from a in context.Announcements.OrderByDescending(a => a.PublishDate) select a;
            Announcements.LoadAsync(query);
        }
        public void LoadTracks()
        {
            Tracks = new DataServiceCollection<Track>(context);
            var query = from t in context.Tracks.Where(t => t.Event_ID == Constants.EventId) select t;
            Tracks.LoadAsync(query);
        }
        public void LoadSessions()
        {
            Sessions = new DataServiceCollection<Session>(context);
            var query = from s in context.Sessions where s.Event_ID == Constants.EventId select s;
            Sessions.LoadAsync(query);
        }
        public void LoadSpeakers()
        {
            Speakers = new DataServiceCollection<Person>(context);
            var query = context.CreateQuery<Person>("Speakers").AddQueryOption("eventId", Constants.EventId).OrderBy(p => p.LastName);
            Speakers.LoadAsync(query);
        }
        public void LoadSponsors()
        {
            Sponsors = new DataServiceCollection<Sponsor>(context);
            var query = from s in context.Sponsors.OrderBy(s=>s.Name) select s;
            Sponsors.LoadAsync(query);
        }
        public void LoadTimeslots()
        {
            Timeslots = new DataServiceCollection<Timeslot>(context);
            var query = from ts in context.Timeslots select ts;
            Timeslots.LoadAsync(query);
        }

        public void SaveToIsoStorage()
        {
            if (!IsDataLoaded)
                return;

            var iso = IsolatedStorageSettings.ApplicationSettings;
            iso["contextdata"] = Serialize();
            iso.Save();
        }
        public void LoadFromIsoStorage()
        {
            var isoDictionary = IsolatedStorageSettings.ApplicationSettings;
            if (isoDictionary.Contains("contextdata"))
            {
                var contextDataString = isoDictionary["contextdata"] as string;
                Deserialize(contextDataString);

            }

        }
        string Serialize()
        {
            if (!IsDataLoaded)
                return null;

            var collections = new Dictionary<string, object>();
            collections["announcements"] = Announcements;
            collections["sessions"] = Sessions;
            collections["tracks"] = Tracks;
            collections["speakers"] = Speakers;
            collections["sponsors"] = Sponsors;
            collections["timeslots"] = Timeslots;
            return DataServiceState.Serialize(context, collections);
        }
        public void SaveState(IDictionary<string, object> stateDictionary)
        {
            if (!IsDataLoaded)
                return;

            stateDictionary["contextdata"] = Serialize();
        }

        void Deserialize(string contextDataString)
        {
            var contextData = DataServiceState.Deserialize(contextDataString);
            context = contextData.Context as OrlandoCodeCampEntities;
            var collections = contextData.RootCollections;

            Announcements = GetCollection<Announcement>(collections,"announcements");
            Sessions = GetCollection<Session>(collections, "sessions");
            Tracks = GetCollection<Track>(collections, "tracks");
            Speakers = GetCollection<Person>(collections, "speakers");
            Sponsors = GetCollection<Sponsor>(collections, "sponsors");
            Timeslots = GetCollection<Timeslot>(collections, "timeslots");
            IsDataLoaded = true;
        }
        static DataServiceCollection<T> GetCollection<T>(IDictionary<string, object> collectionDictionary, string key)
        {
            if (collectionDictionary.ContainsKey(key))
                return collectionDictionary[key] as DataServiceCollection<T>;
            return new DataServiceCollection<T>();
        }
        public void RestoreState(IDictionary<string, object> stateDictionary)
        {
            if (!stateDictionary.ContainsKey("contextdata"))
                return;

            if (IsDataLoaded)
                return;

            var contextDataString = stateDictionary["contextdata"] as string;
            if (string.IsNullOrEmpty(contextDataString))
                return;

            Deserialize(contextDataString);
        }

        public void LoadTwitter()
        {
            var twitter = new WebClient();
            //twitter.DownloadStringCompleted += (s,e)=>
            //                                       {
            //                                           if (e.Error != null)
            //                                               return;


            //                                           var xmlTweets = XElement.Parse(e.Result);
            //                                           Tweets = xmlTweets.Descendants("status").Select(t => new TwitterItem
            //                                           {
            //                                               ImageSource = t.Element("user").Element("profile_image_url").Value,
            //                                               Message = t.Element("text").Value,
            //                                               UserName = t.Element("user").Element("screen_name").Value
            //                                           }).ToList();
            //                                       };
            ////twitter.DownloadStringAsync(new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=bm0061"));
            //twitter.DownloadStringAsync(new Uri("http://search.twitter.com/search.rss?q=%23OrlandoCC"));

            twitter.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null)
                    return;

                var xmlTweets = XElement.Parse(e.Result);
                Tweets = xmlTweets.Descendants("item").Select(t => new TwitterItem
                {
                    ImageSource = t.Elements().Where(el => el.Attribute("url") != null).First().Attribute("url").Value,
                    Message = t.Element("title").Value,
                    UserName = t.Element("author").Value
                }).ToList();
            };
            twitter.DownloadStringAsync(new Uri("http://search.twitter.com/search.rss?q=%23OrlandoCC"));


        }
        public List<TwitterItem> Tweets
        {
            get { return Get(() => Tweets); }
            set { Set(() => Tweets, value); }
        }


    }
}