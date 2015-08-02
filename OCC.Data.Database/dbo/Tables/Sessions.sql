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
    CONSTRAINT [PK__Sessions__3214EC2714C5D8F8] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [Session_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID]),
    CONSTRAINT [Session_Speaker] FOREIGN KEY ([Speaker_ID]) REFERENCES [dbo].[People] ([ID]),
    CONSTRAINT [Session_Tag] FOREIGN KEY ([Tag_ID]) REFERENCES [dbo].[Tags] ([ID]),
    CONSTRAINT [Session_Timeslot] FOREIGN KEY ([Timeslot_ID]) REFERENCES [dbo].[Timeslots] ([ID]),
    CONSTRAINT [Session_Track] FOREIGN KEY ([Track_ID]) REFERENCES [dbo].[Tracks] ([ID])
);

