SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

PRINT N'Creating [dbo].[Announcements]...';



CREATE TABLE [dbo].[Announcements] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Event_ID]    INT             NOT NULL,
    [Title]       NVARCHAR (100)  NULL,
    [Content]     NVARCHAR (2000) NULL,
    [PublishDate] DATETIME        NOT NULL,
    CONSTRAINT [PK__Announce__3214EC270D7A0286] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[EventAttendeeRatings]...';



CREATE TABLE [dbo].[EventAttendeeRatings] (
    [ID]               INT   IDENTITY (1, 1) NOT NULL,
    [EventAttendee_ID] INT   NOT NULL,
    [SignIn]           INT   NOT NULL,
    [Swag]             INT   NOT NULL,
    [Refreshments]     INT   NOT NULL,
    [ReferralSource]   INT   NOT NULL,
    [Comments]         NTEXT NULL,
    CONSTRAINT [PK_EventAttendeeRating] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[EventAttendees]...';



CREATE TABLE [dbo].[EventAttendees] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Event_ID]  INT           NOT NULL,
    [Person_ID] INT           NOT NULL,
    [Rsvp]      NVARCHAR (10) NULL,
    CONSTRAINT [PK__EventAtt__3214EC27114A936A] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[EventAttendeeSessionRatings]...';



CREATE TABLE [dbo].[EventAttendeeSessionRatings] (
    [Id]               INT   IDENTITY (1, 1) NOT NULL,
    [EventAttendee_ID] INT   NOT NULL,
    [Session_ID]       INT   NOT NULL,
    [Ranking]          INT   NOT NULL,
    [Timeslot_ID]      INT   NOT NULL,
    [Comments]         NTEXT NULL,
    CONSTRAINT [PK_EventAttendeeSessionRating] PRIMARY KEY CLUSTERED ([Id] ASC)
);



PRINT N'Creating [dbo].[Events]...';



CREATE TABLE [dbo].[Events] (
    [ID]                          INT             IDENTITY (1, 1) NOT NULL,
    [Name]                        NVARCHAR (100)  NULL,
    [Description]                 NVARCHAR (2000) NULL,
    [TwitterHashTag]              NVARCHAR (100)  NULL,
    [StartTime]                   DATETIME        NOT NULL,
    [EndTime]                     DATETIME        NOT NULL,
    [Location]                    NVARCHAR (100)  NULL,
    [Address1]                    NVARCHAR (100)  NULL,
    [Address2]                    NVARCHAR (100)  NULL,
    [City]                        NVARCHAR (100)  NULL,
    [State]                       NVARCHAR (2)    NULL,
    [Zip]                         NVARCHAR (5)    NULL,
    [IsDefault]                   BIT             NOT NULL,
    [IsSponsorRegistrationOpen]   BIT             NOT NULL,
    [IsSpeakerRegistrationOpen]   BIT             NOT NULL,
    [IsAttendeeRegistrationOpen]  BIT             NOT NULL,
    [IsVolunteerRegistrationOpen] BIT             NOT NULL,
    CONSTRAINT [PK__Events__3214EC2705D8E0BE] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[KeyValuePairs]...';



CREATE TABLE [dbo].[KeyValuePairs] (
    [Id]    NVARCHAR (100)  NOT NULL,
    [Value] NVARCHAR (2000) NULL,
    CONSTRAINT [PK_KeyValuePairs] PRIMARY KEY CLUSTERED ([Id] ASC)
);



PRINT N'Creating [dbo].[People]...';



CREATE TABLE [dbo].[People] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [Email]         NVARCHAR (100)  NULL,
    [FirstName]     NVARCHAR (100)  NULL,
    [LastName]      NVARCHAR (100)  NULL,
    [Title]         NVARCHAR (100)  NULL,
    [Bio]           NVARCHAR (2000) NULL,
    [Website]       NVARCHAR (100)  NULL,
    [Blog]          NVARCHAR (100)  NULL,
    [Twitter]       NVARCHAR (100)  NULL,
    [PasswordHash]  NVARCHAR (100)  NULL,
    [ImageUrl]      NVARCHAR (500)  NULL,
    [IsAdmin]       BIT             NOT NULL,
    [Location]      NVARCHAR (100)  NULL,
    [TShirtSize]    INT             NULL,
    [LoginProvider] NVARCHAR (128)  NULL,
    CONSTRAINT [PK__People__3214EC2714FCD345] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[PersonTasks]...';



CREATE TABLE [dbo].[PersonTasks] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [Task_ID]   INT NOT NULL,
    [Person_ID] INT NOT NULL,
    CONSTRAINT [PK__PersonTa__3214EC27339FAB6E] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[SessionAttendees]...';



CREATE TABLE [dbo].[SessionAttendees] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [Session_ID]    INT             NOT NULL,
    [Person_ID]     INT             NOT NULL,
    [SpeakerRating] INT             NOT NULL,
    [Comment]       NVARCHAR (2000) NULL,
    [SessionRating] INT             NOT NULL,
    CONSTRAINT [PK__SessionA__3214EC27245D67DE] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[Sessions]...';



CREATE TABLE [dbo].[Sessions] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Event_ID]    INT             NOT NULL,
    [Speaker_ID]  INT             NOT NULL,
    [Track_ID]    INT             NULL,
    [Timeslot_ID] INT             NULL,
    [Name]        NVARCHAR (100)  NULL,
    [Description] NVARCHAR (2000) NULL,
    [Status]      NVARCHAR (100)  NULL,
    [Level]       INT             NOT NULL,
    [Location]    NVARCHAR (100)  NULL,
    [Tag_ID]      INT             NULL,
    CONSTRAINT [PK__Sessions__3214EC2714C5D8F8] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[Sponsors]...';



CREATE TABLE [dbo].[Sponsors] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [Event_ID]         INT             NOT NULL,
    [Name]             NVARCHAR (100)  NULL,
    [Description]      NVARCHAR (2000) NULL,
    [SponsorshipLevel] NVARCHAR (100)  NULL,
    [ImageUrl]         NVARCHAR (100)  NULL,
    [WebsiteUrl]       NVARCHAR (500)  NULL,
    [Image]            VARBINARY (MAX) NULL,
    CONSTRAINT [PK__Sponsors__3214EC271CBC4616] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[Tags]...';



CREATE TABLE [dbo].[Tags] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [TagName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[Tasks]...';



CREATE TABLE [dbo].[Tasks] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (100) NULL,
    [StartTime]   DATETIME       NOT NULL,
    [EndTime]     DATETIME       NOT NULL,
    [Disabled]    BIT            NOT NULL,
    [Capacity]    INT            NOT NULL,
    [Event_ID]    INT            NOT NULL,
    CONSTRAINT [PK__Tasks__3214EC2737703C52] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[Timeslots]...';



CREATE TABLE [dbo].[Timeslots] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Event_ID]  INT            NOT NULL,
    [Name]      NVARCHAR (100) NULL,
    [StartTime] DATETIME       NULL,
    [EndTime]   DATETIME       NULL,
    CONSTRAINT [PK__Timeslot__3214EC2718EBB532] PRIMARY KEY CLUSTERED ([ID] ASC)
);



PRINT N'Creating [dbo].[Tracks]...';



CREATE TABLE [dbo].[Tracks] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Event_ID]    INT             NOT NULL,
    [Name]        NVARCHAR (100)  NULL,
    [Description] NVARCHAR (2000) NULL,
    CONSTRAINT [PK__Tracks__3214EC27151B244E] PRIMARY KEY CLUSTERED ([ID] ASC)
);