CREATE TABLE [dbo].[EventAttendeeSessionRatings] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [EventAttendee_ID] INT NOT NULL,
    [Session_ID]       INT NOT NULL,
    [Ranking]          INT CONSTRAINT [DF_EventAttendeeSessionRating_Ranking] DEFAULT ((-1)) NOT NULL,
    [Timeslot_ID]      INT NOT NULL,
    CONSTRAINT [PK_EventAttendeeSessionRating] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EventAttendeeSessionRating_EventAttendees] FOREIGN KEY ([EventAttendee_ID]) REFERENCES [dbo].[EventAttendees] ([ID])
);

