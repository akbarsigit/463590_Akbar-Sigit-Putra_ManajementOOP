CREATE TABLE [dbo].[user]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [username] TEXT NOT NULL,
	[password] TEXT NOT NULL, 
	[cash] NUMERIC NOT NULL DEFAULT 100000.00
)
