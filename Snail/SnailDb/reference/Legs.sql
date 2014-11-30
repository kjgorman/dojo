PRINT 'Updating location reference'

DECLARE @LegsReference TABLE ( [Id] BIGINT NOT NULL, [From] BIGINT NOT NULL, [To] BIGINT NOT NULL)

INSERT INTO @LegsReference VALUES (1, 1, 2)
INSERT INTO @LegsReference VALUES (2, 2, 6)
INSERT INTO @LegsReference VALUES (3, 3, 4)
INSERT INTO @LegsReference VALUES (4, 3, 5)
INSERT INTO @LegsReference VALUES (5, 5, 6)

/* update existing to match reference */
UPDATE [dbo].[Legs]
SET [From] = src.[From], [To] = src.[To]
FROM @LegsReference src INNER JOIN [dbo].[Legs] tgt WITH (UPDLOCK)
on src.[Id] = tgt.[Id]

/* insert missing records */
INSERT INTO [dbo].[Legs]
	SELECT src.[From], src.[To]
	FROM @LegsReference src LEFT OUTER JOIN [dbo].[Legs] tgt WITH (UPDLOCK, HOLDLOCK) ON src.[Id] = tgt.[Id]
WHERE tgt.[Id] IS NULL



