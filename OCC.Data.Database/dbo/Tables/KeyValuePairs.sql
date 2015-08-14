CREATE TABLE [dbo].[KeyValuePairs] (
    [Id]    NVARCHAR (100)  NOT NULL,
    [Value] NVARCHAR (2000) NULL,
    CONSTRAINT [PK_KeyValuePairs] PRIMARY KEY CLUSTERED ([Id] ASC)
);



