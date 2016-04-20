CREATE TABLE [dbo].[Tasks] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (100) NULL,
    [StartTime]   DATETIME       NOT NULL,
    [EndTime]     DATETIME       NOT NULL,
    [Disabled]    BIT            NOT NULL,
    [Capacity]    INT            NOT NULL,
    [Event_ID]    INT            NOT NULL,
    CONSTRAINT [PK__Tasks__3214EC2737703C52] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [Task_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID])
);

