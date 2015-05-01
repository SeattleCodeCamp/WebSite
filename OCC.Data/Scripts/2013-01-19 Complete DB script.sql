USE [master]
GO
/****** Object:  Database [OrlandoCodeCamp]    Script Date: 1/19/2013 1:27:47 PM ******/
CREATE DATABASE [OrlandoCodeCamp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'orlandocodecamp', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\orlandocodecamp.mdf' , SIZE = 4352KB , MAXSIZE = 1024000KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'orlandocodecamp_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\orlandocodecamp_log.LDF' , SIZE = 3520KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OrlandoCodeCamp] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OrlandoCodeCamp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OrlandoCodeCamp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET ARITHABORT OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [OrlandoCodeCamp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OrlandoCodeCamp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OrlandoCodeCamp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OrlandoCodeCamp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OrlandoCodeCamp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OrlandoCodeCamp] SET  MULTI_USER 
GO
ALTER DATABASE [OrlandoCodeCamp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OrlandoCodeCamp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OrlandoCodeCamp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OrlandoCodeCamp] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [OrlandoCodeCamp]
GO
/****** Object:  User [CodeCampAppUser]    Script Date: 1/19/2013 1:27:47 PM ******/
CREATE USER [CodeCampAppUser] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[CodeCampAppUser]
GO
ALTER ROLE [db_owner] ADD MEMBER [CodeCampAppUser]
GO
/****** Object:  Schema [CodeCampAppUser]    Script Date: 1/19/2013 1:27:48 PM ******/
CREATE SCHEMA [CodeCampAppUser]
GO
/****** Object:  Table [dbo].[Announcements]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Announcements](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Content] [nvarchar](2000) NULL,
	[PublishDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EdmMetadata]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EdmMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModelHash] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventAttendees]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventAttendees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Person_ID] [int] NOT NULL,
	[Rsvp] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Events]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](2000) NULL,
	[TwitterHashTag] [nvarchar](100) NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Location] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](2) NULL,
	[Zip] [nvarchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[People]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Title] [nvarchar](100) NULL,
	[Bio] [nvarchar](2000) NULL,
	[Website] [nvarchar](100) NULL,
	[Blog] [nvarchar](100) NULL,
	[Twitter] [nvarchar](100) NULL,
	[PasswordHash] [nvarchar](100) NULL,
	[ImageUrl] [nvarchar](100) NULL,
	[IsAdmin] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PersonTasks]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonTasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Task_ID] [int] NOT NULL,
	[Person_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SessionAttendees]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionAttendees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Session_ID] [int] NOT NULL,
	[Person_ID] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [nvarchar](2000) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 1/19/2013 1:27:48 PM ******/
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
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](2000) NULL,
	[Status] [nvarchar](100) NULL,
	[Level] [int] NOT NULL,
	[Location] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sponsors]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sponsors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](2000) NULL,
	[SponsorshipLevel] [nvarchar](100) NULL,
	[ImageUrl] [nvarchar](100) NULL,
	[WebsiteUrl] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Disabled] [bit] NOT NULL,
	[Capacity] [int] NOT NULL,
	[Event_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Timeslots]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timeslots](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tracks]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tracks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Event_ID] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](2000) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [CodeCampAppUser].[AttendeeInfo]    Script Date: 1/19/2013 1:27:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [CodeCampAppUser].[AttendeeInfo]
AS
SELECT     p.Id, p.Name, p.FirstName, p.LastName, p.Email, p.Title, p.Bio, p.Website, p.Blog, p.Twitter, p.PasswordHash, p.ImageUrl,
                          (SELECT     pv.Answer
                            FROM          dbo.EventAttendeePreferenceValues AS eapv WITH (nolock) INNER JOIN
                                                   dbo.PreferenceValues AS pv WITH (nolock) ON eapv.PreferenceValues_Id = pv.Id
                            WHERE      (ea.Id = eapv.EventAttendees_Id) AND (pv.PreferenceId = 1)) AS ShirtSize,
                          (SELECT     pv.Answer
                            FROM          dbo.EventAttendeePreferenceValues AS eapv WITH (nolock) INNER JOIN
                                                   dbo.PreferenceValues AS pv WITH (nolock) ON eapv.PreferenceValues_Id = pv.Id
                            WHERE      (ea.Id = eapv.EventAttendees_Id) AND (pv.PreferenceId = 2)) AS Meal,
                          (SELECT     pv.Answer
                            FROM          dbo.EventAttendeePreferenceValues AS eapv WITH (nolock) INNER JOIN
                                                   dbo.PreferenceValues AS pv WITH (nolock) ON eapv.PreferenceValues_Id = pv.Id
                            WHERE      (ea.Id = eapv.EventAttendees_Id) AND (pv.PreferenceId = 3)) AS DNN,
                          (SELECT     pv.Answer
                            FROM          dbo.EventAttendeePreferenceValues AS eapv WITH (nolock) INNER JOIN
                                                   dbo.PreferenceValues AS pv WITH (nolock) ON eapv.PreferenceValues_Id = pv.Id
                            WHERE      (ea.Id = eapv.EventAttendees_Id) AND (pv.PreferenceId = 4)) AS AfterParty,
                          (SELECT     pv.Answer
                            FROM          dbo.EventAttendeePreferenceValues AS eapv WITH (nolock) INNER JOIN
                                                   dbo.PreferenceValues AS pv WITH (nolock) ON eapv.PreferenceValues_Id = pv.Id
                            WHERE      (ea.Id = eapv.EventAttendees_Id) AND (pv.PreferenceId = 6)) AS MVP,
                          (SELECT     pv.Answer
                            FROM          dbo.EventAttendeePreferenceValues AS eapv WITH (nolock) INNER JOIN
                                                   dbo.PreferenceValues AS pv WITH (nolock) ON eapv.PreferenceValues_Id = pv.Id
                            WHERE      (ea.Id = eapv.EventAttendees_Id) AND (pv.PreferenceId = 7)) AS OptOut
FROM         dbo.EventAttendees AS ea WITH (nolock) INNER JOIN
                      dbo.People AS p WITH (nolock) ON ea.PersonId = p.Id


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
ALTER TABLE [dbo].[Sponsors]  WITH CHECK ADD  CONSTRAINT [Sponsor_Event] FOREIGN KEY([Event_ID])
REFERENCES [dbo].[Events] ([ID])
GO
ALTER TABLE [dbo].[Sponsors] CHECK CONSTRAINT [Sponsor_Event]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [Task_Event] FOREIGN KEY([Event_ID])
REFERENCES [dbo].[Events] ([ID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [Task_Event]
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
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ea"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 125
               Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'CodeCampAppUser', @level1type=N'VIEW',@level1name=N'AttendeeInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'CodeCampAppUser', @level1type=N'VIEW',@level1name=N'AttendeeInfo'
GO
USE [master]
GO
ALTER DATABASE [OrlandoCodeCamp] SET  READ_WRITE 
GO
