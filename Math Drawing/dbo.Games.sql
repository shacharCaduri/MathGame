CREATE TABLE [dbo].[Games] (
    [GameID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL,
    [Level]  INT            DEFAULT ((0)) NOT NULL,
	[PlayerEmail] NVARCHAR(256) ,
    CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED ([GameID] ASC),
	FOREIGN KEY (PlayerEmail) REFERENCES AspNetUsers(UserName)
);

