CREATE TABLE [dbo].[Timeslots] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Event_ID]  INT            NOT NULL,
    [Name]      NVARCHAR (100) NULL,
    [StartTime] DATETIME       NULL,
    [EndTime]   DATETIME       NULL,
    CONSTRAINT [PK__Timeslot__3214EC2718EBB532] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [Timeslot_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID])
);

