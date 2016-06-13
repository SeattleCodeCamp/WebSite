CREATE TABLE [dbo].[People] (
    [ID]           INT             IDENTITY (1, 1) NOT NULL,
    [Email]        NVARCHAR (100)  NULL,
    [FirstName]    NVARCHAR (100)  NULL,
    [LastName]     NVARCHAR (100)  NULL,
    [Title]        NVARCHAR (100)  NULL,
    [Bio]          NVARCHAR (2000) NULL,
    [Website]      NVARCHAR (100)  NULL,
    [Blog]         NVARCHAR (100)  NULL,
    [Twitter]      NVARCHAR (100)  NULL,
    [PasswordHash] NVARCHAR (100)  NULL,
    [ImageUrl]     NVARCHAR (500)  NULL,
    [IsAdmin]      BIT             NOT NULL,
    [Location]     NVARCHAR (100)  NULL,
    [TShirtSize]   INT             NULL,
    [LoginProvider] NVARCHAR(128) NULL, 
    CONSTRAINT [PK__People__3214EC2714FCD345] PRIMARY KEY CLUSTERED ([ID] ASC)
);

