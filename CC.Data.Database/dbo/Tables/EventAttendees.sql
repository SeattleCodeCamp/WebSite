CREATE TABLE [dbo].[EventAttendees] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Event_ID]  INT           NOT NULL,
    [Person_ID] INT           NOT NULL,
    [Rsvp]      NVARCHAR (10) NULL,
    CONSTRAINT [PK__EventAtt__3214EC27114A936A] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [EventAttendee_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID]),
    CONSTRAINT [EventAttendee_Person] FOREIGN KEY ([Person_ID]) REFERENCES [dbo].[People] ([ID])
);

