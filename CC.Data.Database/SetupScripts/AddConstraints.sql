PRINT N'Creating [dbo].[DF_EventAttendeeRating_SignIn]...';



ALTER TABLE [dbo].[EventAttendeeRatings]
    ADD CONSTRAINT [DF_EventAttendeeRating_SignIn] DEFAULT ((-1)) FOR [SignIn];



PRINT N'Creating [dbo].[DF_EventAttendeeRating_Swag]...';



ALTER TABLE [dbo].[EventAttendeeRatings]
    ADD CONSTRAINT [DF_EventAttendeeRating_Swag] DEFAULT ((-1)) FOR [Swag];



PRINT N'Creating [dbo].[DF_EventAttendeeRating_ReferralSource]...';



ALTER TABLE [dbo].[EventAttendeeRatings]
    ADD CONSTRAINT [DF_EventAttendeeRating_ReferralSource] DEFAULT ((-1)) FOR [ReferralSource];



PRINT N'Creating [dbo].[DF_EventAttendeeSessionRating_Ranking]...';



ALTER TABLE [dbo].[EventAttendeeSessionRatings]
    ADD CONSTRAINT [DF_EventAttendeeSessionRating_Ranking] DEFAULT ((-1)) FOR [Ranking];



PRINT N'Creating [dbo].[DF_Events_IsDefault]...';



ALTER TABLE [dbo].[Events]
    ADD CONSTRAINT [DF_Events_IsDefault] DEFAULT ((0)) FOR [IsDefault];



PRINT N'Creating [dbo].[DF_Events_IsSponsorRegistrationOpen]...';



ALTER TABLE [dbo].[Events]
    ADD CONSTRAINT [DF_Events_IsSponsorRegistrationOpen] DEFAULT ((0)) FOR [IsSponsorRegistrationOpen];



PRINT N'Creating [dbo].[DF_Events_IsSpeakerRegistrationOpen]...';



ALTER TABLE [dbo].[Events]
    ADD CONSTRAINT [DF_Events_IsSpeakerRegistrationOpen] DEFAULT ((0)) FOR [IsSpeakerRegistrationOpen];



PRINT N'Creating [dbo].[DF_Events_IsAttendeeRegistrationOpen]...';



ALTER TABLE [dbo].[Events]
    ADD CONSTRAINT [DF_Events_IsAttendeeRegistrationOpen] DEFAULT ((0)) FOR [IsAttendeeRegistrationOpen];



PRINT N'Creating [dbo].[DF_Events_IsVolunteerRegistrationOpen]...';



ALTER TABLE [dbo].[Events]
    ADD CONSTRAINT [DF_Events_IsVolunteerRegistrationOpen] DEFAULT ((0)) FOR [IsVolunteerRegistrationOpen];



PRINT N'Creating [dbo].[DF_Sessions_Tag_ID]...';



ALTER TABLE [dbo].[Sessions]
    ADD CONSTRAINT [DF_Sessions_Tag_ID] DEFAULT ((1)) FOR [Tag_ID];



PRINT N'Creating [dbo].[Announcement_Event]...';



ALTER TABLE [dbo].[Announcements] WITH NOCHECK
    ADD CONSTRAINT [Announcement_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID]);



PRINT N'Creating [dbo].[FK_EventAttendeeRating_EventAttendees]...';



ALTER TABLE [dbo].[EventAttendeeRatings] WITH NOCHECK
    ADD CONSTRAINT [FK_EventAttendeeRating_EventAttendees] FOREIGN KEY ([EventAttendee_ID]) REFERENCES [dbo].[EventAttendees] ([ID]);



PRINT N'Creating [dbo].[EventAttendee_Event]...';



ALTER TABLE [dbo].[EventAttendees] WITH NOCHECK
    ADD CONSTRAINT [EventAttendee_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID]);



PRINT N'Creating [dbo].[EventAttendee_Person]...';



ALTER TABLE [dbo].[EventAttendees] WITH NOCHECK
    ADD CONSTRAINT [EventAttendee_Person] FOREIGN KEY ([Person_ID]) REFERENCES [dbo].[People] ([ID]);



PRINT N'Creating [dbo].[FK_EventAttendeeSessionRating_EventAttendees]...';



ALTER TABLE [dbo].[EventAttendeeSessionRatings] WITH NOCHECK
    ADD CONSTRAINT [FK_EventAttendeeSessionRating_EventAttendees] FOREIGN KEY ([EventAttendee_ID]) REFERENCES [dbo].[EventAttendees] ([ID]);



PRINT N'Creating [dbo].[PersonTask_Person]...';



ALTER TABLE [dbo].[PersonTasks] WITH NOCHECK
    ADD CONSTRAINT [PersonTask_Person] FOREIGN KEY ([Person_ID]) REFERENCES [dbo].[People] ([ID]);



PRINT N'Creating [dbo].[PersonTask_Task]...';



ALTER TABLE [dbo].[PersonTasks] WITH NOCHECK
    ADD CONSTRAINT [PersonTask_Task] FOREIGN KEY ([Task_ID]) REFERENCES [dbo].[Tasks] ([ID]);



PRINT N'Creating [dbo].[SessionAttendee_Person]...';



ALTER TABLE [dbo].[SessionAttendees] WITH NOCHECK
    ADD CONSTRAINT [SessionAttendee_Person] FOREIGN KEY ([Person_ID]) REFERENCES [dbo].[People] ([ID]);



PRINT N'Creating [dbo].[SessionAttendee_Session]...';



ALTER TABLE [dbo].[SessionAttendees] WITH NOCHECK
    ADD CONSTRAINT [SessionAttendee_Session] FOREIGN KEY ([Session_ID]) REFERENCES [dbo].[Sessions] ([ID]);



PRINT N'Creating [dbo].[Session_Event]...';



ALTER TABLE [dbo].[Sessions] WITH NOCHECK
    ADD CONSTRAINT [Session_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID]);



PRINT N'Creating [dbo].[Session_Speaker]...';



ALTER TABLE [dbo].[Sessions] WITH NOCHECK
    ADD CONSTRAINT [Session_Speaker] FOREIGN KEY ([Speaker_ID]) REFERENCES [dbo].[People] ([ID]);



PRINT N'Creating [dbo].[Session_Tag]...';



ALTER TABLE [dbo].[Sessions] WITH NOCHECK
    ADD CONSTRAINT [Session_Tag] FOREIGN KEY ([Tag_ID]) REFERENCES [dbo].[Tags] ([ID]);



PRINT N'Creating [dbo].[Session_Timeslot]...';



ALTER TABLE [dbo].[Sessions] WITH NOCHECK
    ADD CONSTRAINT [Session_Timeslot] FOREIGN KEY ([Timeslot_ID]) REFERENCES [dbo].[Timeslots] ([ID]);



PRINT N'Creating [dbo].[Session_Track]...';



ALTER TABLE [dbo].[Sessions] WITH NOCHECK
    ADD CONSTRAINT [Session_Track] FOREIGN KEY ([Track_ID]) REFERENCES [dbo].[Tracks] ([ID]);



PRINT N'Creating [dbo].[Sponsor_Event]...';



ALTER TABLE [dbo].[Sponsors] WITH NOCHECK
    ADD CONSTRAINT [Sponsor_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID]);



PRINT N'Creating [dbo].[Task_Event]...';



ALTER TABLE [dbo].[Tasks] WITH NOCHECK
    ADD CONSTRAINT [Task_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID]);



PRINT N'Creating [dbo].[Timeslot_Event]...';



ALTER TABLE [dbo].[Timeslots] WITH NOCHECK
    ADD CONSTRAINT [Timeslot_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID]);



PRINT N'Creating [dbo].[Track_Event]...';



ALTER TABLE [dbo].[Tracks] WITH NOCHECK
    ADD CONSTRAINT [Track_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([ID]);



PRINT N'Checking existing data against newly created constraints';



ALTER TABLE [dbo].[Announcements] WITH CHECK CHECK CONSTRAINT [Announcement_Event];

ALTER TABLE [dbo].[EventAttendeeRatings] WITH CHECK CHECK CONSTRAINT [FK_EventAttendeeRating_EventAttendees];

ALTER TABLE [dbo].[EventAttendees] WITH CHECK CHECK CONSTRAINT [EventAttendee_Event];

ALTER TABLE [dbo].[EventAttendees] WITH CHECK CHECK CONSTRAINT [EventAttendee_Person];

ALTER TABLE [dbo].[EventAttendeeSessionRatings] WITH CHECK CHECK CONSTRAINT [FK_EventAttendeeSessionRating_EventAttendees];

ALTER TABLE [dbo].[PersonTasks] WITH CHECK CHECK CONSTRAINT [PersonTask_Person];

ALTER TABLE [dbo].[PersonTasks] WITH CHECK CHECK CONSTRAINT [PersonTask_Task];

ALTER TABLE [dbo].[SessionAttendees] WITH CHECK CHECK CONSTRAINT [SessionAttendee_Person];

ALTER TABLE [dbo].[SessionAttendees] WITH CHECK CHECK CONSTRAINT [SessionAttendee_Session];

ALTER TABLE [dbo].[Sessions] WITH CHECK CHECK CONSTRAINT [Session_Event];

ALTER TABLE [dbo].[Sessions] WITH CHECK CHECK CONSTRAINT [Session_Speaker];

ALTER TABLE [dbo].[Sessions] WITH CHECK CHECK CONSTRAINT [Session_Tag];

ALTER TABLE [dbo].[Sessions] WITH CHECK CHECK CONSTRAINT [Session_Timeslot];

ALTER TABLE [dbo].[Sessions] WITH CHECK CHECK CONSTRAINT [Session_Track];

ALTER TABLE [dbo].[Sponsors] WITH CHECK CHECK CONSTRAINT [Sponsor_Event];

ALTER TABLE [dbo].[Tasks] WITH CHECK CHECK CONSTRAINT [Task_Event];

ALTER TABLE [dbo].[Timeslots] WITH CHECK CHECK CONSTRAINT [Timeslot_Event];

ALTER TABLE [dbo].[Tracks] WITH CHECK CHECK CONSTRAINT [Track_Event];