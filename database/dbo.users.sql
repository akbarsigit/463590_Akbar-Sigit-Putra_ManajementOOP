CREATE TABLE [dbo].[users] (
    [id]       INT          NOT NULL,
    [username] TEXT         NOT NULL,
    [password] TEXT         NOT NULL,
    [cash]     NUMERIC (18) DEFAULT ((100000.00)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);