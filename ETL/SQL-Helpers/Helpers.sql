--delete from Sessions
--delete from People where ID > 6
select Sessions.*, People.FirstName from Sessions inner join People on People.ID=Sessions.Speaker_ID 
where People.LastName='Cremese'
select count(*) from People
select count(*) from Sessions


--update people set IsAdmin=1 where People.ID=279

--update People set TShirtSize=0 where TShirtSize is null
--update Sessions set Timeslot_ID=2 where Timeslot_ID is null
select * from People where TShirtSize is NULL
--select * from Se where TShirtSize is NULL


USE [Prod_CodeCamp]
GO

select * from People where lastname='stipic'
select Sessions.* from Sessions inner join People on People.ID=Sessions.Speaker_ID where lastname='stipic'
--select * from Events
-- 
select count(*) as people from People -- 1299
select count(*) as sessions from Sessions -- 149
