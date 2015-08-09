CREATE TABLE [dbo].[PersonTasks] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [Task_ID]   INT NOT NULL,
    [Person_ID] INT NOT NULL,
    CONSTRAINT [PK__PersonTa__3214EC27339FAB6E] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [PersonTask_Person] FOREIGN KEY ([Person_ID]) REFERENCES [dbo].[People] ([ID]),
    CONSTRAINT [PersonTask_Task] FOREIGN KEY ([Task_ID]) REFERENCES [dbo].[Tasks] ([ID])
);

