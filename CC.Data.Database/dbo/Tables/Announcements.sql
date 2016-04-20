CREATE TABLE [dbo].[Announcements] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Event_ID]    INT             NOT NULL,
    [Title]       NVARCHAR (100)  NULL,
    [Content]     NVARCHAR (2000) NULL,
    [PublishDate] DATETIME        NOT NULL,
    CONSTRAINT [PK__Announce__3214EC270D7A0286] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [Announcement_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID])
);

