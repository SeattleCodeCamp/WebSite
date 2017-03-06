Insert Into People
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
From People p
Where p.Email Not In (Select Email From People)


Declare @personId Int; Select @personId = (Select Min(Id) From People)

Insert Into Events
(Name, StartTime, EndTime, Address1, Address2, City, State, Zip, IsDefault, IsSponsorRegistrationOpen, IsSpeakerRegistrationOpen, IsAttendeeRegistrationOpen, IsVolunteerRegistrationOpen)
Values
('Seattle Code Camp 2011', '03/21/2011', '03/21/2011', 'Seminole State College', '100 College Dr', 'Sanford', 'FL', '32746', 0, 0, 0, 0, 0),
('Seattle Code Camp 2012', '03/21/2012', '03/21/2012', 'Seminole State College', '100 College Dr', 'Sanford', 'FL', '32746', 1, 0, 0, 0, 0)

Insert Into Events
(Name, Description, TwitterHashTag, StartTime, EndTime, Location, Address1, Address2, City, State, Zip, IsDefault, IsSponsorRegistrationOpen, IsSpeakerRegistrationOpen, IsAttendeeRegistrationOpen, IsVolunteerRegistrationOpen)
Select Name, Description, TwitterHashTag, StartTime, EndTime, Location, Address1, Address2, City, State, Zip, IsDefault, IsSponsorRegistrationOpen, IsSpeakerRegistrationOpen, IsAttendeeRegistrationOpen, IsVolunteerRegistrationOpen
From Events e
Where e.Name Not In (Select Name From Events)

Declare @Event1_Id Int; Select @Event1_Id = (Select Id From Events Where name = 'Seattle Code Camp 2011')
Declare @Event2_Id Int; Select @Event2_Id = (Select Id From Events Where name = 'Seattle Code Camp 2012')
Insert Into Announcements
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
From Announcements a
Where Not Exists 
(Select *
From Announcements t
Where t.Title = a.Title and t.PublishDate = a.PublishDate)

Insert Into Timeslots
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
From Timeslots
Where Name not in (Select Name From Timeslots)

Declare @TimeslotId Int; Select @TimeslotId = (Select Min(ID) From Timeslots)

Insert Into Tracks
(Event_ID, Name, Description)
Values
(@Event2_Id, '"Windows Phone 7', 'Windows Phone 7 development'),
(@Event2_Id, 'Windows 8', 'Windows 8 development'),
(@Event2_Id, 'Architecture', 'Architecture, P and P')

Insert Into Tracks
Select Event_ID, Name, Description
From Tracks
Where Name Not In (Select Name From Tracks)

Insert Into Sessions
(Event_ID, Speaker_ID, Track_ID, Timeslot_ID, Name, Description, Status, Level)
Values
(@Event2_Id, @personId, 1, 1, 'Silverlight for WP7', 'Introduction in Silverlight programming with windows phone 7', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'XNA for WP7', 'Introduction in XNA programming with windows phone 7', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'Intro in Win 8', 'Introduction in Windows 8', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'P & P', 'Patterns and practices', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'Regular Expressions Boot Camp', 'Regular Expressions Boot Camp', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'Running ASP.NET without IIS', 'Running ASP.NET without IIS', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'Git in the enterprise environment', 'Git in the enterprise environment', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'Refactoring Workshop', 'Refactoring Workshop', 'Approved', 100),
(@Event2_Id, @personId, 1, 1, 'Mocking without Fear', 'Mocking without Fear', 'Approved', 100)

Insert Into Sessions
(Event_ID, Speaker_ID, Track_ID, Timeslot_ID, Name, Description, Status, Level, Tag_ID)
Select Event_ID, Speaker_ID, (Select Min(Id) From Tracks), @TimeslotId, Name, Description, Status, Level, null
From Sessions
Where Name Not In (Select Name From Sessions)

Insert Into Tags
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
From Tags
Where TagName Not In (Select TagName From Tags)

Insert Into KeyValuePairs
Values
('tshirtSizes', '[{"Item1":1,"Item2":"Don''t want one"},{"Item1":2,"Item2":"Small"},{"Item1":2,"Item2":"Medium"},{"Item1":2,"Item2":"Large"},{"Item1":2,"Item2":"X-Large"},{"Item1":2,"Item2":"XX-Large"}]')

Insert Into KeyValuePairs
Select *
From KeyValuePairs
Where Id Not In (Select Id From KeyValuePairs)