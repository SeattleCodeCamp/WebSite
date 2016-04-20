CREATE TABLE [dbo].[Events] (
    [ID]                          INT             IDENTITY (1, 1) NOT NULL,
    [Name]                        NVARCHAR (100)  NULL,
    [Description]                 NVARCHAR (2000) NULL,
    [TwitterHashTag]              NVARCHAR (100)  NULL,
    [StartTime]                   DATETIME        NOT NULL,
    [EndTime]                     DATETIME        NOT NULL,
    [Location]                    NVARCHAR (100)  NULL,
    [Address1]                    NVARCHAR (100)  NULL,
    [Address2]                    NVARCHAR (100)  NULL,
    [City]                        NVARCHAR (100)  NULL,
    [State]                       NVARCHAR (2)    NULL,
    [Zip]                         NVARCHAR (5)    NULL,
    [IsDefault]                   BIT             CONSTRAINT [DF_Events_IsDefault] DEFAULT ((0)) NOT NULL,
    [IsSponsorRegistrationOpen]   BIT             CONSTRAINT [DF_Events_IsSponsorRegistrationOpen] DEFAULT ((0)) NOT NULL,
    [IsSpeakerRegistrationOpen]   BIT             CONSTRAINT [DF_Events_IsSpeakerRegistrationOpen] DEFAULT ((0)) NOT NULL,
    [IsAttendeeRegistrationOpen]  BIT             CONSTRAINT [DF_Events_IsAttendeeRegistrationOpen] DEFAULT ((0)) NOT NULL,
    [IsVolunteerRegistrationOpen] BIT             CONSTRAINT [DF_Events_IsVolunteerRegistrationOpen] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK__Events__3214EC2705D8E0BE] PRIMARY KEY CLUSTERED ([ID] ASC)
);

