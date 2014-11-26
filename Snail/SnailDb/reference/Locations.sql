PRINT 'Updating location reference'

DECLARE @LocationReference TABLE ( [Id] BIGINT NOT NULL, [CountryName] NVARCHAR(255) NOT NULL, [PortName] NVARCHAR(255) NOT NULL)

INSERT INTO @LocationReference VALUES (1, 'Thailand', 'Bangkok')
INSERT INTO @LocationReference VALUES (2, 'Japan', 'Tokyo')
INSERT INTO @LocationReference VALUES (3, 'Greece', 'Athens')
INSERT INTO @LocationReference VALUES (4, 'Spain', 'Malaga')
INSERT INTO @LocationReference VALUES (5, 'England', 'Southampton')
INSERT INTO @LocationReference VALUES (6, 'France', 'Calais')

/* update existing to match reference */
UPDATE [dbo].[Locations]
SET [CountryName] = src.[CountryName], [PortName] = src.[PortName]
FROM @LocationReference src INNER JOIN[dbo].[Locations] tgt WITH (UPDLOCK)
on src.[Id] = tgt.[Id]

/* insert missing records */
INSERT INTO [dbo].[Locations]
	SELECT src.[CountryName], src.[PortName]
	FROM @LocationReference src LEFT OUTER JOIN [dbo].[Locations] tgt WITH (UPDLOCK, HOLDLOCK) ON src.[Id] = tgt.[Id]
WHERE tgt.[Id] IS NULL

/* delete unnecessary records */
DELETE [dbo].[Locations]
	FROM [dbo].[Locations] src
	WITH (UPDLOCK, HOLDLOCK) LEFT OUTER JOIN @LocationReference tgt ON src.[Id] = tgt.[Id]
WHERE tgt.[Id] IS NULL AND src.[CountryName] IS NOT NULL

