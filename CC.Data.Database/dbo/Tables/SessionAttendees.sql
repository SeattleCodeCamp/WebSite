CREATE TABLE [dbo].[SessionAttendees] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [Session_ID]    INT             NOT NULL,
    [Person_ID]     INT             NOT NULL,
    [SpeakerRating] INT             NOT NULL,
    [Comment]       NVARCHAR (2000) NULL,
    [SessionRating] INT             NOT NULL,
    CONSTRAINT [PK__SessionA__3214EC27245D67DE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [SessionAttendee_Person] FOREIGN KEY ([Person_ID]) REFERENCES [dbo].[People] ([ID]),
    CONSTRAINT [SessionAttendee_Session] FOREIGN KEY ([Session_ID]) REFERENCES [dbo].[Sessions] ([ID])
);

