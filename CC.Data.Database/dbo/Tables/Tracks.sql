CREATE TABLE [dbo].[Tracks] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Event_ID]    INT             NOT NULL,
    [Name]        NVARCHAR (100)  NULL,
    [Description] NVARCHAR (2000) NULL,
    CONSTRAINT [PK__Tracks__3214EC27151B244E] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [Track_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID])
);

