CREATE TABLE [dbo].[Legs]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[From] BIGINT NOT NULL,
	[To] BIGINT NOT NULL, 
	CONSTRAINT [FK_Legs_Location_From] FOREIGN KEY ([From]) REFERENCES [dbo].[Locations]([Id]),
	CONSTRAINT [FK_Legs_Location_To] FOREIGN KEY ([To]) REFERENCES [dbo].[Locations]([Id])
)
