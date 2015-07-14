USE [master]
GO
/****** Object:  Database [SeattleCodeCamp]    Script Date: 1/19/2013 1:27:47 PM ******/
CREATE DATABASE [SeattleCodeCamp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SeattleCodeCamp', FILENAME = N'D:\Source\SCC\OCC.Data\Scripts\SeattleCodeCamp.mdf' , SIZE = 4352KB , MAXSIZE = 1024000KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SeattleCodeCamp_log', FILENAME = N'D:\Source\SCC\OCC.Data\Scripts\SeattleCodeCamp_log.LDF' , SIZE = 3520KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SeattleCodeCamp] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SeattleCodeCamp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SeattleCodeCamp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET ARITHABORT OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SeattleCodeCamp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SeattleCodeCamp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SeattleCodeCamp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SeattleCodeCamp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SeattleCodeCamp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SeattleCodeCamp] SET  MULTI_USER 
GO
ALTER DATABASE [SeattleCodeCamp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SeattleCodeCamp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SeattleCodeCamp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SeattleCodeCamp] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SeattleCodeCamp]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TwitterHashTag] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Location] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address1] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address2] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Zip] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDefault] [bit] NOT NULL CONSTRAINT [DF_Events_IsDefault]  DEFAULT ((0)),
	[IsSponsorRegistrationOpen] [bit] NOT NULL CONSTRAINT [DF_Events_IsSponsorRegistrationOpen]  DEFAULT ((0)),
	[IsSpeakerRegistrationOpen] [bit] NOT NULL CONSTRAINT [DF_Events_IsSpeakerRegistrationOpen]  DEFAULT ((0)),
	[IsAttendeeRegistrationOpen] [bit] NOT NULL CONSTRAINT [DF_Events_IsAttendeeRegistrationOpen]  DEFAULT ((0)),
	[IsVolunteerRegistrationOpen] [bit] NOT NULL CONSTRAINT [DF_Events_IsVolunteerRegistrationOpen]  DEFAULT ((0)),
 CONSTRAINT [PK__Events__3214EC2705D8E0BE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Announcements](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Title] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Content] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PublishDate] [datetime] NOT NULL,
 CONSTRAINT [PK__Announce__3214EC270D7A0286] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FirstName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Title] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Bio] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Website] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Blog] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Twitter] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PasswordHash] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ImageUrl] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsAdmin] [bit] NOT NULL,
	[Location] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TShirtSize] [int] NULL,
 CONSTRAINT [PK__People__3214EC2714FCD345] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventAttendees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Person_ID] [int] NOT NULL,
	[Rsvp] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK__EventAtt__3214EC27114A936A] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventAttendeeRatings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventAttendee_ID] [int] NOT NULL,
	[SignIn] [int] NOT NULL CONSTRAINT [DF_EventAttendeeRating_SignIn]  DEFAULT ((-1)),
	[Swag] [int] NOT NULL CONSTRAINT [DF_EventAttendeeRating_Swag]  DEFAULT ((-1)),
	[Refreshments] [int] NOT NULL,
	[ReferralSource] [int] NOT NULL CONSTRAINT [DF_EventAttendeeRating_ReferralSource]  DEFAULT ((-1)),
	[Comments] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_EventAttendeeRating] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventAttendeeSessionRatings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventAttendee_ID] [int] NOT NULL,
	[Session_ID] [int] NOT NULL,
	[Ranking] [int] NOT NULL CONSTRAINT [DF_EventAttendeeSessionRating_Ranking]  DEFAULT ((-1)),
	[Timeslot_ID] [int] NOT NULL,
 CONSTRAINT [PK_EventAttendeeSessionRating] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeyValuePairs](
	[Id] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Value] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Disabled] [bit] NOT NULL,
	[Capacity] [int] NOT NULL,
	[Event_ID] [int] NOT NULL,
 CONSTRAINT [PK__Tasks__3214EC2737703C52] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonTasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Task_ID] [int] NOT NULL,
	[Person_ID] [int] NOT NULL,
 CONSTRAINT [PK__PersonTa__3214EC27339FAB6E] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timeslots](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
 CONSTRAINT [PK__Timeslot__3214EC2718EBB532] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tracks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK__Tracks__3214EC27151B244E] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Speaker_ID] [int] NOT NULL,
	[Track_ID] [int] NULL,
	[Timeslot_ID] [int] NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Level] [int] NOT NULL,
	[Location] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Tag_ID] [int] NULL CONSTRAINT [DF_Sessions_Tag_ID]  DEFAULT ((1)),
 CONSTRAINT [PK__Sessions__3214EC2714C5D8F8] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionAttendees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Session_ID] [int] NOT NULL,
	[Person_ID] [int] NOT NULL,
	[SpeakerRating] [int] NOT NULL,
	[Comment] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SessionRating] [int] NOT NULL,
 CONSTRAINT [PK__SessionA__3214EC27245D67DE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sponsors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SponsorshipLevel] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WebsiteUrl] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [Image] [VARBINARY] (MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK__Sponsors__3214EC271CBC4616] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
ALTER TABLE [dbo].[Announcements]  WITH CHECK ADD  CONSTRAINT [Announcement_Event] FOREIGN KEY([Event_ID])
REFERENCES [dbo].[Events] ([ID])
GO
ALTER TABLE [dbo].[Announcements] CHECK CONSTRAINT [Announcement_Event]
GO
ALTER TABLE [dbo].[EventAttendees]  WITH CHECK ADD  CONSTRAINT [EventAttendee_Event] FOREIGN KEY([Event_ID])
REFERENCES [dbo].[Events] ([ID])
GO
ALTER TABLE [dbo].[EventAttendees] CHECK CONSTRAINT [EventAttendee_Event]
GO
ALTER TABLE [dbo].[EventAttendees]  WITH CHECK ADD  CONSTRAINT [EventAttendee_Person] FOREIGN KEY([Person_ID])
REFERENCES [dbo].[People] ([ID])
GO
ALTER TABLE [dbo].[EventAttendees] CHECK CONSTRAINT [EventAttendee_Person]
GO
ALTER TABLE [dbo].[EventAttendeeRatings]  WITH CHECK ADD  CONSTRAINT [FK_EventAttendeeRating_EventAttendees] FOREIGN KEY([EventAttendee_ID])
REFERENCES [dbo].[EventAttendees] ([ID])
GO
ALTER TABLE [dbo].[EventAttendeeRatings] CHECK CONSTRAINT [FK_EventAttendeeRating_EventAttendees]
GO
ALTER TABLE [dbo].[EventAttendeeSessionRatings]  WITH CHECK ADD  CONSTRAINT [FK_EventAttendeeSessionRating_EventAttendees] FOREIGN KEY([EventAttendee_ID])
REFERENCES [dbo].[EventAttendees] ([ID])
GO
ALTER TABLE [dbo].[EventAttendeeSessionRatings] CHECK CONSTRAINT [FK_EventAttendeeSessionRating_EventAttendees]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [Task_Event] FOREIGN KEY([Event_ID])
REFERENCES [dbo].[Events] ([ID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [Task_Event]
GO
ALTER TABLE [dbo].[PersonTasks]  WITH CHECK ADD  CONSTRAINT [PersonTask_Person] FOREIGN KEY([Person_ID])
REFERENCES [dbo].[People] ([ID])
GO
ALTER TABLE [dbo].[PersonTasks] CHECK CONSTRAINT [PersonTask_Person]
GO
ALTER TABLE [dbo].[PersonTasks]  WITH CHECK ADD  CONSTRAINT [PersonTask_Task] FOREIGN KEY([Task_ID])
REFERENCES [dbo].[Tasks] ([ID])
GO
ALTER TABLE [dbo].[PersonTasks] CHECK CONSTRAINT [PersonTask_Task]
GO
ALTER TABLE [dbo].[Timeslots]  WITH CHECK ADD  CONSTRAINT [Timeslot_Event] FOREIGN KEY([Event_ID])
REFERENCES [dbo].[Events] ([ID])
GO
ALTER TABLE [dbo].[Timeslots] CHECK CONSTRAINT [Timeslot_Event]
GO
ALTER TABLE [dbo].[Tracks]  WITH CHECK ADD  CONSTRAINT [Track_Event] FOREIGN KEY([Event_ID])
REFERENCES [dbo].[Events] ([ID])
GO
ALTER TABLE [dbo].[Tracks] CHECK CONSTRAINT [Track_Event]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [Session_Event] FOREIGN KEY([Event_ID])
REFERENCES [dbo].[Events] ([ID])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [Session_Event]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [Session_Speaker] FOREIGN KEY([Speaker_ID])
REFERENCES [dbo].[People] ([ID])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [Session_Speaker]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [Session_Tag] FOREIGN KEY([Tag_ID])
REFERENCES [dbo].[Tags] ([ID])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [Session_Tag]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [Session_Timeslot] FOREIGN KEY([Timeslot_ID])
REFERENCES [dbo].[Timeslots] ([ID])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [Session_Timeslot]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [Session_Track] FOREIGN KEY([Track_ID])
REFERENCES [dbo].[Tracks] ([ID])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [Session_Track]
GO
ALTER TABLE [dbo].[SessionAttendees]  WITH CHECK ADD  CONSTRAINT [SessionAttendee_Person] FOREIGN KEY([Person_ID])
REFERENCES [dbo].[People] ([ID])
GO
ALTER TABLE [dbo].[SessionAttendees] CHECK CONSTRAINT [SessionAttendee_Person]
GO
ALTER TABLE [dbo].[SessionAttendees]  WITH CHECK ADD  CONSTRAINT [SessionAttendee_Session] FOREIGN KEY([Session_ID])
REFERENCES [dbo].[Sessions] ([ID])
GO
ALTER TABLE [dbo].[SessionAttendees] CHECK CONSTRAINT [SessionAttendee_Session]
GO
ALTER TABLE [dbo].[Sponsors]  WITH CHECK ADD  CONSTRAINT [Sponsor_Event] FOREIGN KEY([Event_ID])
REFERENCES [dbo].[Events] ([ID])
GO
ALTER TABLE [dbo].[Sponsors] CHECK CONSTRAINT [Sponsor_Event]
GO

INSERT INTO Events (Name, StartTime, EndTime, IsDefault, IsSpeakerRegistrationOpen, IsSponsorRegistrationOpen, IsAttendeeRegistrationOpen, IsVolunteerRegistrationOpen) VALUES ('Dummy Event', GETDATE(), DATEADD(year,10,GETDATE()),1,1,1,1,1)
INSERT INTO KeyValuePairs (Id, Value) VALUES ('tshirtsizes','[{"Item1":1,"Item2":"Don''t want one"},{"Item1":2,"Item2":"Small"},{"Item1":3,"Item2":"Medium"},{"Item1":4,"Item2":"Large"},{"Item1":5,"Item2":"X-Large"},{"Item1":6,"Item2":"XX-Large"}]')