CREATE TABLE [dbo].[Sponsors] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [Event_ID]         INT             NOT NULL,
    [Name]             NVARCHAR (100)  NULL,
    [Description]      NVARCHAR (2000) NULL,
    [SponsorshipLevel] NVARCHAR (100)  NULL,
    [WebsiteUrl]       NVARCHAR (100)  NULL,
    [Image]            VARBINARY (MAX) NULL,
    CONSTRAINT [PK__Sponsors__3214EC271CBC4616] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [Sponsor_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID])
);

