/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

If '$(DeploySeedData)' = 'True'
Begin
Begin Tran


/*  Seed People */
Create Table #People(
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
    [TShirtSize]   INT             NULL
	)

Insert Into #People
(FirstName, LastName, Email, IsAdmin, TShirtSize)
Values
('NETDA', 'Admin', 'core@dotnetda.org', 0, 0),
('John', 'Smith', 'john@dotnetda.org', 0, 0),
('Brian', 'Hall', 'brian@dotnetda.org', 0, 0),
('Zdravko', 'Danev', 'z@onetug.org', 0, 0),
('Esteban', 'Garcia', 'esteban@dotnetda.org', 0, 0),
('Esteban2', 'Garcia2', 'esteban2@dotnetda.org', 0, 0)

Insert into People
(Email, FirstName, LastName, Title, Bio, Website, Blog, Twitter, PasswordHash, ImageUrl, IsAdmin, Location, TShirtSize)
Select
Email, FirstName, LastName, Title, Bio, Website, Blog, Twitter, PasswordHash, ImageUrl, IsAdmin, Location, TShirtSize
From #People p
Where p.Email Not In (Select Email From People)


Declare @personId Int; Select @personId = (Select Min(Id) From People)

/*  Seed Event */
Create Table #Events
(
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
	)

Insert Into #Events
(Name, StartTime, EndTime, Address1, Address2, City, State, Zip, IsDefault, IsSponsorRegistrationOpen, IsSpeakerRegistrationOpen, IsAttendeeRegistrationOpen, IsVolunteerRegistrationOpen)
Values
('Seattle Code Camp 2011', '03/21/2011', '03/21/2011', 'Seminole State College', '100 College Dr', 'Sanford', 'FL', '32746', 0, 0, 0, 0, 0),
('Seattle Code Camp 2012', '03/21/2012', '03/21/2012', 'Seminole State College', '100 College Dr', 'Sanford', 'FL', '32746', 1, 0, 0, 0, 0)

Insert Into Events
(Name, Description, TwitterHashTag, StartTime, EndTime, Location, Address1, Address2, City, State, Zip, IsDefault, IsSponsorRegistrationOpen, IsSpeakerRegistrationOpen, IsAttendeeRegistrationOpen, IsVolunteerRegistrationOpen)
Select Name, Description, TwitterHashTag, StartTime, EndTime, Location, Address1, Address2, City, State, Zip, IsDefault, IsSponsorRegistrationOpen, IsSpeakerRegistrationOpen, IsAttendeeRegistrationOpen, IsVolunteerRegistrationOpen
From #Events e
Where e.Name Not In (Select Name From Events)

/*  Seed Annoncements */
CREATE TABLE #Announcements (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Event_ID]    INT             NOT NULL,
    [Title]       NVARCHAR (100)  NULL,
    [Content]     NVARCHAR (2000) NULL,
    [PublishDate] DATETIME        NOT NULL)

Declare @Event1_Id Int; Select @Event1_Id = (Select Id From Events Where name = 'Seattle Code Camp 2011')
Declare @Event2_Id Int; Select @Event2_Id = (Select Id From Events Where name = 'Seattle Code Camp 2012')
Insert Into #Announcements
(Event_ID, Title, Content, PublishDate)
Values 
(@Event1_Id, 'call for speakers', 'This is the first announcement.', '1/1/2012'),
(@Event1_Id, 'call for volunteers', 'This is the first announcement.', '2/1/2012'),
(@Event1_Id, 'call for attendees', 'This is the first announcement.', '3/1/2012'),
(@Event2_Id, 'call for speakers', 'This is the first announcement.', '1/1/2012'),
(@Event2_Id, 'call for volunteers', 'This is the first announcement.', '2/1/2012'),
(@Event2_Id, 'call for attendees', 'This is the first announcement.', '3/1/2012')

Insert Into Announcements
Select Event_ID, Title, Content, PublishDate
From #Announcements a
Where Not Exists 
(Select *
From Announcements t
Where t.Title = a.Title and t.PublishDate = a.PublishDate)

/*  Seed TimeSlots */
Create Table #Timeslots
(
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Event_ID]  INT            NOT NULL,
    [Name]      NVARCHAR (100) NULL,
    [StartTime] DATETIME       NULL,
    [EndTime]   DATETIME       NULL
	)

Insert Into #Timeslots
(Event_ID, Name, StartTime, EndTime)
Values  (@Event2_Id, 'Morning Session 1' ,'3/1/2012 9:00:00.000', '3/1/2012 9:50:00.000'),
		(@Event2_Id, 'Morning Session 2' ,'3/1/2012 10:00:00.000', '3/1/2012 10:50:00.000'),
		(@Event2_Id, 'Morning Session 3' ,'3/1/2012 11:00:00.000', '3/1/2012 11:50:00.000'),
		(@Event2_Id, 'Afternoon Session 1' ,'3/1/2012 13:00:00.000', '3/1/2012 13:50:00.000'),
		(@Event2_Id, 'Afternoon Session 2' ,'3/1/2012 14:00:00.000', '3/1/2012 14:50:00.000'),
		(@Event2_Id, 'Afternoon Session 3' ,'3/1/2012 15:00:00.000', '3/1/2012 15:50:00.000'),
		(@Event2_Id, 'Afternoon Session 4' ,'3/1/2012 16:00:00.000', '3/1/2012 16:50:00.000')

Insert Into Timeslots
(Event_ID, Name, StartTime, EndTime)
Select Event_ID, Name, StartTime, EndTime
From #Timeslots
Where Name not in (Select Name From Timeslots)

Declare @TimeslotId Int; Select @TimeslotId = (Select Min(ID) From Timeslots)

/*  Seed Tracks */
Create Table #Tracks
(
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Event_ID]    INT             NOT NULL,
    [Name]        NVARCHAR (100)  NULL,
    [Description] NVARCHAR (2000) NULL
	)

Insert Into #Tracks
(Event_ID, Name, Description)
Values
(@Event2_Id, '"Windows Phone 7', 'Windows Phone 7 development'),
(@Event2_Id, 'Windows 8', 'Windows 8 development'),
(@Event2_Id, 'Architecture', 'Architecture, P and P')

Insert Into Tracks
Select Event_ID, Name, Description
From #Tracks
Where Name Not In (Select Name From Tracks)



/*  Seed Sessions */
Create Table #Sessions (
[ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Event_ID]    INT             NOT NULL,
    [Speaker_ID]  INT             NOT NULL,
    [Track_ID]    INT             NULL,
    [Timeslot_ID] INT             NULL,
    [Name]        NVARCHAR (100)  NULL,
    [Description] NVARCHAR (2000) NULL,
    [Status]      NVARCHAR (100)  NULL,
    [Level]       INT             NOT NULL,
    [Location]    NVARCHAR (100)  NULL,
    [Tag_ID]      INT             NULL
	)

Insert Into #Sessions
(Event_ID, Speaker_ID, Track_ID, Timeslot_ID, Name, Description, Status, Level)
Values
(@Event2_Id, @personId, 1, 1, 'Silverlight for WP7', 'Introduction in Silverlight programming with windows phone 7', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'XNA for WP7', 'Introduction in XNA programming with windows phone 7', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'Intro in Win 8', 'Introduction in Windows 8', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'P & P', 'Patterns and practices', 'Approved', 100)

Insert Into Sessions
(Event_ID, Speaker_ID, Track_ID, Timeslot_ID, Name, Description, Status, Level)
Select Event_ID, Speaker_ID, (Select Min(Id) From Tracks), @TimeslotId, Name, Description, Status, Level
From #Sessions
Where Name Not In (Select Name From Sessions)


/*  Seed Tags */
Create Table #Tags
(
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [TagName] NVARCHAR (50) NOT NULL
	)

Insert Into #Tags
(TagName)
Values 
('Architecture'),
('Career'),
('Cloud'),
('Data'),
('Game/VR'),
('Mobile'),
('Testing'),
('User Experience'),
('Web'),
('Other'),
('IoT'),
('Hardware')

Insert Into Tags
(TagName)
Select TagName
From #Tags
Where TagName Not In (Select TagName From #Tags)


/*  Seed KeyValuePair */
Create Table #KeyValuePair
(
    [Id]    NVARCHAR (100)  NULL,
    [Value] NVARCHAR (2000) NULL
	)

Insert Into #KeyValuePair
Values
('tshirtSizes', '[{"Item1":1,"Item2":"Don''t want one"},{"Item1":2,"Item2":"Small"},{"Item1":2,"Item2":"Medium"},{"Item1":2,"Item2":"Large"},{"Item1":2,"Item2":"X-Large"},{"Item1":2,"Item2":"XX-Large"}]')


Insert Into KeyValuePairs
Select *
From #KeyValuePair
Where Id Not In (Select Id From KeyValuePairs)



Commit Tran

End
