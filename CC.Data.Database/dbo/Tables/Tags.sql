CREATE TABLE [dbo].[Tags] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [TagName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED ([ID] ASC)
);

