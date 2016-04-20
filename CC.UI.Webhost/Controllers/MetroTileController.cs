using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CC.UI.Webhost.Infrastructure;
using CC.UI.Webhost.Models;
using CC.Service.Webhost.Services;

namespace CC.UI.Webhost.Controllers
{
    public class MetroTileController : BaseController
    {
        [ChildActionOnly]
        public PartialViewResult DoubleMetroTileForCCPhotos()
        {

            var tileViewModel = new MetroTileViewModel
            {
                TileLinkActionName = "Index",
                TileLinkControllerName = "Home",
                TileDisplayName = "Codecamp 2014 Photos",
                TileBackgroundCssClass = "greenTile"
            };

            tileViewModel.MetroTileIcons.Add(new MetroTileImage("~/Content/Avatar/2014.jpg")
            {
                AltText = "Codecamp 2014 Photos",
                Title = "Codecamp 2014 Photos",
                //AnchorTagUri = "https://www.facebook.com/media/set/?set=oa.10152344594037028&type=1"
            });

            return PartialView("_DoubleMetroTile", tileViewModel);

        }

        [ChildActionOnly]
        public PartialViewResult SingleMetroTileForAccount()
        {
            MetroTileImage metroTileImage = null;
            MetroTileViewModel metroTileViewModel = null;

            if (CurrentUser != null)
            {
                metroTileViewModel = new MetroTileViewModel
                {
                    TileLinkActionName = "UpdateProfile",
                    TileLinkControllerName = "Account",
                    TileDisplayName = "Update Profile",
                    TileBackgroundCssClass = "singleTileGreenImage"
                };

                var user = CurrentUser;

                if (!String.IsNullOrEmpty(user.ImageUrl))
                {
                    metroTileImage = new MetroTileImage(user.ImageUrl)
                    {
                        AltText = user.FullName,
                        Title = string.Format("Welcome, {0}", user.FirstName)
                    };
                }
            }
            else
            {
                metroTileViewModel = new MetroTileViewModel
                {
                    TileLinkActionName = "LogOn",
                    TileLinkControllerName = "Account",
                    TileDisplayName = "Login / Register",
                    TileBackgroundCssClass = "singleTileGreenImage"
                };

                metroTileImage = new MetroTileImage("../../Content/themes/Shared/BlankUser.png")
                {
                    AltText = "Login or Register",
                    Title = "Login or Register"
                };
            }

            if (metroTileImage == null)
            {
                metroTileImage = new MetroTileImage("../../Content/themes/Shared/BlankUser.png")
                {
                    AltText = "Login or Register",
                    Title = "Login or Register"
                };
            }

            metroTileViewModel.MetroTileIcons.Add(item: metroTileImage);
            metroTileViewModel.MetroTileIcons.Add(new MetroTileImage("../../Content/themes/Shared/SpaceNeedle.ico")
            {
                AltText = "The only tug",
                Title = "The only tug"
            });

            return PartialView("_SingleMetroTile", metroTileViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult DoubleMetroTileForSponsors()
        {
            var tileViewModel = new MetroTileViewModel
            {
                TileLinkActionName = "Sponsors",
                TileLinkControllerName = "Home",
                TileDisplayName = "Sponsors",
                TileBackgroundCssClass = "blueLightTile" // "singleTileGreenImage",
            };

            var defaultEvent = service.GetDefaultEvent();
            var sponsors = new List<Service.Webhost.Services.Sponsor>(service.GetSponsors(defaultEvent.ID));

            foreach (var sponsor in sponsors)
            {
                if (!string.IsNullOrEmpty(sponsor.Name) && sponsor.Image != null)
                {
                    tileViewModel.MetroTileIcons.Add(new MetroTileImage(new Infrastructure.WebImageOCC(sponsor.Image))
                    {
                        AltText = sponsor.Name,
                        Title = string.Format("{0} ({1} sponsor)", sponsor.Name, sponsor.SponsorshipLevel.Replace("sponsor", String.Empty)),
                        AnchorTagUri = sponsor.WebsiteUrl
                    });
                }
            }
            tileViewModel.MetroTileIcons.Shuffle();

            return PartialView("_DoubleMetroTile", tileViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult SingleMetroTileForAgenda()
        {
            var tileViewModel = new MetroTileViewModel
            {
                TileLinkActionName = "Agenda",
                TileLinkControllerName = "Home",
                TileDisplayName = "Agenda",
                TileBackgroundCssClass = "greenTile",
                DefaultTileImage = new MetroTileImage("../../Content/themes/Metro/MetroIcons/QAgenda.png")
                {
                    AltText = "Agenda",
                    Title = "Agenda"
                }
            };

            return PartialView("_SingleMetroTile", tileViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult SingleMetroTileForAdmin()
        {
            var tileViewModel = new MetroTileViewModel
            {
                TileLinkActionName = "Admin",
                TileLinkControllerName = "Home",
                TileDisplayName = "Admin",
                TileBackgroundCssClass = "blueTile",
                DefaultTileImage = new MetroTileImage("../../Content/themes/Metro/MetroIcons/QAdmin.png")
                {
                    AltText = "Admin",
                    Title = "Admin"
                }
            };

            return PartialView("_SingleMetroTile", tileViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult SingleMetroTileForVenue()
        {
            var tileViewModel = new MetroTileViewModel
            {
                TileLinkActionName = "Venue",
                TileLinkControllerName = "Home",
                TileDisplayName = "Venue",
                TileBackgroundCssClass = "singleTileMapImage",
                DefaultTileImage = new MetroTileImage("../../Content/themes/Metro/MetroIcons/BlankImage-1x1.png")
                {
                    AltText = "Venue",
                    Title = "Venue"
                }
            };

            return PartialView("_SingleMetroTile", tileViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult SingleMetroTileForSpeakers()
        {
            var defaultEvent = service.GetDefaultEvent();
            int speakersCount = service.GetSpeakersCount(defaultEvent.ID);

            var tileViewModel = new MetroTileViewModel
            {
                TileLinkActionName = "Speakers",
                TileLinkControllerName = "Home",
                TileDisplayName = "Speakers",
                TileBackgroundCssClass = "redTile",
                DefaultTileImage = new MetroTileImage("../../Content/themes/Metro/MetroIcons/QSpeaker.png")
                {
                    AltText = "Speakers",
                    Title = "Speakers"
                },
                TileNotificationMessage = speakersCount.ToString()
            };

            return PartialView("_SingleMetroTile", tileViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult SingleMetroTileForSessions()
        {
            var defaultEvent = service.GetDefaultEvent();
            int sessionsCount = service.GetSessionsCount(defaultEvent.ID);

            var tileViewModel = new MetroTileViewModel
            {
                TileLinkActionName = "Sessions",
                TileLinkControllerName = "Home",
                TileDisplayName = "Sessions",
                TileBackgroundCssClass = "orangeTile",
                DefaultTileImage = new MetroTileImage("../../Content/themes/Metro/MetroIcons/QSession.png")
                {
                    AltText = "Sessions",
                    Title = "Sessions"
                },
                TileNotificationMessage = sessionsCount.ToString()
            };

            return PartialView("_SingleMetroTile", tileViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult SingleMetroTileForVolunteers()
        {
            var tileViewModel = new MetroTileViewModel
            {
                TileLinkActionName = "Volunteers",
                TileLinkControllerName = "Home",
                TileDisplayName = "Volunteers",
                DefaultTileImage = new MetroTileImage("../../Content/themes/Metro/MetroIcons/QVolunteer.png")
                {
                    AltText = "Volunteers",
                    Title = "Volunteers"
                },
                TileBackgroundCssClass = "limeTile"
            };

            return PartialView("_SingleMetroTile", tileViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult SingleMetroTileForAttendees()
        {
            var defaultEvent = service.GetDefaultEvent();
            int attendeesCount = service.GetAttendeesCount(defaultEvent.ID);

            var tileViewModel = new MetroTileViewModel
            {
                TileLinkActionName = "Index",
                TileLinkControllerName = "Home",
                TileDisplayName = "Attendees",
                TileBackgroundCssClass = "pinkTile",
                DefaultTileImage = new MetroTileImage("../../Content/themes/Metro/MetroIcons/QAttendee.png")
                {
                    AltText = "Attendees",
                    Title = "Attendees"
                },
                TileNotificationMessage = attendeesCount.ToString()
            };

            return PartialView("_SingleMetroTile", tileViewModel);
        }

        //[ChildActionOnly]
        //public PartialViewResult SingleMetroTileForPasswordChange()
        //{
        //    var tileViewModel = new MetroTileViewModel {
        //            TileLinkActionName = "ChangePassword",
        //            TileLinkControllerName = "Account",
        //            TileDisplayName = "Change Password",
        //            TileBackgroundCssClass = "singleTileBlueImage"
        //    };

        //    return PartialView("_SingleMetroTile", tileViewModel);
        //}

        //[ChildActionOnly]
        //public PartialViewResult SingleMetroTileForLogOut()
        //{
        //    var tileViewModel = new MetroTileViewModel {
        //            TileLinkActionName = "LogOff",
        //            TileLinkControllerName = "Account",
        //            TileDisplayName = "Log Out",
        //            TileBackgroundCssClass = "singleTileBlueImage"
        //    };

        //    return PartialView("_SingleMetroTile", tileViewModel);
        //}

        //[ChildActionOnly]
        //public PartialViewResult DoubleMetroTileForSessions()
        //{
        //    var doubleTileViewModel = new DoubleMetroTileViewModel();

        //    return PartialView("_DoubleMetroTile", doubleTileViewModel);
        //}

        [ChildActionOnly]
        public PartialViewResult DoubleMetroTileForTwitter()
        {
            var twitterTileViewModel = new TwitterMetroTileViewModel
            {
                TileBackgroundCssClass = "blueTile", //  "doubleTileBlueImage",
                TileDisplayName = "#SeattleCC"
            };

            //insert some logic to go get tweets with the #SeattleCC hashtag or tweets mentioning @netda or tweets that @NETDA puts out

            //If no data is returned from the twitter stream create two default tweets
            if (twitterTileViewModel.Tweets.Count < 1)
            {
                twitterTileViewModel.Tweets.Add(new TweetViewModel());
                twitterTileViewModel.Tweets.Add(new TweetViewModel());
            }

            return PartialView("_TwitterMetroTile", twitterTileViewModel);
        }
    }
}