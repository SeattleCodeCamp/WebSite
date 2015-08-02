CREATE TABLE [dbo].[EventAttendeeRatings] (
    [ID]               INT   IDENTITY (1, 1) NOT NULL,
    [EventAttendee_ID] INT   NOT NULL,
    [SignIn]           INT   CONSTRAINT [DF_EventAttendeeRating_SignIn] DEFAULT ((-1)) NOT NULL,
    [Swag]             INT   CONSTRAINT [DF_EventAttendeeRating_Swag] DEFAULT ((-1)) NOT NULL,
    [Refreshments]     INT   NOT NULL,
    [ReferralSource]   INT   CONSTRAINT [DF_EventAttendeeRating_ReferralSource] DEFAULT ((-1)) NOT NULL,
    [Comments]         NTEXT NULL,
    CONSTRAINT [PK_EventAttendeeRating] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventAttendeeRating_EventAttendees] FOREIGN KEY ([EventAttendee_ID]) REFERENCES [dbo].[EventAttendees] ([ID])
);

